using ApptManager.Filters;
using ApptManager.Mapping;
using ApptManager.Models;
using ApptManager.Repository;
using ApptManager.Services;
using AutoMapper;
using AutoWrapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load configuration FIRST
builder.Configuration.AddJsonFile("appsettings.json");

// ✅ Then read it safely
var configuration = builder.Configuration;

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));


// ✅ Dependency Injection
builder.Services.AddScoped<IDatabaseOperations, DatabaseOperations>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITaxProfessionalRepository, TaxProfessionalRepository>();
builder.Services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
var log = new LoggerConfiguration().WriteTo.File("C:\\OneDrive - H&R BLOCK LTD\\Documents\\TaxAppointment\\Appointment-booking-website\\ApptManager\\ApptManager\\Logs\\log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
Log.Logger = log;


builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>(); // add global filter
});


// ✅ Inject IDbConnection after configuration is ready
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(configuration.GetConnectionString("mydb"))
);

// ✅ CORS for Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true; // forces regeneration
    });
    app.UseSwaggerUI();
}


//middleware for exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseApiResponseAndExceptionWrapper();
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();

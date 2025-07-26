using ApptManager.Repository;
using ApptManager.Services;
using ApptManager.Mapping;
using Microsoft.Extensions.Configuration;
using System.Data;
using AutoMapper;
using System.Data.SqlClient;
using ApptManager.Models;

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

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();

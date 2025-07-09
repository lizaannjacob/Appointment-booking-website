using ApptManager.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("mydb");
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<int> InsertUserAsync(UserRegistrationInfo user)
    {
        using var db = CreateConnection();
        string sql = @"
            INSERT INTO user_registration_tbl (first_name, last_name, email, phone_number, password, role, is_email_verified)
            VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @Role, @IsEmailVerified);
            SELECT CAST(SCOPE_IDENTITY() as int)";
        return await db.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<bool> InsertLoginAsync(string email, string password, string role)
    {
        using var db = CreateConnection();
        string sql = "INSERT INTO apm_login_tbl (email, password, role) VALUES (@Email, @Password, @Role)";
        var rows = await db.ExecuteAsync(sql, new { Email = email, Password = password, Role = role });
        return rows > 0;
    }

    public async Task<string> GetRoleByCredentialsAsync(string email, string password)
    {
        using var db = CreateConnection();
        string sql = "SELECT role FROM apm_login_tbl WHERE email = @Email AND password = @Password";
        return await db.QueryFirstOrDefaultAsync<string>(sql, new { Email = email, Password = password });
    }

    public async Task<IEnumerable<TaxProfessionalWithSlots>> GetProfessionalsWithSlotsAsync()
    {
        var query = @"
        SELECT tp.id, tp.name, s.slot_id AS SlotId, s.slot_start AS SlotStart, s.slot_end AS SlotEnd, s.status
        FROM tax_professional_tbl tp
        INNER JOIN availability_slots_tbl s ON tp.id = s.professional_id
        WHERE s.status = 'available'
        ORDER BY tp.id, s.slot_start";

        using var connection = CreateConnection();
        var lookup = new Dictionary<int, TaxProfessionalWithSlots>();

        var result = await connection.QueryAsync<TaxProfessionalWithSlots, AvailabilitySlot, TaxProfessionalWithSlots>(
            query,
            (pro, slot) =>
            {
                if (!lookup.TryGetValue(pro.Id, out var taxPro))
                {
                    taxPro = new TaxProfessionalWithSlots
                    {
                        Id = pro.Id,
                        Name = pro.Name,
                        Slots = new List<AvailabilitySlot>()
                    };
                    lookup[pro.Id] = taxPro;
                }

                taxPro.Slots.Add(slot);
                return taxPro;
            },
            splitOn: "SlotId"
        );

        return lookup.Values;
    }

    public async Task<int> GetProfessionalIdByEmailAsync(string email)
    {
        string sql = "SELECT id FROM tax_professional_tbl WHERE email = @Email";
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<int>(sql, new { Email = email });
    }

    public async Task<List<AvailabilitySlot>> GetBookedSlotsByProfessionalIdAsync(int professionalId)
    {
        string sql = "SELECT * FROM availability_slots_tbl WHERE professional_id = @ProfessionalId AND status = 'booked'";
        using var connection = CreateConnection();
        var result = await connection.QueryAsync<AvailabilitySlot>(sql, new { ProfessionalId = professionalId });
        return result.AsList();
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByProfessionalAsync(int professionalId)
    {
        var query = @"
        SELECT slot_id AS SlotId, professional_id AS ProfessionalId, slot_start AS SlotStart, slot_end AS SlotEnd, status
        FROM availability_slots_tbl
        WHERE professional_id = @ProfessionalId AND status = 'available'
        ORDER BY slot_start";

        using var connection = CreateConnection();
        return await connection.QueryAsync<AvailabilitySlot>(query, new { ProfessionalId = professionalId });
    }

    public async Task<bool> UpdateSlotStatusAsync(int slotId, string status, string email)
    {
        string sql = @"UPDATE availability_slots_tbl 
                   SET status = @Status, 
                       booked_by_email = @Email 
                   WHERE slot_id = @SlotId";

        using var connection = CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Status = status, Email = email, SlotId = slotId });
        return rows > 0;
    }



    public async Task<IEnumerable<AvailabilitySlot>> GetBookedSlotsByEmailAsync(string email)
    {
        string sql = @"SELECT * FROM availability_slots_tbl WHERE booked_by_email = @Email";

        using var connection = CreateConnection();
        return await connection.QueryAsync<AvailabilitySlot>(sql, new { Email = email });
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetBookedSlotsByUserEmailAsync(string email)
    {
        var sql = @"SELECT s.slot_id AS SlotId, s.professional_id AS ProfessionalId, s.slot_start AS SlotStart, 
                       s.slot_end AS SlotEnd, s.status AS Status, p.name AS ProfessionalName
                FROM availability_slots_tbl s
                JOIN tax_professional_tbl p ON s.professional_id = p.id
                WHERE s.booked_by_email = @Email";

        using var connection = CreateConnection();
        return await connection.QueryAsync<AvailabilitySlot>(sql, new { Email = email });
    }



}

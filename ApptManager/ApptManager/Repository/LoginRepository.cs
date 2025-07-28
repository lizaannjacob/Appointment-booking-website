using ApptManager.Models;
using ApptManager.Repository;
using Dapper;
using System.Data;
using System.Threading.Tasks;

public class LoginRepository : ILoginRepository
{
    private readonly IGenericRepository<LoginInfo> _genericRepo;

    public LoginRepository(IDbConnection db)
    {
        _genericRepo = new GenericRepository<LoginInfo>(db);
    }

    public async Task<LoginInfo> GetLoginByEmailAsync(string email)
    {
        string query = "SELECT * FROM apm_login_tbl WHERE email = @Email";
        return await _genericRepo.GetByIdAsync(query, new { Email = email });
    }

    public async Task<bool> InsertLoginAsync(LoginInfo login)
    {
        string query = @"INSERT INTO apm_login_tbl (user_id, email, password, created_at)
                         VALUES (@UserId, @Email, @Password, @CreatedAt)";
        return await _genericRepo.ExecuteAsync(query, login);
    }
}

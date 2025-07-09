using ApptManager.Models;
using ApptManager.Repository;
using Dapper;
using System.Data;
using System.Threading.Tasks;

public class LoginRepository : ILoginRepository
{
    private readonly IDbConnection _db;

    public LoginRepository(IDbConnection db)
    {
        _db = db;
    }


    public async Task<bool> InsertLoginAsync(LoginInfo login)
    {
        string query = @"
            INSERT INTO apm_login_tbl (user_id, email, password, created_at)
            VALUES (@UserId, @Email, @Password, @CreatedAt)";
        var result = await _db.ExecuteAsync(query, login);
        return result > 0;
    }

    public async Task<LoginInfo> GetLoginByEmailAsync(string email)
    {
        string query = "SELECT * FROM apm_login_tbl WHERE email = @Email";
        return await _db.QueryFirstOrDefaultAsync<LoginInfo>(query, new { Email = email });
    }

}

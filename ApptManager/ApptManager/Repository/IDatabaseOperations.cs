using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ApptManager.Models;

namespace ApptManager.Repository
{
    public interface IDatabaseOperations
    {
        IDbConnection CreateConnection();

        Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters);

        Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, DynamicParameters parameters);

        Task<int> RegisterUserAsync(UserRegistrationInfo user);

        // ✅ Add support for SQL queries like SELECT * FROM admin_tbl WHERE ...
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters);
    }
}

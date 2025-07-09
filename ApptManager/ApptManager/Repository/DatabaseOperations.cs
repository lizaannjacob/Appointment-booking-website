using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ApptManager.Models;

namespace ApptManager.Repository
{
    public class DatabaseOperations : IDatabaseOperations
    {
        private readonly string _connectionString;

        public DatabaseOperations(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("mydb");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, DynamicParameters parameters)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<int> RegisterUserAsync(UserRegistrationInfo user)
        {
            using var connection = CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@Password", user.Password);
            parameters.Add("@Role", user.Role);
            parameters.Add("@IsEmailVerified", user.IsEmailVerified);

            return await connection.ExecuteAsync(
                "InsertUserRegistration",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}

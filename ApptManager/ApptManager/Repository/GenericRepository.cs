using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _connection;

        public GenericRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string sql)
        {
            return await _connection.QueryAsync<T>(sql);
        }

        public async Task<T> GetByIdAsync(string sql, object param)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public async Task<int> InsertAsync(string sql, T entity)
        {
            return await _connection.ExecuteAsync(sql, entity);
        }

        public async Task<bool> ExecuteAsync(string sql, object param)
        {
            var result = await _connection.ExecuteAsync(sql, param);
            return result > 0;
        }
    }
}

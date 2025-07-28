using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string sql);
        Task<T> GetByIdAsync(string sql, object param);
        Task<int> InsertAsync(string sql, T entity);
        Task<bool> ExecuteAsync(string sql, object param);
    }
}

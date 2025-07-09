using ApptManager.Models;

namespace ApptManager.Repository
{
    public interface ILoginRepository
    {
        Task<LoginInfo> GetLoginByEmailAsync(string email);
        Task<bool> InsertLoginAsync(LoginInfo login);
    }
}

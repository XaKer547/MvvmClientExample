using MvvmClientExample.Domain.Models;
using System.Threading.Tasks;

namespace MvvmClientExample.Domain.Services
{
    public interface IUserService
    {
        UserDTO GetUser(int id);
        Task<bool> VerifyPassword(int userId, string newPassword);
        Task ChangePassword(int userId, string newPassword);
        void SetUserVerified(int userId);
        void DropTwoFactorAuthentification(int userId);
    }
}

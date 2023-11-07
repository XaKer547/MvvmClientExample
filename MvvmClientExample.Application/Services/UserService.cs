using MvvmClientExample.Application.Helpers;
using MvvmClientExample.DataAccess.Data;
using MvvmClientExample.Domain.Models;
using MvvmClientExample.Domain.Services;

namespace MvvmClientExample.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AuthDbContext _context;
        public UserService(AuthDbContext context)
        {
            _context = context;
        }

        public UserDTO GetUser(int id)
        {
            return _context.Users.Select(u => new UserDTO()
            {
                Id = u.Id,
                Password = u.Password,
                Email = u.Email,
                IsVerified = u.IsVerified
            }).SingleOrDefault(u => u.Id == id);
        }

        public async Task<bool> VerifyPassword(int userId, string oldPassword)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
            {
                return false;
            }

            return user.Password == oldPassword.ToHash();
        }

        public async Task ChangePassword(int userId, string newPassword)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return;

            user.Password = newPassword.ToHash();

            user.IsForgotPassword = false;

            user.TemporaryPassword = null;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async void SetUserVerified(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return;

            user.IsVerified = true;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public void DropTwoFactorAuthentification(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return;

            user.IsVerified = false;

            _context.Users.Update(user);
        }
    }
}

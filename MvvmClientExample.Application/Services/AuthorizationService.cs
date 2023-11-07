using Microsoft.EntityFrameworkCore;
using MvvmClientExample.Application.Helpers;
using MvvmClientExample.DataAccess.Data;
using MvvmClientExample.DataAccess.Data.Entities;
using MvvmClientExample.Domain.Models;
using MvvmClientExample.Domain.Models.Enums;
using MvvmClientExample.Domain.Services;

namespace MvvmClientExample.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AuthDbContext _context;
        public AuthorizationService(AuthDbContext context)
        {
            _context = context;
        }

        public AuthorizationResultDTO Authorize(LoginDTO login)
        {
            var user = _context.Users.SingleOrDefault(u => u.Login == login.Login);

            if (user == null)
                return new AuthorizationResultDTO()
                {
                    IsSuccess = false
                };

            if (user.Password != login.Password.ToHash() && user.TemporaryPassword != login.Password)
                return new AuthorizationResultDTO()
                {
                    IsSuccess = false
                };

            return new AuthorizationResultDTO()
            {
                IsSuccess = true,
                UserId = user.Id,
                IsVerified = user.IsVerified
            };
        }

        public async Task<TemporaryPasswordRequestDTO> BeginPasswordRestore(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

            if (user is null)
            {
                return new TemporaryPasswordRequestDTO()
                {
                    Result = PasswordRestoreResults.EmailNotFound
                };
            }

            if (user.IsForgotPassword)
            {
                return new TemporaryPasswordRequestDTO()
                {
                    Result = PasswordRestoreResults.AlreadyUsed
                };
            }

            user.IsForgotPassword = true;

            _context.Users.Update(user);

            var tempPassword = (user.Login + 12).ToHash();

            user.TemporaryPassword = tempPassword;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return new TemporaryPasswordRequestDTO()
            {
                Result = PasswordRestoreResults.Success,
                TemporaryPassword = user.TemporaryPassword
            };
        }

        public async Task Register(RegisterDTO register)
        {
            var user = new User()
            {
                Login = register.Login,
                Email = register.Email.ToLower(),
                Password = register.Password.ToHash(),
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}
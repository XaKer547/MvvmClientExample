using MvvmClientExample.Domain.Models;
using System.Threading.Tasks;

namespace MvvmClientExample.Domain.Services
{
    public interface IAuthorizationService
    {
        AuthorizationResultDTO Authorize(LoginDTO login);

        Task Register(RegisterDTO register);

        Task<TemporaryPasswordRequestDTO> BeginPasswordRestore(string userName);
    }
}

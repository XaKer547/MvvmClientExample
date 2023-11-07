using System.Threading.Tasks;

namespace MvvmClientExample.Domain.Services
{
    public interface IMailService
    {
        Task SendVerificationCode(int verificationCode, string userEmail);
    }
}

using MvvmClientExample.Domain.Models.Enums;

namespace MvvmClientExample.Domain.Models
{
    public class TemporaryPasswordRequestDTO
    {
        public string TemporaryPassword { get; set; }
        public PasswordRestoreResults Result { get; set; }
    }
}

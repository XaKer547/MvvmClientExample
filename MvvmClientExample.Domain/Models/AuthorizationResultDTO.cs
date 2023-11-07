namespace MvvmClientExample.Domain.Models
{
    public class AuthorizationResultDTO
    {
        public bool IsSuccess { get; set; }
        public bool IsVerified { get; set; }
        public int UserId { get; set; }
    }
}

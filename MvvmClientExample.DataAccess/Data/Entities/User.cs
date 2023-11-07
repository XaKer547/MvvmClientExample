namespace MvvmClientExample.DataAccess.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? TemporaryPassword { get; set; }
        public bool IsVerified { get; set; } = false;
        public bool IsForgotPassword { get; set; } = false;
    }
}

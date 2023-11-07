namespace MvvmClientExample.Domain.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
    }
}

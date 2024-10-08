
namespace WonderPlane.Shared
{
    public class UserInfo
    {
        public int Id { get; set; }

        public required string Document { get; set; }

        public required string UserName { get; set; }

        public required string Name { get; set; }

       
        public required string LastName { get; set; }

       
        public required DateTime BirthDate { get; set; }

        public string? Gender { get; set; }

        public string? PhoneNumber { get; set; }

        public required string Email { get; set; }

        public string? Address { get; set; }

        public string? Country { get; set; }

        public bool? IsSuscribedToNews { get; set; } = false;

        public bool? ReciveNotifications { get; set; } = false;

        public bool? IsActive { get; set; } = true;

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? Image { get; set; }
    }
}

namespace TetPee.Services.User;

public class Response
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null;
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string? ImageUrl { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        
        public string? Address { get; set; }
        public string Role { get; set; } = "User";
        public string? dateOfBirth { get; set; } = null;
    }
}
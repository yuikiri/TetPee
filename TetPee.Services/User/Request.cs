namespace TetPee.Services.User;

public class Request
{
    public class CreateUserRequest
    {
        public required string  Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password  { get; set; }
    }

    public class UpdateUserRequest : CreateUserRequest
    {
        public Guid id  { get; set; }
    }
}
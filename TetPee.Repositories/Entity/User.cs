using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;
public class User: BaseEntity<Guid>, IAuditableEntity
//sau dấu : thì là kế thừa(expect), và sau đó mới là impliment
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ImageUrl { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
    public required string HashedPassword { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = "User"; // User, Seller, Admin
    public bool IsVerify { get; set; } = false; // Khi user register, thì phải verify email hợp lệ
    public int VerifyCode { get; set; } // Mã verify gửi về email
    
    public Seller? Seller { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    //mối quan hệ 1 nhiều với Order, 1 User có nhiều Order
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
}





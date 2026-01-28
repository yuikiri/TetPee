using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;

public class Order: BaseEntity<Guid>, IAuditableEntity
{
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Processing, Completed, Cancelled
    public required string Address { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
}
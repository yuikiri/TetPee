using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;

public class OrderDetail: BaseEntity<Guid>, IAuditableEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Price at the time of order
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
}
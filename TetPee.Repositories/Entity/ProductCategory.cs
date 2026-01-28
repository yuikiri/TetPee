using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;

public class ProductCategory: BaseEntity<Guid>, IAuditableEntity
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
}
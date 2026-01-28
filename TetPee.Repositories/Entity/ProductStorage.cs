using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;

public class ProductStorage: BaseEntity<Guid>, IAuditableEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public Guid StorageId { get; set; }
    public Storage Storage { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
}
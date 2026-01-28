using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;

public class Storage: BaseEntity<Guid>, IAuditableEntity
{
    public decimal Price { get; set; }
    public required string Type { get; set; } // Export, Import
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
    
    public ICollection<ProductStorage> ProductStorages { get; set; } = new List<ProductStorage>();
}
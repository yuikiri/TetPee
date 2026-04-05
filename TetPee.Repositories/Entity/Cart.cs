using TetPee.Repositories.Abtraction;
using TetPee.Repository.Abtraction;

namespace TetPee.Repositories.Entity;

public class Cart: BaseEntity<Guid>, IAuditableEntity
{
    public  Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<CartDetail> CartDetails { get; set; } =  new List<CartDetail>();
    
    public DateTimeOffset CreatedAt { get; set; } // Dòng dữ liệu này được tạo ra khi nào
    public DateTimeOffset? UpdatedAt { get; set; } // Dòng dữ liệu này được cập nhật lần cuối khi nào
}
using TetPee.Repositories.Abstraction;

namespace TetPee.Repositories.Entity;

public class Cart
{
    public Guid Id { get; set; }
    
    public bool IsDeleted { get; set; } = false; // Soft Delete, Tránh xung đột khóa ngoại (Foreign Key Constraint)
    public DateTimeOffset CreatedAt { get; set; } // Dòng dữ liệu này được tạo ra khi nào
    public DateTimeOffset? UpdateAt { get; set; } // Dòng dữ liệu này được cập nhật lần cuối khi nào
}
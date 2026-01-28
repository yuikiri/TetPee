namespace TetPee.Repositories.Abstraction;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }//dòng dữ liệu này đc tạo khi nào
    public DateTimeOffset? UpdateAt { get; set; }//dòng dữ liệu này đc cập nhập ần cuối khi nào
}
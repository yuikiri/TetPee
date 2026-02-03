namespace TetPee.Repositories.Abstraction;

public abstract class BaseEntity <TKey>
{
    public TKey Id { get; set; }
    
    public bool IsDelete { get; set; } = false;//Soft Delete, tránh xung đột khóa ngoại ()
    
}
using Microsoft.EntityFrameworkCore;
using TetPee.Repositories.Entity;

namespace TetPee.Repositories;

public class AppDbContext: DbContext
// DbContext là 1 class quan trọng của entity framword,
// nó là 1 đại diện cho database
//mik làm việc với n, nó làm việc với database
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}
    public DbSet<User> Users { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategorys { get; set; }
    public DbSet<ProductStorage> ProductStorages { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
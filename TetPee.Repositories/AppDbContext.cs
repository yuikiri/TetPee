using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TetPee.Repositories.Entity;

namespace TetPee.Repositories;

public class AppDbContext : DbContext
{
    public static Guid UserId1 = Guid.NewGuid();
    public static Guid UserId2 = Guid.NewGuid();
    
    public static Guid CategoryParent1 = Guid.NewGuid();
    public static Guid CategoryParent2 = Guid.NewGuid();

    public static Guid ProductId1 = Guid.NewGuid();
    public static Guid ProductId2 = Guid.NewGuid();
    public static Guid ProductId3 = Guid.NewGuid();
    public static Guid ProductId4 = Guid.NewGuid();

    public static Guid SellerId1 = Guid.NewGuid();
    
    public static Guid OrderId2 = Guid.NewGuid();
    public static Guid OrderId1 = Guid.NewGuid();
    
    public static Guid InventoryId1 = Guid.NewGuid();
    public static Guid InventoryId2 = Guid.NewGuid();
    
    public static Guid StorageId1 = Guid.NewGuid();
    public static Guid StorageId2 = Guid.NewGuid();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //đây là clss đại diện cho db
    }
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
        // ==================== User Configuration ====================
        //user
        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                .IsUnique(); //indexing

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            // LastName - required, max 100 characters
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            // ImageUrl - nullable, max 500 characters (URL)
            builder.Property(u => u.ImageUrl)
                .HasMaxLength(500);

            // PhoneNumber - nullable, max 20 characters
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            // HashedPassword - required, max 500 characters
            builder.Property(u => u.HashedPassword)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("User");
            
            // Relationship: User has one Seller (one-to-one)
            builder.HasOne(u => u.Seller)
                .WithOne(s => s.User)
                .HasForeignKey<Seller>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            // DeleteBehavior.Cascade: Khi một User bị xóa, thì Seller liên quan cũng sẽ bị xóa theo.
            // DeleteBehavior.Restrict: Ngăn chặn việc xóa một User nếu có Seller liên quan tồn tại.
                //(Tham chiếu tới PK tồn tại)
                // 1 Project còn Task thì không xoá được
            // DeleteBehavior.NoAction: Không thực hiện hành động gì đặc biệt khi User bị xóa. ( Gàn giống Restrict, xử lí ở DB)
            // DeleteBehavior.SetNull: Khi một User bị xóa, thì trường UserId trong bảng Seller sẽ được đặt thành NULL.
                //(Áp dụng khi trường FK cho phép NULL)
//==================
            
            List<User> users = new List<User>()
            {
                new()
                {
                    Id = UserId1,
                    Email = "tan182205@gmail.com",
                    FirstName = "Tan",
                    LastName = "Tran",
                    HashedPassword = "hashed_password_1",
                },
                new()
                {
                    Id = UserId2,
                    Email = "tan182206@gmail.com",
                    FirstName = "Tan",
                    LastName = "Tran",
                    HashedPassword = "hashed_password_1",
                }
            };

            for (int i = 0; i <= 1000; i++)
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "Duong" + i,
                    FirstName = "Duong" + i,
                    LastName = "Duong" + i,
                    HashedPassword = "hashed_password_" + i,
                };
                users.Add(newUser);
            }

            builder.HasData(users);
        });
        //seller
        modelBuilder.Entity<Seller>(builder =>
        {
            builder.Property(s => s.TaxCode).IsRequired().HasMaxLength(50);

            builder.Property(s => s.CompanyName).IsRequired().HasMaxLength(200);

            builder.Property(s => s.CompanyAddress).IsRequired().HasMaxLength(500);

            var Seller = new List<Seller>()
            {
                new()
                {
                    Id = SellerId1,
                    TaxCode = "TAXCODE123",
                    CompanyName = "ABC Company",
                    CompanyAddress = "123 Main st , Cityville",
                    UserId = UserId1,
                }
            };
            builder.HasData(Seller);
        });
        
        //Category
        modelBuilder.Entity<Category>(builder =>
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            var categories = new List<Category>()
            {
                new()
                {
                    Id = CategoryParent1,
                    Name = "Áo"
                },
                new()
                {
                    Id = CategoryParent2,
                    Name = "Quần"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo thể thao",
                    ParentId = CategoryParent1,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo mùa đông",
                    ParentId = CategoryParent1,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo ba lỗ",
                    ParentId = CategoryParent1,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Quần ba lỗ",
                    ParentId = CategoryParent2,
                },
                
            };
            
            builder.HasData(categories);
        });
        
        //product
        modelBuilder.Entity<Product>(builder =>
        {   
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(u => u.UrlImage)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(u => u.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(u => u.Seller)
                .WithMany(u => u.Products)
                .HasForeignKey(u => u.SellerId);
            
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = ProductId1,
                    Name = "Áo Thun Nam",
                    Description = "Áo thun nam chất liệu cotton cao cấp, thoáng mát, phù hợp cho mọi hoạt động hàng ngày.",
                    UrlImage = "https://example.com/images/ao_thun_nam.jpg",
                    Price = 199000m,
                    SellerId = SellerId1
                },
                new Product()
                {
                    Id = ProductId2,
                    Name = "Quần Jeans Nữ",
                    Description = "Quần jeans nữ dáng ôm, tôn dáng, chất liệu denim co giãn, phù hợp cho mọi dịp.",
                    UrlImage = "https://example.com/images/quan_jeans_nu.jpg",
                    Price = 399000m,
                    SellerId = SellerId1
                },
                new Product()
                {
                    Id = ProductId3,
                    Name = "Áo Sơ Mi Nam",
                    Description = "Áo sơ mi nam công sở, thiết kế hiện đại, chất liệu vải cao cấp, thoáng mát.",
                    UrlImage = "https://example.com/images/ao_so_mi_nam.jpg",
                    Price = 299000m,
                    SellerId = SellerId1
                },
                new Product()
                {
                    Id = ProductId4,
                    Name = "Chân Váy Nữ",
                    Description = "Chân váy nữ xòe, thiết kế trẻ trung, chất liệu vải mềm mại, phù hợp cho mọi dịp.",
                    UrlImage = "https://example.com/images/chan_vay_nu.jpg",
                    Price = 249000m,
                    SellerId = SellerId1
                }
            };
            builder.HasData(products);
        });
        
        //order detail
        modelBuilder.Entity<OrderDetail>(builder =>
        {
            var orderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    Id =  Guid.NewGuid(),
                    OrderId = OrderId1,
                    ProductId = ProductId1,
                    Quantity = 2,
                    UnitPrice = 200m,
                },
                new OrderDetail()
                {
                    Id =  Guid.NewGuid(),
                    OrderId = OrderId1,
                    ProductId = ProductId2,
                    Quantity = 2,
                    UnitPrice = 200m,
                },
                new OrderDetail()
                {
                    Id =  Guid.NewGuid(),
                    OrderId = OrderId2,
                    ProductId = ProductId3,
                    Quantity = 2,
                    UnitPrice = 200m,
                },
            };
            builder.HasData(orderDetails);
        });
        
        //order
        modelBuilder.Entity<Order>(builder =>
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = OrderId1,
                    UserId = UserId2,
                    Address = "Bien Hoa, Dong Nai",
                    TotalAmount = 10000m,
                    Status = "Completed",
                },
                new Order()
                {
                    Id = OrderId2,
                    UserId = UserId2,
                    Address = "Bien Hoa, Dong Nai",
                    TotalAmount = 10000m,
                    Status = "Completed",
                }
            };
            builder.HasData(orders);
        });
        
        //Inventory
        modelBuilder.Entity<Inventory>(builder =>
        {
            var inventories = new List<Inventory>()
            {
                new Inventory()
                {
                    Id =  InventoryId1,
                    ProductId = ProductId1,
                    TotalSell = 10,
                    TotalInStock = 100,
                },
                new Inventory()
                {
                    Id =  InventoryId2,
                    ProductId = ProductId1,
                    TotalSell = 1,
                    TotalInStock = 10,
                }
            };
            builder.HasData(inventories);
        });
        
        //ProductCategory
        modelBuilder.Entity<ProductCategory>(builder =>
        {
            var productCategories = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Id =  Guid.NewGuid(),
                    ProductId = ProductId1,
                    CategoryId = CategoryParent1
                },
                new ProductCategory()
                {
                    Id =  Guid.NewGuid(),
                    ProductId = ProductId1,
                    CategoryId = CategoryParent2
                }
            };
            builder.HasData(productCategories);
        });
        
        //Storage
        modelBuilder.Entity<Storage>(builder =>
        {
            builder.Property(u => u.Type)
                .IsRequired()
                .HasMaxLength(100);
            var storages = new List<Storage>()
            {
                new Storage()
                {
                    Id =  StorageId1,
                    Price = 1000,
                    Type = "ao 1"
                },
                new Storage()
                {
                    Id =  StorageId2,
                    Price = 10000,
                    Type = "ao 2"
                }
            };
            builder.HasData(storages);
        });
        
        //ProductStorage
        modelBuilder.Entity<ProductStorage>(builder =>
        {
            var productStorages = new List<ProductStorage>()
            {
                new ProductStorage()
                {
                    Id =  Guid.NewGuid(),
                    ProductId = ProductId1,
                    StorageId = StorageId1,
                },
                new ProductStorage()
                {
                    Id =  Guid.NewGuid(),
                    ProductId = ProductId1,
                    StorageId = StorageId2
                }
            };
            builder.HasData(productStorages);
        });
    }
}
        
        

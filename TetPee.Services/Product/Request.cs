namespace TetPee.Services.Product;

public class Request
{
    public class CreateProductRequest
    {
        public required Guid SellerId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public List<Guid>? CategoryIds { get; set; }
    }
    
}
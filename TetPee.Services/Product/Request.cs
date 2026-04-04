using System.ComponentModel.DataAnnotations;

namespace TetPee.Services.Product;

public class Request
{
    public class CreateProductRequest
    {
        
        // public required Guid SellerId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public required string Name { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(1500)]
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public List<Guid>? CategoryIds { get; set; }
    }
    
}
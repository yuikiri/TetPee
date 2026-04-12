namespace TetPee.Services.Order;

public class Response
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public required string BankName { get; set; }
        public required string BankAccount { get; set; }
        public required string Description { get; set; }
        public required string QrCode { get; set; }
    }
    
    public class GetOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Address { get; set; }
        public List<ProductOder>  Products { get; set; }
        public string Status { get; set; } 
    }
    
    public class ProductOder
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
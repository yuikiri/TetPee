namespace TetPee.Services.Order;

public class Request
{
    public class CreateOrderRequest
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<ProductOrderRequest> Products { get; set; }
    }

    public class ProductOrderRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    
    public class SepayWebhookRequest
    {
        public string Gateway { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AccountNumber { get; set; }
        public string SubAccount { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public string TransferType { get; set; }
        public string Description { get; set; }
        public decimal TransferAmount { get; set; }
        public string ReferenceCode { get; set; }
        public decimal Accumulated { get; set; }
        public long Id { get; set; }
    }
    
    //tạo đơn hàng (muốn đặt hàng phải chuyển khoản trước nhé)
        //setup chuyển khoản thành công(bằng tiền thâật). để xác nhận đơn hàng này đã đc đặt
        //nếu tạo ra đơn hàng mà ko chuyển khoản liền, thì đơn hàng sẽ bị hủy sau 15 phút
        //để tránh tình trạng khách hàng đặt hàng rồi mà ko chuyển khoản, ảnh hưởng ến việc quản lý
    // public class UpdateOrderRequest
    // {
    //     
    // }
    
}
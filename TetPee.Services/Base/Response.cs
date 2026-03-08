namespace TetPee.Services.Base;

public class Response
{
    public class PageResult<T>
    {
        //phải tạo 1 cái phân trang cho forn end nó thấy để dễ xử lý, vì forn ko thấy đc database
        
        public List<T>? Item { get; set; } = new List<T>();
        public int TotalItems  { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
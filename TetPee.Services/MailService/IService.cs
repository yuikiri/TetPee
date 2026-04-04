namespace TetPee.Services.MailService;

public interface IService
{
    public Task SendMail(MailContent mailContent);
}

public class MailContent
{
    public required string To { get; set; }//đại chỉ gửi đến
    public required string Subject { get; set; }//chủ đề
    public required string Body { get; set; }//nội dung mail
}
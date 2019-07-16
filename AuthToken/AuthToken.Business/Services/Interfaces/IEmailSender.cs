namespace AuthToken.Business.Services.Interfaces
{
    public interface IEmailSender
    {
        bool SendMail(string email, string subject, string body);
    }
}

namespace ITPLibrary.Core.Services.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}

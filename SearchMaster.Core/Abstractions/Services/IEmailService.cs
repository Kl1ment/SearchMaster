
namespace SearchMaster.Application.Services
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
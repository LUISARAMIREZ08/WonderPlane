using WonderPlane.Shared;

namespace WonderPlane.Server.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}

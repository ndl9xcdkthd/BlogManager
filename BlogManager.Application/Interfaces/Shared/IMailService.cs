using BlogManager.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace BlogManager.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
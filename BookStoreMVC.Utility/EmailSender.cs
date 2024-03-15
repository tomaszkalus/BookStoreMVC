using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookStoreMVC.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Logic for sending email
            return Task.CompletedTask;
        }
    }
}

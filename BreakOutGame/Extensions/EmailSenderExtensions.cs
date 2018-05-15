using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BreakOutGame.Services;

namespace BreakOutGame.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Bevestig je account door op dze link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a> te klikken!");
        }
    }
}

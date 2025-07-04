
using Helpers;
using System.Security.Cryptography;
using System.Text;
using BO;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Net.Mail;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace Helpers;
internal static class Tools
{
    private static readonly DalApi.IDal _dal = DalApi.Factory.Get; //stage 4
    private static readonly string apiUrl = "https://geocode.maps.co/search?q={0}&api_key={1}";


    /// <summary>
    /// Sends an email using an SMTP client.
    /// </summary>
    /// <param name="toEmail">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    //public static async Task SendEmail(string toEmail, string subject, string body)
    //{
    //    var fromAddress = new MailAddress("wesaveliveseveryday@gmail.com", "We_Save_Lives");
    //    var toAddress = new MailAddress(toEmail);

    //    var smtpClient = new SmtpClient("smtp.gmail.com")
    //    {
    //        Port = 587,
    //        Credentials = new NetworkCredential("wesaveliveseveryday@gmail.com", "yuul ttyh pvep birb"),
    //        EnableSsl = true,
    //    };

    //    using (var message = new MailMessage(fromAddress, toAddress)
    //    {
    //        Subject = subject,
    //        Body = body
    //    })
    //    {
    //        smtpClient.Send(message);
    //    }
    //}
    public static async Task SendEmail(string toEmail, string subject, string body)
    {
        var fromAddress = new MailAddress("wesaveliveseveryday@gmail.com", "We_Save_Lives");
        var toAddress = new MailAddress(toEmail);

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("wesaveliveseveryday@gmail.com", "yuul ttyh pvep birb"),
            EnableSsl = true,
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            // Use SendMailAsync to send the email asynchronously
            await smtpClient.SendMailAsync(message);
        }
    }

    public static string HashPassword(string password)
    {
        password = password.Trim();
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }

    /// <summary>
    /// Verifies if a password matches its hashed value.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="hashedPassword">The hashed password to compare against.</param>
    /// <returns>True if the password matches the hash, otherwise false.</returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var hash = HashPassword(password);
        return hash == hashedPassword||password==hashedPassword;
    }
}
   

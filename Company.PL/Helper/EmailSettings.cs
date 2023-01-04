using Company.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.ethereal.email", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("torrey48@ethereal.email", "kYB8zu1tJUC1BFzRsZ");
            client.Send("torrey48@ethereal.email", email.To, email.Title, email.Body);
        }
    }
}

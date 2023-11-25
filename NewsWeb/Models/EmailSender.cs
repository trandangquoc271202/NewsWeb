using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace NewsWeb.Models
{
    public class EmailSender
    {
        public void SendEmail(string toAddress, string subject, string body)
        {
            // Cấu hình SMTP client sử dụng máy chủ SMTP của Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Port thường là 587 khi sử dụng TLS
                Credentials = new NetworkCredential("sosinhsv1b@gmail.com", "vzfi pjff brob laox"),
                EnableSsl = true // Bật SSL
            };

            // Tạo thông điệp email
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("sosinhsv1b@gmail"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toAddress);

            // Gửi email
            smtpClient.Send(mailMessage);
        }
    }
}
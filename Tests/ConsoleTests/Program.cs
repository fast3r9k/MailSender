using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = new MailAddress("faster20092009@yandex.ru", "Никита");
            var to = new MailAddress("faster20092009@gmail.com", "Никита");
            var message = new MailMessage(from,to);
            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма + " + DateTime.Now;
            var client = new SmtpClient("smtp.yandex.ru", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "user_name",
                Password = "PassWord!"
            };

            client.Send(message);


        }
    }
}

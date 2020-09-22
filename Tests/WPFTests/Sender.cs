using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace WPFTests
{
    public static class Sender
    {
        static MailAddress from = new MailAddress("faster20092009@yandex.ru", "Никита");
        static MailAddress to = new MailAddress("faster20092009@gmail.com", "Никита");
        static MailMessage message = new MailMessage(from, to)
        {
            Subject = "Заголовок письма от " + DateTime.Now,
            Body = "Текст тестового письма " + DateTime.Now
        };
        public static MailAddress From
        {
            get {return from; }            
        }
        public static MailAddress To
        {
            get { return to; }
        }

        public static MailMessage Message
        {
            get { return message; }
        }

        static string host = "smtp.yandex.ru";
        static int port = 587;

        public static string Host
        {
            get { return host; }
        }
        public static int Port
        {
            get { return port; }
        }

    
    }
}

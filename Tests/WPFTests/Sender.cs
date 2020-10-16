using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace WPFTests
{
    public static class Sender
    {

            public static int port = 587;

            public static string host = "smtp.yandex.ru";

            public static string from_address = "faster20092009@yandex.ru";

            public static string from_name = "Никита";

            public static string to_address = "faster20092009@gmail.com";

            public static string to_name = "Nikita";

            public static string subject = "Тестовое письмо от " + DateTime.Now;

            //public static string user_name = UserNameEditor.Text;
            //public static securestring password = PasswordEditor.SecurePassword;


            public static string msg = "Привет, это тестовое письмо, отвечать на него вовсе не обязательно!) ";

            public static string okMessage = "Письмо успешно отправлена";

            public static string errorSubject = "Ошибка!";

    }
}

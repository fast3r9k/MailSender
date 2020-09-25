using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Windows;

namespace WPFTests
{
    public class EmailSendServiceClass
    {
        public EmailSendServiceClass()
        {
  
        }

        public static void sendSpam(string userName, SecureString password, string subject, string text)
        {
            using (var client = new SmtpClient(Sender.host, Sender.port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(userName, password);

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(Sender.from_address, Sender.from_name);
                    message.To.Add(new MailAddress(Sender.to_address, Sender.to_name));
                    message.Subject = subject;
                    message.Body = text;
                    try
                    {
                        client.Send(message);
                        MessageBox.Show(Sender.okMessage, "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Sender.errorSubject, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}

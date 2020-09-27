using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib
{
    public class MailSenderService
    {
        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public void SendMessage(string SenderAddress, string RecepientAddress, string Subject, string Body)
        {

            var from = new MailAddress(SenderAddress);
            var to = new MailAddress(RecepientAddress);
            using (var message = new MailMessage(from,to))
            {
                message.Subject = Subject;
                message.Body = Body;

                using (var client = new SmtpClient(ServerAddress, ServerPort))
                {
                    client.EnableSsl = UseSSL;
                    client.Credentials = new NetworkCredential(Login, Password);
                    try
                    {
                        client.Send(message);
                    }
                    catch(SmtpException ex)
                    {
                        Trace.TraceError(ex.ToString());
                        throw;
                    }
                }
            }
        }
    }
}

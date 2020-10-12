using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public class SmtpMailService : IMailService
    {
        public SmtpMailService()
        {

        }

        public IMailSender GetSender(string Server, int Port, bool SSL, string Login, string Password)
        {
            return new SmtpMailSender(Server, Port, SSL, Login, Password);
        }
    }

    internal class SmtpMailSender :IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _SSL;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpMailSender(string Address, int Port, bool SSL, string Login, string Password)
        {
            _Address = Address;
            _Port = Port;
            _SSL = SSL;
            _Login = Login;
            _Password = Password;
        }

        public void Send(string SenderAddress, string RecipientAddress, string Subject, string Body)
        {
            var from = new MailAddress(SenderAddress);
            var to = new MailAddress(RecipientAddress);
            using (var message = new MailMessage(from, to))
            {

                message.Subject = Subject;
                message.Body = Body;

                using (var client = new SmtpClient(_Address, _Port))
                {
                    client.EnableSsl = _SSL;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = _Login,
                        Password = _Password
                    };

                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }
        public void Send(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientAddresses)
                Send(SenderAddress, recipient_address, Subject, Body);
        }

        public async Task SendAsync(string SenderAddress, string RecipientAddress, string Subject, string Body, CancellationToken Cancel = default)
        {
            var from = new MailAddress(SenderAddress);
            var to = new MailAddress(RecipientAddress);
            using (var message = new MailMessage(from, to))
            {

                message.Subject = Subject;
                message.Body = Body;

                using (var client = new SmtpClient(_Address, _Port))
                {
                    client.EnableSsl = _SSL;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = _Login,
                        Password = _Password
                    };

                    try
                    {
                        Cancel.ThrowIfCancellationRequested();
                        await client.SendMailAsync(message).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }

        public async Task SendAsync(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body, IProgress<(string Recipient, double percent)> Progress = null, 
            CancellationToken Cancel = default)
        {
            var recipients = RecipientAddresses.ToArray();

            for (int i = 0, count = recipients.Length; i < count; i++)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendAsync(SenderAddress, recipients[i], Subject, Body, Cancel).ConfigureAwait(false);
                Progress?.Report((recipients[i], i / (double)count));
            }
          
        }

        public void SendParallel(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body)
        {

            foreach (var recipient_address in RecipientAddresses)
            {
                ThreadPool.QueueUserWorkItem(o => Send(SenderAddress, recipient_address, Subject, Body));
            }
        }

        public async Task SendParallelAsync(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body,
            CancellationToken Cancel = default)
        {
            var tasks = RecipientAddresses
                .Select(recipient_address => SendAsync(SenderAddress, recipient_address, Subject, Body, Cancel));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}

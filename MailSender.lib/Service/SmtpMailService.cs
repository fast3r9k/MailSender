﻿using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender.lib.Service
{
    public class SmtpMailService : IMailService
    {
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
            using var message = new MailMessage(from, to)
            {
                Subject = Subject,
                Body = Body
            };

            using (var client = new SmtpClient(_Address, _Port))
            {
                client.EnableSsl = _SSL;
                client.Credentials = new NetworkCredential(_Login, _Password);
                try
                {
                    client.Send(message); //NullException???
                }
                catch (SmtpException ex)
                {
                    Trace.TraceError(ex.ToString());
                    throw;
                }
            }
        }
    }
}

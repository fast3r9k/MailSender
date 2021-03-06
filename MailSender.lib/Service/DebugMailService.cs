﻿using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public class DebugMailService : IMailService
    {
        private readonly IEncryptorService _EncryptorService;
        public DebugMailService(IEncryptorService EncryptorService)
        {
            _EncryptorService = EncryptorService; 
        }
        public IMailSender GetSender(string Server, int Port, bool SSL, string Login, string Password)
        {
            return new DebugMailSender(Server, Port, SSL, Login, Password);
        }

        private class DebugMailSender : IMailSender
        {
            private readonly string _Address;
            private readonly int _Port;
            private readonly bool _SSL;
            private readonly string _Login;
            private readonly string _Password;

            public DebugMailSender(string Address, int Port, bool SSL, string Login, string Password)
            {
                _Address = Address;
                _Port = Port;
                _SSL= SSL;
                _Login = Login;
                _Password= Password;
            }
            public void Send(string SenderAddress, string RecipientAddress, string Subject, string Body)
            {
                Debug.WriteLine($"ОТправка почты через сервер {_Address}:{_Port} SSL:{_SSL}, Login:{_Login}, Password:{_Password}");
                Debug.WriteLine($"Сообщение от {SenderAddress} к {RecipientAddress}:\r\n{Subject}\r\n{Body}");
            }
            public void Send(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body)
            {
                foreach (var recipient_address in RecipientAddresses)
                    Send(SenderAddress, recipient_address, Subject, Body);
            }

            public Task SendAsync(string SenderAddress, string RecipientAddress, string Subject, string Body, CancellationToken Cancel = default)
            {
                throw new NotImplementedException();
            }

            public Task SendAsync(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body, IProgress<(string Recipient, double percent)> Progress = null, CancellationToken Cancel = default)
            {
                throw new NotImplementedException();
            }

            public void SendParallel(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body)
            {
                Send(SenderAddress, RecipientAddresses, Subject, Body);
            }

            public Task SendParallelAsync(string SenderAddress, IEnumerable<string> RecipientAddresses, string Subject, string Body, CancellationToken Cancel = default)
            {
                throw new NotImplementedException();
            }
        }
    }
     

}

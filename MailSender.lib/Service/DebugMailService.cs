using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MailSender.lib.Service
{
    public class DebugMailService : IMailService
    {
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
        }
    }
     

}

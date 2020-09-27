using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    class Server
    {
        public string Adress { get; set; }

        private int _Port;
        public int Port 
        { 
            get => _Port;
            set 
            {
                if (value < 0 || value >= 65535)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Номер порта должен находиться в диапазоне от 0 до 65535");
                _Port = value;
            } 
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public bool UseSSL { get; set; }
    }
}

using MailSender.lib.Models.Base;
using System;

namespace MailSender.lib.Models
{
    public class Recipient : Person
    {
        public override string Name 
        { 
            get => base.Name;
            set
            {
                if (value == "")
                    throw new ArgumentException("Имя должно быть заполнено", nameof(value));
                if (value == "QWE")
                    throw new ArgumentException("Запрещенное имя QWE", nameof(value));
                base.Name = value;
            }
        }
    }
}

using MailSender.lib.Models.Base;
using System;
using System.ComponentModel;

namespace MailSender.lib.Models
{
    public class Recipient : Person, IDataErrorInfo
    {
         string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch(PropertyName)
                {
                    default: return null;

                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Имя должно быть заполнено";
                        if (name.Length < 2) return "Имя должно содержать не менее 2 символов";
                        if (name.Length > 20) return "Имя должно содержать не более 20 символов";
                        return null;
                    case nameof(Address):
                        return null;
                }
            }
        }

        public override string Name 
        { 
            get => base.Name;
            set
            {
                base.Name = value;
            }
        }

         string IDataErrorInfo.Error { get; } = null;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    class MainViewModel:ViewModel
    {
        private string _Title = "MailSender";
        public string Title
        {
            get => _Title;
            set 
            {
                if (_Title == value) return;
                _Title = value;
                OnPropertyChanged("MainWindow");
            }
        }

    }
}

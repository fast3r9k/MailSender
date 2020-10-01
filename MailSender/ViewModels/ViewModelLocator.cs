using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.ViewModels
{
    class ViewModelLocator
    {
        public MainViewModel MainWindowModel => App.Services.GetRequiredService<MainViewModel>();
    }
}

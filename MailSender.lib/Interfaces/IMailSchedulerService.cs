using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Interfaces
{
    public interface IMailSchedulerService
    {
        void Start();

        void Stop();
    }
}

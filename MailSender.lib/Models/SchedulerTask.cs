﻿using MailSender.lib.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Models
{
    public class SchedulerTask : Entity
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender{ get; set; }
        public Message Message{ get; set; }
        public ICollection<Recipient> Recipients { get; set; }

    }
}

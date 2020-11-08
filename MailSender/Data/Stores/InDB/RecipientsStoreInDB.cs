﻿using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data.Stores.InDB
{
    class RecipientsStoreInDB : StoreInDB<Recipient>
    {
        public RecipientsStoreInDB(MailSenderDB db) : base(db) {}
    }
    class MessagesStoreInDB : StoreInDB<Message>
    {
        public MessagesStoreInDB(MailSenderDB db) : base(db) { }
    }
    class SendersStoreInDB : StoreInDB<Sender>
    {
        public SendersStoreInDB(MailSenderDB db) : base(db) { }
    }
    class SchedulerTasksStoreInDB : StoreInDB<SchedulerTask>
    {
        public SchedulerTasksStoreInDB(MailSenderDB db) : base(db) { }
    } 
    class ServersStoreInDB : StoreInDB<Server>
    {
        public ServersStoreInDB(MailSenderDB db) : base(db) { }
    }
}

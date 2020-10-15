using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MailSender.Data
{
    class MailSenderDbInitialiser
    {
        private readonly MailSenderDB _db;
        public MailSenderDbInitialiser(MailSenderDB db){ _db = db; }
        public void Initialise()
        {
            _db.Database.Migrate();
            InitialiseRecipient();
            InitialiseSender();
            InitialiseServer();
            InitialiseMessage();
        }

        private void InitialiseRecipient()
        {
            if (_db.Recipients.Any()) return;
            _db.Recipients.AddRange(TestData.Recipients);
            _db.SaveChanges();
        }

        private void InitialiseSender()
        {
            if (_db.Senders.Any()) return;
            _db.Senders.AddRange(TestData.Senders);
            _db.SaveChanges();
        }
        private void InitialiseServer()
        {
            if (_db.Servers.Any()) return;
            _db.Servers.AddRange(TestData.Servers);
            _db.SaveChanges();
        }
        private void InitialiseMessage()
        {
            if (_db.Messages.Any()) return;
            _db.Messages.AddRange(TestData.Messages);
            _db.SaveChanges();
        }
    }
}

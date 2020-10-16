using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InDB
{
    class MessagesStoreInDB : IStore<Message>
    {
        private readonly MailSenderDB _db;
        public MessagesStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public Message Add(Message Item)
        {
            _db.Entry(Item).State = EntityState.Added;
            _db.SaveChanges();
            return Item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public IEnumerable<Message> GetAll() => _db.Messages.ToArray();

        public Message GetById(int Id) => _db.Messages.SingleOrDefault(r => r.Id == Id);

        public void Update(Message item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

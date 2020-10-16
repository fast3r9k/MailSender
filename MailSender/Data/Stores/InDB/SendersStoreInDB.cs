using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InDB
{
    class SendersStoreInDB : IStore<Sender>
    {
        private readonly MailSenderDB _db;
        public SendersStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public Sender Add(Sender Item)
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

        public IEnumerable<Sender> GetAll() => _db.Senders.ToArray();

        public Sender GetById(int Id) => _db.Senders.SingleOrDefault(r => r.Id == Id);

        public void Update(Sender item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

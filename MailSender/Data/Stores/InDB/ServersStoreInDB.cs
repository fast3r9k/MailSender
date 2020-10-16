using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InDB
{
    class ServersStoreInDB : IStore<Server>
    {
        private readonly MailSenderDB _db;
        public ServersStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public Server Add(Server Item)
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

        public IEnumerable<Server> GetAll() => _db.Servers.ToArray();

        public Server GetById(int Id) => _db.Servers.SingleOrDefault(r => r.Id == Id);

        public void Update(Server item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

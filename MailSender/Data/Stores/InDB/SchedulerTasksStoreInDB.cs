using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InDB
{
    class SchedulerTasksStoreInDB : IStore<SchedulerTask>
    {
        private readonly MailSenderDB _db;
        public SchedulerTasksStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public SchedulerTask Add(SchedulerTask Item)
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

        public IEnumerable<SchedulerTask> GetAll() => _db.SchedulerTasks.ToArray();

        public SchedulerTask GetById(int Id) => _db.SchedulerTasks.SingleOrDefault(r => r.Id == Id);

        public void Update(SchedulerTask item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

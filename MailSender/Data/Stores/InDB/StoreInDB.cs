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
    class StoreInDB<T> : IStore<T> where T: Entity
    {
        private readonly MailSenderDB _db;
        private readonly DbSet<T> _Set;
        public StoreInDB(MailSenderDB db)
        {
            _db = db;
            _Set = db.Set<T>();
        }
        public T Add(T Item)
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

        public IEnumerable<T> GetAll() => _Set.ToArray();

        public T GetById(int Id) => _Set.SingleOrDefault(r => r.Id == Id);

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

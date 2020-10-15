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
    class RecipientsStoreInDB : IStore<Recipient>
    {
        private readonly MailSenderDB _db;
        public RecipientsStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public Recipient Add(Recipient Item)
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

        public IEnumerable<Recipient> GetAll() => _db.Recipients.ToArray();

        public Recipient GetById(int Id) => _db.Recipients.SingleOrDefault(r => r.Id == Id);

        public void Update(Recipient item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

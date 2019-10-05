using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Repositories
{
    public abstract class Repository<TValue, TKey> where TValue : class, IEntity<TKey>
    {
        ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext context)
        {
            dbContext = context;
        }
        private DbSet<TValue>Items => dbContext.Set<TValue>();

        public TValue Get(TKey id)
        {
            return Items.Find(id);
        }

        public List<TValue> GetAll()
        {
            return Items.ToList();
        }
        public void Remove(TKey id)
        {
            var item = Items.Find(id);
            Items.Remove(item);
        }

        public void Add(TValue item)
        {
            Items.Add(item);
        }

        public void Edit(TValue item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
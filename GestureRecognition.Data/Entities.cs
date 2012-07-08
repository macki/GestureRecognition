using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using GestureRecognition.Data.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace GestureRecognition.Data
{
    public class Entities  : DbContext, IDataContext
    {
        public DbSet<Records> Records { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }

        public int SaveChange()
        {
            return SaveChanges();
        }

        public T Add<T>(T entity) where T : class
        {
            return Set<T>().Add(entity);
        }

        public T Delete<T>(T entity) where T : class
        {
            return Set<T>().Remove(entity);
        }

        public T Attach<T>(T entity) where T : class
        {
            var entry = Entry(entity);
            entry.State = System.Data.EntityState.Modified;
            return entity;
        }
    }
}

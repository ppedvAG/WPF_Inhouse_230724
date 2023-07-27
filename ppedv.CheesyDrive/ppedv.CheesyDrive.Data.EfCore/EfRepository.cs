using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.Model.DomainModel;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ppedv.CheesyDrive.Data.EfCore
{
    public class EfRepository : IRepository
    {
        private EfContext context;
        public EfRepository(string conString)
        {
            context = new EfContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return context.Set<T>();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            context.Set<T>().AddOrUpdate(entity);
        }
    }
}

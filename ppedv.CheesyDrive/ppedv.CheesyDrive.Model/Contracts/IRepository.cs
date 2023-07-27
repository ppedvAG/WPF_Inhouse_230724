using ppedv.CheesyDrive.Model.DomainModel;
using System.Collections.Generic;
using System.Security;

namespace ppedv.CheesyDrive.Model.Contracts
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;

        IEnumerable<T> GetAll<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;

        void Save();
    }
}

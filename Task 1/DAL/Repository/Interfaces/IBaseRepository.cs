using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Find(int id);
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void Update(T item);
        void Delete(T item);
    }
}

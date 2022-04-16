using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IBaseService<T> where T : BaseDTO
    {
        IEnumerable<T> GetAll();
        T Find(int id);
        void Add(T item);
        void AddRange(IEnumerable<T> item);
        void Update(T item);
        void Delete(T item);
    }
}

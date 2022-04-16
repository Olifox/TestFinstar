using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public abstract class BaseService<T, D> : IBaseService<T> where T : BaseDTO where D : BaseEntity
    {
        protected readonly IBaseRepository<D> _repository;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepository<D> repository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<D, T>();
                cfg.CreateMap<T, D>();
            });
            _mapper = config.CreateMapper();

            _repository = repository;
        }

        public virtual void Add(T item)
        {
            _repository.Add(_mapper.Map<T, D>(item));
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            _repository.AddRange(_mapper
                .Map<IEnumerable<T>, IEnumerable<D>>(items));
        }

        public virtual void Delete(T item)
        {
            _repository.Delete(_mapper.Map<T, D>(item));
        }

        public virtual T Find(int id)
        {
            return _mapper.Map<D, T>(_repository.Find(id));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _mapper
                .Map<IEnumerable<D>, IEnumerable<T>>(_repository.GetAll());
        }

        public virtual void Update(T item)
        {
            _repository.Update(_mapper.Map<T, D>(item));
        }
    }
}

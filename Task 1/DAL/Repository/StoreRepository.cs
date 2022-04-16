using DAL.Common;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace DAL.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(AppDbContext context) : base(context)
        {
        }

        public override void AddRange(IEnumerable<Store> entities)
        {
            foreach (var entity in _entities)
            {
                _entities.Remove(entity);
            }
            _entities.AddRangeAsync(entities);
            _context.SaveChanges();
        }
    }
}

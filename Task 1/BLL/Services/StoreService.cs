using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class StoreService : BaseService<StoreDTO, Store>, IStoreService
    {
        public StoreService(IStoreRepository repository) : base(repository)
        {
        }
    }
}

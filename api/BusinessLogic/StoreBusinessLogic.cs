using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class StoreBusinessLogic : IStoreBusinessLogic
    {
        private readonly StoreDataAccess _storeDataAccess;

        public StoreBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _storeDataAccess = new StoreDataAccess(connectionString);
        }

        public List<Store> GetStoresByOwnerId(int ownerId)
        {
            return _storeDataAccess.GetStoresByOwnerId(ownerId);
        }
    }
}

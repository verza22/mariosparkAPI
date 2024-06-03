using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IStoreBusinessLogic
    {
        List<Store> GetStoresByOwnerId(int ownerId);
    }
}

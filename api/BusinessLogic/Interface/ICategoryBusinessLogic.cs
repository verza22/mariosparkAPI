using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface ICategoryBusinessLogic
    {
        List<Category> GetCategoriesByStoreId(int storeID);
    }
}

using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface ICategoryBusinessLogic
    {
        List<Category> GetCategoriesByStoreId(int storeID);
        Category GetCategory(int id);
        bool RemoveCategory(int categoryId);
        int AddOrUpdateCategory(int categoryID, string name, string image, int storeID);
    }
}

using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class CategoryBusinessLogic : ICategoryBusinessLogic
    {
        private readonly CategoryDataAccess _categoryDataAccess;

        public CategoryBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _categoryDataAccess = new CategoryDataAccess(connectionString);
        }

        public List<Category> GetCategoriesByStoreId(int storeID)
        {
            return _categoryDataAccess.GetCategoriesByStoreId(storeID);
        }
    }
}

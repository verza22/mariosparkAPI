using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class ProductBusinessLogic : IProductBusinessLogic
    {
        private readonly ProductDataAccess _productDataAccess;

        public ProductBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _productDataAccess = new ProductDataAccess(connectionString);
        }

        public List<Product> GetProducts(int storeID, int categoryID)
        {
            return _productDataAccess.GetProducts(storeID, categoryID);
        }
    }
}

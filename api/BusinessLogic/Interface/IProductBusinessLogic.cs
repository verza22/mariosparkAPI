using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IProductBusinessLogic
    {
        List<Product> GetProducts(int storeID, int categoryID);
    }
}

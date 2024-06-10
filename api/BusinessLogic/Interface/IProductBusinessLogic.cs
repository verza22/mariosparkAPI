using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IProductBusinessLogic
    {
        List<Product> GetProducts(int storeID, int categoryID);
        Product GetProduct(int id);
        bool RemoveProduct(int productId);
        bool AddOrUpdateProduct(Product product);
    }
}

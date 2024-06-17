using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IOrderBusinessLogic
    {
        List<Order> GetOrders(int store_id);
        int InsertOrder(Order order);
    }
}

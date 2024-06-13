using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class OrderBusinessLogic : IOrderBusinessLogic
    {
        private readonly OrderDataAccess _orderDataAccess;

        public OrderBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _orderDataAccess = new OrderDataAccess(connectionString);
        }

        public List<Order> GetOrders(int store_id)
        {
            return _orderDataAccess.GetOrders(store_id);
        }

        public bool InsertOrder(Order order)
        {
            return _orderDataAccess.InsertOrder(order);
        }
    }
}

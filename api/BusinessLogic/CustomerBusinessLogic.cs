using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;

namespace api.BusinessLogic
{
    public class CustomerBusinessLogic : ICustomerBusinessLogic
    {
        private readonly CustomerDataAccess _customerDataAccess;

        public CustomerBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _customerDataAccess = new CustomerDataAccess(connectionString);
        }

        public List<Customer> GetCustomers(int storeID)
        {
            return _customerDataAccess.GetCustomers(storeID);
        }
    }
}

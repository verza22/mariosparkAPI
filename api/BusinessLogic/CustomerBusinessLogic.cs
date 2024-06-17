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
        public Customer GetCustomer(int customerID)
        {
            return _customerDataAccess.GetCustomer(customerID);
        }
        public bool RemoveCustomer(int customerID)
        {
            return _customerDataAccess.RemoveCustomer(customerID);
        }
        public int AddOrUpdateCustomer(Customer customer)
        {
            return _customerDataAccess.AddOrUpdateCustomer(customer);
        }
    }
}

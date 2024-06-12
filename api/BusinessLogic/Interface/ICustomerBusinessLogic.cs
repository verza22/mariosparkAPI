using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface ICustomerBusinessLogic
    {
        List<Customer> GetCustomers(int storeID);
        Customer GetCustomer(int customerID);
        bool RemoveCustomer(int customerID);
        bool AddOrUpdateCustomer(Customer customer);
    }
}

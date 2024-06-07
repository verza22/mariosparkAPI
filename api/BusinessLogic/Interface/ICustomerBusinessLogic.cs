using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface ICustomerBusinessLogic
    {
        List<Customer> GetCustomers(int storeID);
    }
}

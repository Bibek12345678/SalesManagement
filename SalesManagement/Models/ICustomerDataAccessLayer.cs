using System.Collections.Generic;

namespace SalesManagement.Models
{
    public interface ICustomerDataAccessLayer
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(int? id);
        IEnumerable<Customer> GetAllCustomer();
        Customer GetCustomerById(int? id);
        void UpdateCustomer(Customer customer);
    }
}
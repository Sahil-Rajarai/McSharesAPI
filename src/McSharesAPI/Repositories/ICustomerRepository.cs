using McSharesAPI.Models;
using System.Collections.Generic;

namespace McSharesAPI.Repositories
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer cust);   
        IEnumerable<Customer> CreateCustomers(List<Customer> customersList);   
        IEnumerable<Customer> GetAllCustomer();   
        Customer GetCustomerById(string Id);   
        Customer UpdateCustomer(Customer customerToUpdate, CustomerEntity customerEntity);   
        Customer DeleteCustomer(string Id);
        IEnumerable<Customer> SearchCustomerByName(string name);
    }
}
using McSharesAPI.Models;
using System.Collections.Generic;

namespace McSharesAPI.Repository
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer cust);   
        Dictionary<string, Customer> CreateCustomers(List<Customer> customersList);   
        Dictionary<string, Customer> GetAllCustomer();   
        Customer GetCustomerById(string Id);   
        Customer UpdateCustomer(Customer customerToUpdate, CustomerEntity customerEntity);   
        Customer DeleteCustomer(string Id);
        IEnumerable<Customer> SearchCustomerByName(string name);
    }
}
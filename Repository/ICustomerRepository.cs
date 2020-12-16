using McSharesAPI.Models;
using System.Collections.Generic;

namespace McSharesAPI.Repository
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer cust);   
        List<Customer> GetAllCustomer();   
        Customer GetCustomerById(string Id);   
        Customer UpdateCustomer(string Id);   
        Customer DeleteCustomer(string Id);   
    }
}
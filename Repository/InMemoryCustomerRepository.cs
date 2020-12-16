using McSharesAPI.Repository;
using McSharesAPI.Models;
using System.Collections.Generic;

namespace McSharesAPI.Repository
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        public Dictionary<string, Customer> IdToCustomerList = new Dictionary<string, Customer>();
        public List<Customer> CustomerList = new List<Customer>();

        public Customer CreateCustomer(Customer cust)
        {
            return null;
        }
        public List<Customer> GetAllCustomer()
        {
            Customer c = new Customer 
            {
                CustomerId = "1",
                CustomerType = "test",
                RegistrationNo = "134"
            };

            CustomerList.Add(c);

            return CustomerList;
        }
        public Customer GetCustomerById(string Id)
        {
            return null;
        }
        public Customer UpdateCustomer(string Id)
        {
            return null;
        }
        public Customer DeleteCustomer(string Id)
        {
            return null;
        }
        
    }
}
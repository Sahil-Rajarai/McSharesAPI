using McSharesAPI.Repository;
using McSharesAPI.Models;
using System.Collections.Generic;

namespace McSharesAPI.Repository
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        public Dictionary<string, Customer> IdToCustomerList = new Dictionary<string, Customer>();
        public Customer CreateCustomer(Customer cust)
        {
            return null;
        }

        public Dictionary<string, Customer> CreateCustomers(List<Customer> customersList)
        {
            foreach(Customer cust in customersList)
            {
                IdToCustomerList.Add(cust.CustomerId, cust);
            }
            
            return IdToCustomerList;
        }
        public Dictionary<string, Customer> GetAllCustomer()
        {
            return IdToCustomerList;
        }
        public Customer GetCustomerById(string Id)
        {
            return IdToCustomerList.ContainsKey(Id) ? IdToCustomerList[Id] : null;
        }
        public Customer UpdateCustomer(Customer customerToUpdate, CustomerEntity customerEntity)
        {
            customerToUpdate.Contacts.ContactName = customerEntity.CustomerName;
            customerToUpdate.DateOfBirth = customerEntity.DateOfBirth;
            customerToUpdate.DateIncorp = customerEntity.DateIncorp;
            customerToUpdate.Shares.NumShares = customerEntity.NumShares.ToString();
            customerToUpdate.Shares.SharePrice = customerEntity.SharePrice.ToString();

            return customerToUpdate;
        }
        public Customer DeleteCustomer(string Id)
        {
            return null;
        }
        
    }
}
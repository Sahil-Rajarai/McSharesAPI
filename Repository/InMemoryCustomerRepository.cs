using McSharesAPI.Repository;
using McSharesAPI.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace McSharesAPI.Repository
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        public Dictionary<string, Customer> IdToCustomerDictionary = new Dictionary<string, Customer>();
        public Customer CreateCustomer(Customer cust)
        {
            return null;
        }

        public Dictionary<string, Customer> CreateCustomers(List<Customer> customersList)
        {
            foreach(Customer cust in customersList)
            {
                try
                {
                    IdToCustomerDictionary.Add(cust.CustomerId, cust);
                }
                catch(Exception e)
                {
                    return new Dictionary<string, Customer>
                    {
                        { e.Message, null }
                    };
                }
            }
            
            return IdToCustomerDictionary;
        }
        public Dictionary<string, Customer> GetAllCustomer()
        {
            return IdToCustomerDictionary;
        }
        public Customer GetCustomerById(string Id)
        {
            return IdToCustomerDictionary.ContainsKey(Id) ? IdToCustomerDictionary[Id] : null;
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

        public IEnumerable<Customer> SearchCustomerByName(string name)
        {
            return IdToCustomerDictionary.Values.Where(customer => customer.Contacts.ContactName.ToLower().Contains(name));
        }
        
    }
}
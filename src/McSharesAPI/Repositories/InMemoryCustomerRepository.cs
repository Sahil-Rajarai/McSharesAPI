using McSharesAPI.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace McSharesAPI.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        // dictionary which will contain all the valid customers retrieved from the XML file
        private Dictionary<string, Customer> _customerDictionary = new Dictionary<string, Customer>();
        
        // not implemented as not part of requirements
        public Customer CreateCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> CreateCustomers(List<Customer> customersList)
        {
            foreach(Customer cust in customersList)
            {
                _customerDictionary.Add(cust.CustomerId, cust);
            }
            
            return _customerDictionary.Values.ToList().AsReadOnly();
        }

        public Customer GetCustomerById(string Id)
        {
            return _customerDictionary.ContainsKey(Id) ? _customerDictionary[Id] : null;
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
            return _customerDictionary.Values.Where(customer => customer.Contacts.ContactName.ToLower().Contains(name));
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return _customerDictionary.Values.ToList().AsReadOnly();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using McSharesAPI.Repository;
using McSharesAPI.Models;

namespace McSharesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public List<Customer> Get() =>
            _customerRepository.GetAllCustomer();

        // [HttpPost]
        // // public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        // // {
        // //     _inMemoryCustomerRepository.IdToCustomerList.Add(customer.CustomerId, customer);
        // //     await _inMemoryCustomerRepository.SaveChangesAsync();

        // //     return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        // // }
    }
}
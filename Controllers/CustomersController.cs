using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using McSharesAPI.Repository;
using McSharesAPI.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using McSharesAPI.Services;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;

namespace McSharesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private ILoggerRepository _logger;
        public CustomersController(ICustomerRepository customerRepository, ILoggerRepository logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        // GET: api/Customer/details
        [HttpGet("details")]
        public Dictionary<string, Customer> Get() =>
            _customerRepository.GetAllCustomer();

        
        // GET: api/Customer/5
        [HttpGet("{id}/details")]
        public ActionResult<Customer> GetCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                var error = StaticVariables.errorCustomerNotFound;
                _logger.LogError(error, DateTime.Now);
                return NotFound(error);
            }

            return Ok(customer);
        }

        // GET: api/Customer
        [HttpGet]
        public List<CustomerEntity> GetAllCustomerEntity()
        {
            List<CustomerEntity> customerEntityList = new List<CustomerEntity>();

            foreach(Customer currentCust in _customerRepository.GetAllCustomer().Values)
            {
                var custEntity = CustomerMapper.ConvertCustomerToCustomerEntity(currentCust);
                customerEntityList.Add(custEntity);
            }

            return customerEntityList;
        }

        // GET: api/Customers
        [HttpGet("{id}")]
        public ActionResult<CustomerEntity> GetCustomerEntityById(String Id)
        {
            var customer = _customerRepository.GetCustomerById(Id);

            if (customer == null)
            {
                var error = StaticVariables.errorCustomerNotFound;
                _logger.LogError(error, DateTime.Now);
                return NotFound(error);
                
            }

            return Ok(CustomerMapper.ConvertCustomerToCustomerEntity(customer));
        }
            
        [HttpPost("upload")]
        public async Task<IActionResult> UploadXMLFile([FromForm] IFormFile file)
        {
            var xmlData = Path.Combine(Environment.CurrentDirectory, "data");
            Directory.CreateDirectory(xmlData);
            var path = Path.Combine(xmlData, file.FileName);

            using (var fileStream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(fileStream);
            }

            var xml = new XmlDocument();
            xml.Load(path);
            List<Customer> customers =  CustomerService.ValidateFile(xml);
            var createdCustomers = _customerRepository.CreateCustomers(customers);
           
           if(createdCustomers.Values.First() is null)
           {
                var error = createdCustomers.Keys.First();
                _logger.LogError(error, DateTime.Now);
                return BadRequest(error);
           }

            return Created(nameof(Customer), createdCustomers);

        }

         // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<CustomerEntity> PutCustomer(string id, CustomerEntity customerEntity)
        {
            Customer cust = _customerRepository.GetCustomerById(id);

            if (cust == null)
            {
                var error = StaticVariables.errorIncorrectId;
                _logger.LogError(error, DateTime.Now);
                return BadRequest(error);
            }

            if(!String.IsNullOrEmpty(customerEntity.CustomerId) && id != customerEntity.CustomerId)
            {
                var error = StaticVariables.errorNotAmendableId;
                _logger.LogError(error, DateTime.Now);
                return BadRequest(error);
            }

            if(customerEntity.CustomerType == "Corporate" && customerEntity.NumShares.ToString() != cust.Shares.NumShares)
            {
                var error = StaticVariables.errorNumShares;
                _logger.LogError(error, DateTime.Now);
                return BadRequest(error);
            }

            var customer = _customerRepository.UpdateCustomer(cust, customerEntity);

            return Ok(CustomerMapper.ConvertCustomerToCustomerEntity(customer));
        }

        // GET: api/Customers
        [HttpGet("search")]
        public ActionResult<Customer> GetCustomersByName([FromQuery] string name)
        {
            var customerList = _customerRepository.SearchCustomerByName(name.ToLower());
            var customerEntityList = new List<CustomerEntity>();

            if(!customerList.Any())
            {
                var error = StaticVariables.errorNoCustomersFoundName;
                _logger.LogError(error, DateTime.Now);
                return NotFound(error);
            }

            foreach(Customer currentCust in customerList)
            {
                var custEntity = CustomerMapper.ConvertCustomerToCustomerEntity(currentCust);
                customerEntityList.Add(custEntity);
            }

            return Ok(customerEntityList);
        }

         // GET: api/Customers/export
        [HttpGet("export")]
        public ActionResult GetCSVFile()
        {
            var customerEntityList = new List<CustomerEntity>();

            foreach(Customer currentCust in _customerRepository.GetAllCustomer().Values)
            {
                var custEntity = CustomerMapper.ConvertCustomerToCustomerEntity(currentCust);
                customerEntityList.Add(custEntity);
            }

            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            
            try
            {
                if(customerEntityList.Any())
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var sw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true)))
                        {
                            using (var cw = new CsvWriter(sw, cc))
                            {
                                cw.WriteRecords(customerEntityList);
                            }
                            
                            return File(ms.ToArray(), "text/csv", "Reports.csv");
                        }
                    }
                }

                var error = StaticVariables.errorNoCustomersFound;
                _logger.LogError(error, DateTime.Now);
                return NotFound(error);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, DateTime.Now);
                return BadRequest(e.Message);
            }
           
        }

    }
}
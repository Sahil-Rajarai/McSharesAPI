using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using McSharesAPI.Repositories;
using McSharesAPI.Models;
using System.IO;
using System.Xml;
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

        // GET: api/Customers/details
        // Get all customers with their fields
        [HttpGet("details")]
        public IEnumerable<Customer> GetAllCustomers() =>
            _customerRepository.GetAllCustomer();
        
        // GET: api/Customers/5/details
        // Get a specific customers with all their fields
        [HttpGet("{id}/details")]
        public ActionResult<Customer> GetCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                var error = ErrorMessages.CustomerNotFound;
                _logger.LogError(error);
                return NotFound(error);
            }

            return Ok(customer);
        }

        // GET: api/Customers
        // Get all customers with only specific fields - CustomerEntity object
        [HttpGet]
        public List<CustomerEntity> GetAllCustomersEntity()
        {
            List<CustomerEntity> customerEntityList = new List<CustomerEntity>();

            foreach(var currentCust in _customerRepository.GetAllCustomer())
            {
                var custEntity = CustomerMapper.ConvertCustomerToCustomerEntity(currentCust);
                customerEntityList.Add(custEntity);
            }

            return customerEntityList;
        }

        // GET: api/Customers/5
        // Get a specific customer with only specific fields - CustomerEntity object
        [HttpGet("{id}")]
        public ActionResult<CustomerEntity> GetCustomerEntityById(string Id)
        {
            var customer = _customerRepository.GetCustomerById(Id);

            if (customer == null)
            {
                var error = ErrorMessages.CustomerNotFound;
                _logger.LogError(error);
                return NotFound(error);
                
            }

            return Ok(CustomerMapper.ConvertCustomerToCustomerEntity(customer));
        }
        
        // POST: api/Customers/upload
        // processes the XML file and save the valid customers in the Db
        [HttpPost("upload")]
        public async Task<IActionResult> UploadXmlFile([FromForm] IFormFile file)
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
            var customers =  CustomerService.ValidateFile(xml);
            
            try
            {
                var createdCustomers = _customerRepository.CreateCustomers(customers);
                return Created(nameof(Customer), createdCustomers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Conflict(ex.Message);
            }
        }

        // PUT: api/Customers/5
        //update customer with new values in JSON object (deserialize to CustomerEntity)
        [HttpPut("{id}")]
        public ActionResult<CustomerEntity> PutCustomer(string id, CustomerEntity customerEntity)
        {
            Customer cust = _customerRepository.GetCustomerById(id);

            if (cust == null)
            {
                var error = ErrorMessages.IncorrectId;
                _logger.LogError(error);
                return NotFound(error); // not found
            }

            if(!string.IsNullOrEmpty(customerEntity.CustomerId) && id != customerEntity.CustomerId)
            {
                var error = ErrorMessages.NotAmendableId;
                _logger.LogError(error);
                return BadRequest(error);
            }

            if(customerEntity.CustomerType == CustomerTypes.Corporate && customerEntity.NumShares.ToString() != cust.Shares.NumShares)
            {
                var error = ErrorMessages.NumShares;
                _logger.LogError(error);
                return BadRequest(error);
            }

            var customer = _customerRepository.UpdateCustomer(cust, customerEntity);

            return Ok(CustomerMapper.ConvertCustomerToCustomerEntity(customer));
        }

        // GET: api/Customers/search?name="sa"
        // search customers
        //get a list of customers whose names are similar to the one passed as params
        [HttpGet("search")]
        public ActionResult<CustomerEntity> GetCustomersByName([FromQuery] string name)
        {
            var customerList = _customerRepository.SearchCustomerByName(name.ToLower());
            var customerEntityList = new List<CustomerEntity>();

            if(!customerList.Any())
            {
                var error = ErrorMessages.NoCustomersFoundName;
                _logger.LogError(error);
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
        // populate CSV file with CustomerEntity object using CsvHelper package and return file
        [HttpGet("export")]
        public ActionResult GetCsvFile()
        {
            var customerEntityList = new List<CustomerEntity>();

            foreach(var currentCust in _customerRepository.GetAllCustomer())
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

                var error = ErrorMessages.NoCustomersFound;
                _logger.LogError(error);
                return NotFound(error);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
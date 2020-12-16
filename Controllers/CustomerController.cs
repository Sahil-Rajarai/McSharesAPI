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
using Microsoft.AspNetCore.Http;

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
        public Dictionary<string, Customer> Get() =>
            _customerRepository.GetAllCustomer();

        
        // GET: api/Customer/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
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
            List<Customer> customers = ValidateFile(xml);
            var createdCustomers = _customerRepository.CreateCustomers(customers);
           
            return Ok(createdCustomers);
        }

        public List<Customer> ValidateFile(XmlDocument xmlFile) 
        {
            List<Customer> customerList = new List<Customer>();
            var xmlNodes = xmlFile.SelectNodes("RequestDoc/Doc_Data/DataItem_Customer");
            XmlSerializer serial = new XmlSerializer(typeof(Customer));

            foreach (XmlNode node in xmlNodes)
            {
                Customer currentCust =(Customer)serial.Deserialize(new XmlNodeReader(node));
                
                if(!String.IsNullOrEmpty(currentCust.Shares.NumShares) && !String.IsNullOrEmpty(currentCust.Shares.SharePrice))
                {  
                    if(int.Parse(currentCust.Shares.NumShares) > 0 && IsSharePriceValid(currentCust.Shares.SharePrice))
                    {
                        if(currentCust.CustomerType != "Individual" || (!String.IsNullOrEmpty(currentCust.DateOfBirth) && currentCust.CustomerType == "Individual" &&  CalculateAge(currentCust.DateOfBirth) >= 18))
                        {
                            customerList.Add(currentCust);
                        }

                    }
                }
            }
          
            return customerList;
        }

        public int CalculateAge(String xmlDob) 
        {
            DateTime dob = DateTime.Parse(xmlDob);
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - dob.Year;
            // Go back to the year in which the person was born in case of a leap year
            if (dob.Date > today.AddYears(-age)) age--;

            return age;

        }

        public bool IsSharePriceValid(String xmlSharePrice) 
        {
            int numDecimals = xmlSharePrice.Substring(xmlSharePrice.LastIndexOf('.') + 1).Length; 
            double sharePrice = double.Parse(xmlSharePrice);

            if(numDecimals == 2 && sharePrice > 0)
            {
                return true;
            }

            return false;
        }
    }
}
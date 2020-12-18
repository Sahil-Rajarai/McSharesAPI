using McSharesAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;
using System.Xml.Serialization;

namespace McSharesAPI.Services
{
    public class CustomerService
    {   
        //Validate xmlFile as per the rules given
        public static List<Customer> ValidateFile(XmlDocument xmlFile) 
        {
            List<Customer> customerList = new List<Customer>();
            var xmlNodes = xmlFile.SelectNodes("RequestDoc/Doc_Data/DataItem_Customer");
            XmlSerializer serial = new XmlSerializer(typeof(Customer));

            foreach(XmlNode node in xmlNodes)
            {
                Customer currentCust =(Customer)serial.Deserialize(new XmlNodeReader(node));
                
                if(!String.IsNullOrEmpty(currentCust.Shares.NumShares) && !String.IsNullOrEmpty(currentCust.Shares.SharePrice))
                {  
                    if(IsInteger(currentCust.Shares.NumShares) && int.Parse(currentCust.Shares.NumShares) > 0 && IsSharePriceValid(currentCust.Shares.SharePrice))
                    {
                        if(currentCust.CustomerType != CustomerTypes.Individual || (!String.IsNullOrEmpty(currentCust.DateOfBirth) && currentCust.CustomerType == CustomerTypes.Individual &&  CalculateAge(currentCust.DateOfBirth) >= 18))
                        {
                            customerList.Add(currentCust);
                        }
                    }
                }
            }
          
            return customerList;
        }

        public static bool IsSharePriceValid(String xmlSharePrice) 
        {
            int numDecimals = xmlSharePrice.Substring(xmlSharePrice.LastIndexOf('.') + 1).Length; 
            double sharePrice = double.Parse(xmlSharePrice);

            if(numDecimals == 2 && sharePrice > 0)
            {
                return true;
            }

            return false;
        }

        public static int CalculateAge(String xmlDob) 
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

        public static bool IsInteger(String number)
        {
            try 
            {
                int.Parse(number);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
using System;
using System.Xml.Serialization;

namespace McSharesAPI.Models
{
    [XmlRoot(ElementName="DataItem_Customer")]
    public class Customer
    {
        [XmlElement("customer_id")]
        public string CustomerId { get; set; }
        [XmlElement("Customer_Type")]
        public string CustomerType { get; set; }

        [XmlElement("Date_Of_Birth")]
        public String DateOfBirth { get; set; }

        [XmlElement("Date_Incorp")]
        public String DateIncorp { get; set; }

        [XmlElement("REGISTRATION_NO")]
        public string RegistrationNo { get; set; }

        [XmlElement("Mailing_Address")]
        public MailingAddress MailingAddr { get; set; }

        [XmlElement("Contact_Details")]
        public ContactDetails Contacts { get; set; }

        [XmlElement("Shares_Details")]
        public SharesDetails Shares { get; set; }
    }

}
using System;
using System.Xml.Serialization;

namespace McSharesAPI.Models
{
    [XmlRoot(ElementName="DataItem_Customer")]
    public class Customer
    {
        [System.Xml.Serialization.XmlElement("customer_id")]
        public string CustomerId { get; set; }
        [System.Xml.Serialization.XmlElement("Customer_Type")]
        public string CustomerType { get; set; }

        [System.Xml.Serialization.XmlElement("Date_Of_Birth")]
        public String DateOfBirth { get; set; }

        [System.Xml.Serialization.XmlElement("Date_Incorp")]
        public String DateIncorp { get; set; }

        [System.Xml.Serialization.XmlElement("REGISTRATION_NO")]
        public string RegistrationNo { get; set; }

        [System.Xml.Serialization.XmlElement("Mailing_Address")]
        public MailingAddress MailingAddr { get; set; }

        [System.Xml.Serialization.XmlElement("Contact_Details")]
        public ContactDetails Contacts { get; set; }

        [System.Xml.Serialization.XmlElement("Shares_Details")]
        public SharesDetails Shares { get; set; }
    }
}
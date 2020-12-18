using System.Xml.Serialization;

namespace McSharesAPI.Models
{
    public class ContactDetails
    {
        [XmlElement("Contact_Name")]
        public string ContactName { get; set; }

        [XmlElement("Contact_Number")]
        public string ContactNumber { get; set; } 
    }
}
namespace McSharesAPI.Models
{
    public class ContactDetails
    {
        [System.Xml.Serialization.XmlElement("Contact_Name")]
        public string ContactName { get; set; }

        [System.Xml.Serialization.XmlElement("Contact_Number")]
        public string ContactNumber { get; set; } 
    }
}
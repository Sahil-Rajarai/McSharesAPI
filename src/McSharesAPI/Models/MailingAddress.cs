using System.Xml.Serialization;

namespace McSharesAPI.Models
{
    public class MailingAddress
    {
        [XmlElement("Address_Line1")]
        public string AddressLine1 { get; set; }

        [XmlElement("Address_Line2")]
        public string AddressLine2 { get; set; }

        [XmlElement("Town_City")]
        public string TownCity { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }

    }
}
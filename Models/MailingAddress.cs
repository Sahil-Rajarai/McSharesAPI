namespace McSharesAPI.Models
{
    public class MailingAddress
    {
        [System.Xml.Serialization.XmlElement("Address_Line1")]
        public string AddressLine1 { get; set; }

        [System.Xml.Serialization.XmlElement("Address_Line2")]
        public string AddressLine2 { get; set; }

        [System.Xml.Serialization.XmlElement("Town_City")]
        public string TownCity { get; set; }

        [System.Xml.Serialization.XmlElement("Country")]
        public string Country { get; set; }

    }
}
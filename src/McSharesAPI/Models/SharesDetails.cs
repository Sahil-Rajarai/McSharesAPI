using System.Xml.Serialization;

namespace McSharesAPI.Models
{
    public class SharesDetails
    {
        [XmlElement("Num_Shares")]
        public string NumShares { get; set; }

        [XmlElement("Share_Price")]
        public string SharePrice { get; set; }
    
    }
}
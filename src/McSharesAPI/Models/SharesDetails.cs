using Microsoft.EntityFrameworkCore;

namespace McSharesAPI.Models
{
    public class SharesDetails
    {
        [System.Xml.Serialization.XmlElement("Num_Shares")]
        public string NumShares { get; set; }

        [System.Xml.Serialization.XmlElement("Share_Price")]
        public string SharePrice { get; set; }
    
    }
}
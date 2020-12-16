using Microsoft.EntityFrameworkCore;

namespace McSharesAPI.Models
{
    public class SharesDetails
    {
        public string Shares_Id { get; set; }
        public string NumShares { get; set; }
        public string SharePrice { get; set; }
    
    }
}
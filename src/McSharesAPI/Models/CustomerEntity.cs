namespace McSharesAPI.Models
{
    public class CustomerEntity
    {   
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateIncorp { get; set; }
        public string CustomerType { get; set; }
        public double NumShares { get; set; }
        public double SharePrice { get; set; }
        public double Balance => NumShares * SharePrice;
    }
}
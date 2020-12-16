using System;

namespace McSharesAPI.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CustomerType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateIncorp { get; set; }
        public string RegistrationNo { get; set; }
        public MailingAddress MailingAddr { get; set; }
        public ContactDetails Contacts { get; set; }
        public SharesDetails Shares { get; set; }
    }
}
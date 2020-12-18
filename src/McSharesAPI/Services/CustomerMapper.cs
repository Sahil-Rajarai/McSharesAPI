using McSharesAPI.Models;

namespace McSharesAPI
{
    public class CustomerMapper
    {
        public static CustomerEntity ConvertCustomerToCustomerEntity(Customer customer)
        {
            var custEntity = new CustomerEntity
            {
                CustomerId =  customer.CustomerId,
                CustomerName =  customer.Contacts.ContactName,
                DateOfBirth =  customer.DateOfBirth,
                DateIncorp =  customer.DateIncorp,
                CustomerType =  customer.CustomerType,
                NumShares =  double.Parse(customer.Shares.NumShares),
                SharePrice =  double.Parse(customer.Shares.SharePrice)
            };

            custEntity.Balance = custEntity.NumShares * custEntity.SharePrice;

            return custEntity;
        }
    }
}
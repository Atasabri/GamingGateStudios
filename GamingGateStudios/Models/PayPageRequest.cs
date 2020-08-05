using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingGateStudios.Models
{
    public class PayPageRequest
    {
        public string MerchantEmail { get; set; }
        public string SecretKey { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string SiteUrl { get; set; }
        public string Title { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string ProductsPerTitle { get; set; }
        public string ReturnUrl { get; set; }
        public string CcFirstNname { get; set; }
        public string CcLastName { get; set; }
        public string CcPhoneNumber { get; set; }
        public string Phonenumber { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string AddressShipping { get; set; }
        public string CityShipping { get; set; }
        public string StateShipping { get; set; }
        public string PostalCodeShipping { get; set; }
        public string CountryShipping { get; set; }
        public string PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
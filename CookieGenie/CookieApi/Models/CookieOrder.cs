using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieGenie.CookieApi.Models
{
    public class CookieOrder
    {
        public Contact Contact { get; set; }

        [JsonProperty("delivery_contact")]
        public Contact DeliveryContact { get; set; }
        
        public List<CookieQty> Cookies { get; set; }

        public string Source { get; set; } // SOCIAL

        public string Type { get; set; } // GIRLDELIVERY

        public bool Paid { get; set; }

        public int Id { get; set; }

        [JsonProperty("order_number")]
        public int OrderNumber { get; set; }

        [JsonProperty("order_date")]
        public DateTime? OrderDate { get; set; }
        
        [JsonProperty("last_updated_date")]
        public DateTime LastUpdatedDate { get; set; }

        [JsonProperty("paid_date")]
        public DateTime? PaidDate { get; set; }

        public float Amount { get; set; }
        
        public int Packages { get; set; }
        
        public string Status { get; set; } // "ORDERED",
        
        [JsonProperty("direct_ship_number")]
        public string DirectShipNumber { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; } // "CreditCard",

        [JsonProperty("payment_method_id")]
        public int? PaymentMethodId { get; set; }

        [JsonProperty("payment_declined_date")]
        public DateTime? PaymentDeclinedDate { get; set; }
        
        public bool Refunded { get; set; }

        public bool Approved { get; set; }

        [JsonProperty("marked_donation")]
        public bool MarkedDonation { get; set; }
    }
}

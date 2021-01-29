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
        {
        "source": "SOCIAL",
        "type": "GIRLDELIVERY",
        "paid": true,
        "id": 407246,
        "order_number": 407246,
        "order_date": "2021-01-21T10:09:17.6989509",
        "last_updated_date": "2021-01-21T10:18:38.5356643",
        "paid_date": "2021-01-21T10:18:38.5356643",
        "amount": 48,
        "packages": 12,
        "status": "ORDERED",
        "direct_ship_number": null,
        "transaction_number": "1233989676",
        "payment_method": "CreditCard",
        "payment_method_id": 3,
        "payment_declined_date": null,
        "refunded": false,
        "approved": true,
        "marked_donation": false
    },
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CookieGenie.CookieApi.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}

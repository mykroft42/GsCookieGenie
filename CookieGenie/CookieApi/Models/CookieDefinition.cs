using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieGenie.CookieApi.Models
{
    public class CookieDefinition
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        [JsonProperty("foot_note")]
        public string FootNote { get; set; }
        
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        public string Image { get; set; }

        public string Color { get; set; }

        [JsonProperty("unit_cost")]
        public decimal UnitCost { get; set; }

        [JsonProperty("packages_per_case")]
        public int PackagesPerCase { get; set; }

        public string Description { get; set; }
        
        [JsonProperty("nutrition_info")]
        public string NutritionInfo { get; set; }

        public int Sequence { get; set; }

        [JsonProperty("available_for_initial_order")]
        public bool AvailableForInitialOrder { get; set; }

        [JsonProperty("available_for_planned_order")]
        public bool AvailableForPlannedOrder { get; set; }

        [JsonProperty("available_for_restock_order")]
        public bool AvailableForRestockOrder { get; set; }

        [JsonProperty("available_online")]
        public bool AvailableOnline { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("cookie_share")]
        public bool CookieShare { get; set; }
    }
}

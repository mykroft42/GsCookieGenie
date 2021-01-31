using CookieGenie.CookieApi;
using CookieGenie.CookieApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace CookieGenie
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CookieClient api = new CookieClient();

            await api.Login("***", "***");

            List<CookieDefinition> def = await api.GetCookieData();

            List<CookieOrder> orders = await api.GetOrderData();

            //var result = (from cookieQty in orders.Where(o => o.Type == "GIRLDELIVERY" && (o.OrderDate ?? DateTime.MinValue) < new DateTime(2021, 1, 25)).SelectMany(o => o.Cookies)
            //              group cookieQty by cookieQty.Id into qtys
            //              select new { CookieId = qtys.Key, Total = qtys.Sum(q => q.Quantity) }).ToList();

            var result = orders.Where(o => o.Type == "GIRLDELIVERY" && o.DeliveryContact.Address != null).OrderBy(o => o.DeliveryContact?.Address?.City).ThenBy(o => o.DeliveryContact?.Address?.Zip).ToList();

            StringBuilder builder = new StringBuilder();

            foreach (var res in result)
            {
                //Console.WriteLine($"Type: {def.SingleOrDefault(d => d.Id == res.CookieId)?.Name}, Total: {res.Total}");
                builder.AppendLine("--------------------------------------------");
                builder.AppendLine($"{res.DeliveryContact.FirstName} {res.DeliveryContact.LastName}");
                builder.AppendLine($"Ordered on {res.OrderDate?.ToString("g")}");
                builder.Append($"{res.DeliveryContact.Address.Street}");
                if (!string.IsNullOrEmpty(res.DeliveryContact.Address.Suite))
                    builder.Append($" Suite: {res.DeliveryContact.Address.Suite}");
                builder.Append("\r\n");
                builder.AppendLine($"{res.DeliveryContact.Address.City}, {res.DeliveryContact.Address.State} {res.DeliveryContact.Address.Zip}");
                builder.AppendLine("--------------------------------------------");
            }

            Console.WriteLine(builder.ToString());
            System.IO.File.WriteAllText("deliveryList.txt", builder.ToString());
        }
    }
}

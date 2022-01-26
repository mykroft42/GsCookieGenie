using CookieGenie.CookieApi;
using CookieGenie.CookieApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using CommandLine;

namespace CookieGenie
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GenieOptions options = null;
            await Parser.Default.ParseArguments<GenieOptions>(args)
                .WithParsedAsync(PerformAction);
        }
        
        static async Task PerformAction(GenieOptions options)
        { 
            CookieClient api = new CookieClient();

            await api.Login(options.Username, options.Password);

            List<CookieDefinition> def = await api.GetCookieData();

            List<CookieOrder> orders = await api.GetOrderData();

            var result1 = (from cookieQty in orders.Where(o => o.Type == "GIRLDELIVERY" && (o.OrderDate ?? DateTime.MinValue) > new DateTime(2022, 1, 1)).SelectMany(o => o.Cookies)
                          group cookieQty by cookieQty.Id into qtys
                          select new { CookieId = qtys.Key, Total = qtys.Sum(q => q.Quantity) }).ToList();

            var result2 = orders.Where(o => o.Type == "GIRLDELIVERY" && o.DeliveryContact.Address != null && o.Status == "ORDERED" && o.OrderDate > DateTime.Parse("1/1/2022 8:43 PM")).OrderBy(o => o.OrderDate); 
            //result2 = result2.OrderBy(o => o.DeliveryContact?.Address?.City).ThenBy(o => o.DeliveryContact?.Address?.Zip);

            StringBuilder builder = new StringBuilder();

            foreach (var res in result1)
            {
                Console.WriteLine($"Type: {def.SingleOrDefault(d => d.Id == res.CookieId)?.Name}, Total: {res.Total}");
            }

            foreach (var res in result2)
            {
                builder.AppendLine("--------------------------------------------");
                builder.AppendLine($"{res.DeliveryContact.FirstName} {res.DeliveryContact.LastName}");
                builder.AppendLine($"Ordered on {res.OrderDate?.ToString("g")}");
                builder.AppendLine($"Status: {res.Status}");
                builder.Append($"{res.DeliveryContact.Address.Street}");
                if (!string.IsNullOrEmpty(res.DeliveryContact.Address.Suite))
                    builder.Append($" Suite: {res.DeliveryContact.Address.Suite}");
                builder.Append("\r\n");
                builder.AppendLine($"{res.DeliveryContact.Address.City}, {res.DeliveryContact.Address.State} {res.DeliveryContact.Address.Zip}");
                foreach (var qty in res.Cookies)
                {
                    builder.AppendLine($"\tType: {def.SingleOrDefault(def => def.Id == qty.Id)?.Name}\t{qty.Quantity}");
                }
                builder.AppendLine("--------------------------------------------");
            }

            var result3 = (from qty in result2.SelectMany(o => o.Cookies)
                            group qty by qty.Id into qtys
                            select new { CookieId = qtys.Key, Total = qtys.Sum(q => q.Quantity) }).ToList();

            builder.AppendLine("--------------------------------------------");
            foreach (var qty1 in result3)
            {
                builder.AppendLine($"Type: {def.SingleOrDefault(d => d.Id == qty1.CookieId)?.Name}, Total: {qty1.Total}");
            }
            builder.AppendLine("--------------------------------------------");

            Console.WriteLine(builder.ToString());
            System.IO.File.WriteAllText("deliveryList.txt", builder.ToString());
        }
    }
}

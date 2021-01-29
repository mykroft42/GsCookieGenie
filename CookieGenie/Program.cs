using CookieGenie.CookieApi;
using System;
using System.Threading.Tasks;

namespace CookieGenie
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CookieClient api = new CookieClient();

            await api.Login("***", "***");

            await api.GetCookieData();

            await api.GetOrderData();
        }
    }
}

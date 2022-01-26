using CookieGenie.CookieApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CookieGenie.CookieApi
{
    class CookieClient : IDisposable
    {
        private HttpClient _client { get; set; }
        private RestClient _restClient { get; set; }
        private int GirlId { get; set; }

        private const string BASE_URL = "https://app.abcsmartcookies.com/webapi/api";
        public CookieClient()
        {
            _client = new HttpClient();
            _restClient = new RestClient(BASE_URL);
        }

        private const string LOGIN_URL = @"https://app.abcsmartcookies.com/webapi/api/account/login";
        public async Task Login(string username, string password)
        {
            var request = new RestRequest("account/login", Method.Post);
            request.AddJsonBody(new LoginRequest { Username = username, Password = password });
            RestResponse response = await _restClient.ExecuteAsync(request);
            
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Failed to login as user: {username} returned status code: {response.StatusCode}");

            foreach(Cookie cookie in response.Cookies)
            {
                _restClient.CookieContainer.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
            }
        }

        private const string ME_URL = @"https://app.abcsmartcookies.com/webapi/api/me";
        private readonly string COOKIE_URL = $"{ME_URL}/cookies";
        public async Task<List<CookieDefinition>> GetCookieData()
        {
            var request = new RestRequest("me", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException("Failed to load me URL");

            JObject obj = JObject.Parse(response.Content);
            GirlId = (int)obj["role"]["girl_id"];

            request = new RestRequest("me/cookies", Method.Get);
            response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException("Failed to load cookie data");

            List<CookieDefinition> cookieDefinitions = JsonConvert.DeserializeObject<List<CookieDefinition>>(response.Content);

            return cookieDefinitions;
        }

        public async Task<List<CookieOrder>> GetOrderData()
        {
            var request = new RestRequest("girl-orders", Method.Get);
            request.AddParameter("details", "full");
            request.AddParameter("girl_id", GirlId);

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException("Failed to get order data");

            List<CookieOrder> results = JsonConvert.DeserializeObject<List<CookieOrder>>(response.Content);

            return results;
        }

        #region IDisposable

        private void Dispose(bool disposing)
        {
            if (disposing)
                GC.SuppressFinalize(this);

            _client.Dispose();
        }

        public void Dispose() => Dispose(true);

        ~CookieClient() => Dispose(false);

        #endregion IDisposable
    }
}

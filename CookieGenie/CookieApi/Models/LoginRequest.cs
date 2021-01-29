using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieGenie.CookieApi.Models
{
    class LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}

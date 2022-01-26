using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace CookieGenie
{
    internal class GenieOptions
    {
        [Option('u', "username", Required = true, HelpText = "Username for the cookie app")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password for the cookie app")]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ITMeat.BusinessLogic.Configuration.Implementations
{
    public class EmailServiceCredentials
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
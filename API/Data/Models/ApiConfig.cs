using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Models
{
    public class ApiConfig
    {
        public string Secret { get; set; }
        public string Secret2 { get; set; }
        public int ExpirationHoursCredentials { get; set; }
    }
}

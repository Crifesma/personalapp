using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Models
{
    public class PermissionInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Granted { get; set; }
    }
}

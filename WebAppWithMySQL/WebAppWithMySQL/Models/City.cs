using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithMySQL.Models
{
    public class City
    {
        public int city_id { get; set; }
        public string city { get; set; }
        public int country_id { get; set; }
        public DateTime last_update { get; set; }
    }
}
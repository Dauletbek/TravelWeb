using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Role
    {
        public int ID { get; set; }
        public String name { get; set; }
        public virtual ICollection<User> user { get; set; }
    }
}
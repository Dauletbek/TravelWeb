using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class TravelType
    {
        public int TravelTypeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Travel> Travels { get; set; }
    }
}
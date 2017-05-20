using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Hotel
    {
        public int hotelID { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public int room { get; set; }
        public int maxPerson { get; set; }
        public double price { get; set; }
        public int TravelID { get; set; }
        public virtual Travel travel { get; set; }
        public virtual ICollection<Order> order { get; set; }
    }
}
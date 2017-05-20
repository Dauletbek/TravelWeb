using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public double totalPrice { get; set; }
        public int userID { get; set; }
        public int hotelID { get; set; }
        public virtual User user { get; set; }
        public virtual Hotel hotel { get; set; }
        public virtual ICollection<Payment> payment { get; set; }

    }
}
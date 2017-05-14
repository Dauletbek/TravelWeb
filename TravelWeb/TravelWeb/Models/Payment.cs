using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Payment
    {
        public int paymentID { get; set; }
        public double balance { get; set; }
        public double totalPaid{get;set;}
        public int orderID { get; set; }
        public virtual Order order { get; set; }
        public virtual ICollection<PaymentHistory> historyes { get; set; }
    }
}
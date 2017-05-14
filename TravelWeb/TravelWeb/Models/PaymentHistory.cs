using System;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public enum PaidType {
        CASH,BANK,ONLINE,CARD
    }
    public class PaymentHistory
    {
        public int ID { get; set; }
        public double paidPrice { get; set; }
        public PaidType paidType { get; set; }
        public DateTime date { get; set; }
        public int paymentID { get; set; }
        public virtual Payment payment { get; set; }
    }
}
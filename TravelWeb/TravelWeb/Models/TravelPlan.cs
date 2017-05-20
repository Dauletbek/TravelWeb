using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class TravelPlan
    {
        [Key]
        public int ID { get; set; }
        public double price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string detail { get; set; }
        public int TravelID { get; set; }
        public virtual Travel travel { get; set; }
        public virtual ICollection<Day> Days { get; set; }
    }
}
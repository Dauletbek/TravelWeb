using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Day
    {
        public int dayID { get; set; }
        public int day { get; set; }
        public string detail { get; set; }
        public int travelPlanID { get; set; }
        public virtual TravelPlan travelPlan { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Travel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StartFrom { get; set; }
        public string EndTo { get; set; }
        public int TravelTypeID { get; set; }
        public int guiderID { get; set; }
        public virtual TravelType TravelType { get; set; }
        public virtual Guider guider { get; set; }
        public virtual ICollection<TravelPlan> travelPlan { get; set; }
        public virtual ICollection<Image> images { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        public virtual ICollection<Hotel> hotels { get; set; }

    }
}
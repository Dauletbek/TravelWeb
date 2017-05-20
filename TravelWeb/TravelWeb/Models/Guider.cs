using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class Guider
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int workedYear { get; set; }
        public string img { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}
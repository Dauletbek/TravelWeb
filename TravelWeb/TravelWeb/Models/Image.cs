using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class Image
    {
        
        public int imageID { get; set; }

        public string source { get; set; }
        public int TravelID { get; set; }
       // [Required(ErrorMessage = "Please Select Image file")]
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        public virtual Travel travel { get; set; }
       

    }
}
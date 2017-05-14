using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Comment
    {
        public int commentID { get; set; }
        public string detail { get; set; }
        public DateTime createdDate { get; set; }
        public int TravelID { get; set; }
        public int userID { get; set; }
        public virtual Travel travel { get; set; }
        public virtual User user { get; set; }
    }
}
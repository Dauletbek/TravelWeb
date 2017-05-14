using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public enum Gender {
        MALE,FEMALE
    }
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string regNo { get; set; }
        public Gender? gender { get; set; }
        public DateTime birthDate { get; set; }
        public int roleID { get; set; }
        public virtual Role role { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        public virtual ICollection<Order> orders { get; set; }

    }
}
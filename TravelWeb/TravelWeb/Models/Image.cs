﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class Image
    {
        public int imageID { get; set; }
        public string source { get; set; }
        public int TravelID { get; set; }
        public virtual Travel travel { get; set; }
    }
}
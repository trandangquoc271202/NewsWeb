using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Models
{
    public class Favorite
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public String Link { get; set; }
        public string ImagePath { get; set; }
        public DateTime Time { get; set; }

        // Khóa ngoại cho UserID
        public virtual Users User { get; set; }

        // Khóa ngoại cho NewsIDa

    }
}
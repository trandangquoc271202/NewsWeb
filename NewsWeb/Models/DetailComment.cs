using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeb.Models
{
    public class DetailComment
    {
        public int id { get; set; }
        public string nameUser { get; set; }
        public int idUser { get; set; }
        public string message { get; set; }
    }
}
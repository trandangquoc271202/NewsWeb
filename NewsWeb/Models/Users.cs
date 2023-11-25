namespace NewsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public int id { get; set; }

        [StringLength(40)]
        public string name { get; set; }

        [Required]
        [StringLength(40)]
        public string email { get; set; }

        [Required]
        [StringLength(40)]
        public string password { get; set; }

        [Required]
        [StringLength(10)]
        public string permission { get; set; }

        public bool status { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(40)]
        public string address { get; set; }

        [StringLength(40)]
        public string birthDay { get; set; }
    }
}

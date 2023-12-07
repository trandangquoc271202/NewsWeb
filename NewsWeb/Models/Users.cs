namespace NewsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(40)]
        public string name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string email { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public string password { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string permission { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool status { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(40)]
        public string address { get; set; }

        [StringLength(40)]
        public string birthDay { get; set; }
    }
}

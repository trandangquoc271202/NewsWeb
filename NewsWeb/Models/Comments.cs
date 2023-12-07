namespace NewsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int idUser { get; set; }

        [Required]
        public string link { get; set; }

        [Required]
        public string message { get; set; }

        public DateTime dateTimeComment { get; set; }
    }
}

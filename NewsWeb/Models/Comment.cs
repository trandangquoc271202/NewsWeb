namespace NewsWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int idUser { get; set; }

        [Required]
        public string link { get; set; }

        [Required]
        public string message { get; set; }

        [Required]
        public DateTime dateTimeComment { get; set; }
    }
}

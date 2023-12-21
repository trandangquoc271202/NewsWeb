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
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự.")]
        [RegularExpression(@"^(?=.*[!@#$%^&*])(?=.*[A-Z]).*$", ErrorMessage = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt và một chữ cái hoa.")]
        public string password { get; set; }

        [Required]
        [StringLength(10)]
        public string permission { get; set; }

        public bool status { get; set; }

        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string phone { get; set; }


        [StringLength(40)]
        public string address { get; set; }

        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Ngày sinh không hợp lệ.")]
        public string birthDay { get; set; }
    }
}

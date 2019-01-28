using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagemant.ViewModels
{
    public class PasswordConform
    {
        [Required(ErrorMessage ="this field is required")]
        public string Password { get; set; }
        [Required(ErrorMessage ="This field is Required")]
        [Compare("Password",ErrorMessage ="the password must match")]
        public string ConformPassword { get; set; }
    }
}
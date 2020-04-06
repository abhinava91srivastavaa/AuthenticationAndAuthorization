using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAndAuthorization.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "User Name")]
        [RegularExpression(".+@.+\\.[a-z]+", ErrorMessage = "Must be a valid EmailID")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required !")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginServiceReference.Login login { get; set; }

        public LoginViewModel()
        {
            login = new LoginServiceReference.Login();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            login = new LoginServiceReference.Login();
            user = new LoginServiceReference.User();
        }

        [Required(ErrorMessage = "Name is required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lastname is required !")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Country is required !")]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "Phone is required !")]
        [RegularExpression(@"^\d{8}|00\d{10}|\+\d{2}\d{8}", ErrorMessage = "Invalid Phone Number !")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required !")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required !")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage= "Password must be atleast 6 characters !")]
        public string Password { get; set; }

        public LoginServiceReference.Login login { get; set; }
        public LoginServiceReference.User user { get; set; }

    }
}
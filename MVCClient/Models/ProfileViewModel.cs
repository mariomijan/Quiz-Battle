using MVCClient.UserReference;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }

        [Required(ErrorMessage = "Name is required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lastname is required !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Country is required !")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required !")]
        [RegularExpression(@"^\d{8}|00\d{10}|\+\d{2}\d{8}", ErrorMessage = "Invalid Phone Number !")]
        public string Phone { get; set; }

    }
}
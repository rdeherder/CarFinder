using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.BlazorApp.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "Emailadres vereist.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is vereist.")]
        public string Password { get; set; }
    }
}

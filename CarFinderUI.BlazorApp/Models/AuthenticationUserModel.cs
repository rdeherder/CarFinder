using System.ComponentModel.DataAnnotations;

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

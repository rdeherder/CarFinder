using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarFinderUI.Library.Models
{
    public class CreateUserModel
    {
        [Required]
        [DisplayName("Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Achternaam")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Emailadres")]
        public string EmailAddress { get; set; }

        // Hier komt nog een wachtwoord-eis attribute bij
        [Required]
        [DisplayName("Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "De wachtwoorden komen niet overeen.")]
        [DisplayName("Bevestig wachtwoord")]
        public string ConfirmPassword { get; set; }
    }
}

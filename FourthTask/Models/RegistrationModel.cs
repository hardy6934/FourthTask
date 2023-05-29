using System.ComponentModel.DataAnnotations;

namespace FourthTask.Models
{
    public class RegistrationModel
    { 

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }

        [Required]
        public string Name { get; set; } 
    }
}

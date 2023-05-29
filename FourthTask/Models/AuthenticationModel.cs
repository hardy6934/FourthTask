using System.ComponentModel.DataAnnotations;

namespace FourthTask.Models
{
    public class AuthenticationModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

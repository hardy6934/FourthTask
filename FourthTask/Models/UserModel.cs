using FourthTask.Core.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace FourthTask.Models
{
    public class UserModel
    {
        public int Id { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Name { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLoginDate { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
         
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}

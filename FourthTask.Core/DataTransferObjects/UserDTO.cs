using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.Core.DataTransferObjects
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLoginDate { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
}

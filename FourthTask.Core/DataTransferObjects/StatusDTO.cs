using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.Core.DataTransferObjects
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public List<UserDTO> UsersDTO { get; set; }
    }
}

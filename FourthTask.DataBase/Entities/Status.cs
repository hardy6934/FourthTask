using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.DataBase.Entities
{
    public class Status : IBaseEntity
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public List<User> Users { get; set; }
    }
}

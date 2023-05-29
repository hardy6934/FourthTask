using FourthTask.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.DataBase
{
    public class FourthTaskContext : DbContext
    {
         public DbSet<User> Users { get; set; }
         public DbSet<Status> Statuses { get; set; }


        public FourthTaskContext(DbContextOptions<FourthTaskContext> options)
            : base(options)
        {

        }
    }
}

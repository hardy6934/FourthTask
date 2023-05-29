using FourthTask.Data.Repositories;
using FourthTask.Data.Repositories.Abstractions;
using FourthTask.DataBase;
using FourthTask.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.Data.Abstractions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FourthTaskContext context;
        public IRepository<User> Users { get; set; }
        public IRepository<Status> Status { get; set; }

        public UnitOfWork(FourthTaskContext context, IRepository<User> Users, IRepository<Status> Status) 
        { 
            this.context = context;
            this.Users = Users;
            this.Status = Status;
        }

        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }
    }
}

using FourthTask.Data.Repositories.Abstractions;
using FourthTask.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Status> Status { get; }

        Task<int> Commit();
    }
}

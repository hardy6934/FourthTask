using AutoMapper;
using FourthTask.Core.Abstractions;
using FourthTask.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthTask.Buisness.Services
{
    public class StatusService : IStatusService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public StatusService(IMapper mapper, IUnitOfWork unitOfWork) {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> GetStatusByName(string name)
        {
            var statusId =  unitOfWork.Status.Get().FirstOrDefault(x => x.StatusName.Equals(name)).Id;
            return   statusId;
        }
    }
}

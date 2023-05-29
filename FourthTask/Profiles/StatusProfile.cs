using AutoMapper;
using FourthTask.Core.DataTransferObjects;
using FourthTask.DataBase.Entities;
using FourthTask.Models;

namespace FourthTask.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDTO>();
            CreateMap<StatusDTO, Status>();


            CreateMap<StatusDTO, StatusModel>();
            CreateMap<StatusModel, StatusDTO>();

        }
    }
}

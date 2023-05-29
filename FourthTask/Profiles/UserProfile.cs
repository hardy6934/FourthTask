using AutoMapper;
using FourthTask.Core.DataTransferObjects;
using FourthTask.DataBase.Entities;
using FourthTask.Models;

namespace FourthTask.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
             CreateMap<User, UserDTO >().ForMember(dto => dto.StatusName, opt => opt.MapFrom(acc => acc.status.StatusName)); 
            CreateMap<UserDTO, User>();


            CreateMap<UserDTO, UserModel>();
            CreateMap<UserModel, UserDTO>();

            CreateMap<AuthenticationModel, UserDTO>();
            CreateMap<RegistrationModel, UserDTO>();

            CreateMap<UserDTO, UserShortDataModel>();
        }
    }
}

using AutoMapper;
using FourthTask.Core.Abstractions;
using FourthTask.Core.DataTransferObjects;
using FourthTask.Data.Repositories;
using FourthTask.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FourthTask.Buisness.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStatusService statusService;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IStatusService statusService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.statusService = statusService;
        }

        public async Task<int> CreateUserAsync(UserDTO dto)
        {
            try
            {
                dto.Password = CreateMd5(dto.Password); 
                await unitOfWork.Users.AddAsync(mapper.Map<User>(dto));

                return await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EditUserAsync(UserDTO dto)
        {
            try
            {
                unitOfWork.Users.Update(mapper.Map<User>(dto));
                await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EditUsersLastLoginDateAsync(UserDTO dto)
        {
            try
            {
                dto.LastLoginDate = DateTime.Now.ToString();
                unitOfWork.Users.Update(mapper.Map<User>(dto));
                await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task UpdateUserRangeAsync(List<UserDTO> dtos)
        {
            try
            {
                foreach (var dto in dtos)
                {
                    dto.Password = unitOfWork.Users.Get().AsNoTracking().FirstOrDefault(x => x.Id.Equals(dto.Id)).Password;
                }
                unitOfWork.Users.UpdateRange(dtos.Select(x=>mapper.Map<User>(x)));
                await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsUserExistAsync(UserDTO dto)
        {
            try
            {
                dto.Password = CreateMd5(dto.Password);
                return await unitOfWork.Users.Get().AnyAsync(x => x.Equals(mapper.Map<User>(dto)));
            }
            catch (Exception)
            {
                throw;
            }
        }

         


        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var User = await unitOfWork.Users.GetByIdAsync(id);
                return mapper.Map<UserDTO>(User);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetIdUserByEmailAsync(string email)
        {
            try
            {
                var UserId = (await unitOfWork.Users.Get().AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email))).Id;
                return UserId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                var User = unitOfWork.Users.Get().Where(x => x.Email.Equals(email)).FirstOrDefault();

                if (User == null)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RemoveUserAsync(UserDTO dto)
        {
            try
            {
                unitOfWork.Users.Remove(mapper.Map<User>(dto));
                await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RemoveRangeUserAsync(List<UserDTO> dtos)
        {
            try
            {
                unitOfWork.Users.RemoveRange(dtos.Select(x=>mapper.Map<User>(x)).ToList());
                await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> CheckUserPassword(UserDTO dto)
        {
            try
            {
                var dbPasswordHash = (await unitOfWork.Users.Get().FirstOrDefaultAsync(x => x.Email.Equals(dto.Email)))?.Password;

                if (dbPasswordHash != null && CreateMd5(dto.Password).Equals(dbPasswordHash))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        public async Task<bool> CheckUserStatus(UserDTO dto)
        {
            try
            {  
                var statusId = (await unitOfWork.Users.Get().FirstOrDefaultAsync(x => x.Email.Equals(dto.Email)))?.StatusId;
                var statusName =(await unitOfWork.Status.Get().FirstOrDefaultAsync(x => x.Id.Equals(statusId)))?.StatusName;
                if (statusName == "Active")
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<int> UpdateUserPasswordAsync(UserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            try
            {
                var User = await unitOfWork.Users.Get().AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email));
                return  mapper.Map<UserDTO>(User);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserDTO> GetUserWithIncludesByEmailAsync(string email)
        {
            try
            {
                var user = await unitOfWork.Users.FindBy(us => us.Email.Equals(email), user => user.status, us => us.status).AsNoTracking().FirstOrDefaultAsync();
                return mapper.Map<UserDTO>(user); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetAllUsersWithIncluds()
        {
            try
            {
                var user =  await unitOfWork.Users.Get().AsNoTracking().Select(x => x).Include(x => x.status).ToListAsync();
                return user.Select(x=>mapper.Map<UserDTO>(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
         

        private string CreateMd5(string password)
        {
            try
            {
                var passwordSalt = "qwe";

                using MD5 md5 = MD5.Create();
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
                var hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}

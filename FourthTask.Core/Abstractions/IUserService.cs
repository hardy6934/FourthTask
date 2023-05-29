using FourthTask.Core.DataTransferObjects;

namespace FourthTask.Core.Abstractions
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(UserDTO dto);

        bool IsEmailExist(string email);

        Task EditUserAsync(UserDTO dto);
        Task UpdateUserRangeAsync(List<UserDTO> dtos);
        Task EditUsersLastLoginDateAsync(UserDTO dto);
        Task<bool> CheckUserStatus(UserDTO dto);
        Task RemoveUserAsync(UserDTO dto);

        Task<bool> IsUserExistAsync(UserDTO dto);

        Task<bool> CheckUserPassword(UserDTO dto);

        Task<UserDTO> GetUserByIdAsync(int id);

        Task<int> GetIdUserByEmailAsync(string email);
        Task<UserDTO> GetUserByEmailAsync(string email);

        Task<int> UpdateUserPasswordAsync(UserDTO dto);
        Task<UserDTO> GetUserWithIncludesByEmailAsync(string email);
        Task<List<UserDTO>> GetAllUsersWithIncluds();
        
    }
}
    
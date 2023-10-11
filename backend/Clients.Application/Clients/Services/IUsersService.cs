using Users.Application.Users.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Application.Clients.Models;

namespace Users.Application.Users.Services;

public interface IUsersService
{
    Task<List<UserModel>> GetAllAsync();
    Task<UserModel> GetAsync(int id);
    Task<UserModel> GetCurrentUser();
    Task<UserModel> AddUserRoleAsync(int userId, string role);
    Task<UserModel> CreateAsync(CreatedUserModel createdUserModel);
    Task<UserModel> UpdateAsync(int id, UserPatchModel userPatchDto);
    Task DeleteAsync(int id);
    Task<UserModel> CheckUser(string email);
}

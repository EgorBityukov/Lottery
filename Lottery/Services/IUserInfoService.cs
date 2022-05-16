using Lottery.Models;
using Microsoft.AspNetCore.Identity;

namespace Lottery.Services
{
    public interface IUserInfoService
    {
        public bool UserExist(string id);
        public Task<UserInfo> GetUserInfoByIdAsync(string id);
        public Task SetPhotoByIdAsync(string userId, IFormFile image);
        public Task<IFormFile> GetPhotoByIdAsync(string id);
        public Task<string> GetImageByIdAsync(string id);
        public Task AddUserInfoAsync(string id);
    }
}

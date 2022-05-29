using Lottery.Models;
using Microsoft.AspNetCore.Identity;

namespace Lottery.Services
{
    public interface IUserInfoService
    {
        public bool UserExist(string id);
        public Task<UserInfo> GetUserInfoByIdAsync(string id);
        public Task SetPhotoByIdAsync(string userId, IFormFile image);
        public Task<IFormFile> GetImageByIdAsync(string id);
        public Task<string> GetImageStringByIdAsync(string id);
        public Task AddUserInfoAsync(string id);
        public Task AddBalanceAsync(string id, decimal amount);
        public Task TakeBalanceAsync(string id, decimal amount);
    }
}

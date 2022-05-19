using Lottery.Data;
using Lottery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationDbContext _context;

        public UserInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserInfoAsync(string id)
        {
            await _context.UserInfos.AddAsync(new Models.UserInfo() { Id = id, Balance = 0 });
            await _context.SaveChangesAsync();
        }

        public async Task<IFormFile> GetPhotoByIdAsync(string id)
        {
            var userInfo = await GetUserInfoByIdAsync(id);
            if (userInfo.Photo != null)
            {
                var stream = new MemoryStream(userInfo.Photo.Image);
                IFormFile file = new FormFile(stream, 0, userInfo.Photo.Image.Length, userInfo.Photo.Name, userInfo.Photo.FileName);
                return file;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<string> GetImageByIdAsync(string id)
        {
            var userInfo = await GetUserInfoByIdAsync(id);
            if (userInfo.Photo != null)
            {
                return Convert.ToBase64String(userInfo.Photo.Image);
            }
            else
            {
                return null;//_context.Photos.;
            }
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(string id)
        {
            return await _context.UserInfos.Include(p => p.Photo).Include(p => p.Address).Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task SetPhotoByIdAsync(string userId, IFormFile image)
        {
            var userInfo = await GetUserInfoByIdAsync(userId);

            if (userInfo != null)
            {
                if (image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)image.Length);
                    }
                    // установка массива байтов
                    userInfo.Photo = new Photo() { Image = imageData, Name = image.Name, FileName = image.FileName };

                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();
                }
            }
        }

        //public async Task<string> GetDefaultImage() { }

        public bool UserExist(string id)
        {
            return _context.UserInfos.Any(x => x.Id == id);
        }
    }
}

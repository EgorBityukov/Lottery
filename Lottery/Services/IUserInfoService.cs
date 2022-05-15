namespace Lottery.Services
{
    public interface IUserInfoService
    {
        public Task<bool> CheckUser(Guid id);
        public Task AddUserInfo(Guid guid);
    }
}

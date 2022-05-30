using Lottery.Models;
using Lottery.ViewModels;

namespace Lottery.Services
{
    public interface IDrawService
    {
        public Task<Draw> Draw(Lot lot);
        public Task<List<HistoryViewModel>> GetLastDraws();
        public Task<List<HistoryViewModel>> GetUserDraws(string userId);
        public Task<Draw> GetDrawByIdAsync(int id);
        public Task SellLot(int id);
        public Task OrderLot(int id);
        public Task ChangeStatusLot(int id, string status);
        public Task<List<HistoryViewModel>> GetAllDraws();
    }
}

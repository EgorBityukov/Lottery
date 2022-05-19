using Lottery.Models;
using Lottery.ViewModels;

namespace Lottery.Services
{
    public interface IDrawService
    {
        public Task<Draw> Draw(Lot lot);
        public Task<List<HistoryViewModel>> GetLastDraws();
    }
}

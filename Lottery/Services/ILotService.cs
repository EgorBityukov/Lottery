using Lottery.Models;

namespace Lottery.Services
{
    public interface ILotService
    {
        public Task<Ticket> BuyTicketAsync(UserInfo userInfo, int ticketId);

        public Task<Ticket> GetTicketByIdAsync(int ticketId);

        public Task<Lot> GetLotByIdAsync(int id);
    }
}

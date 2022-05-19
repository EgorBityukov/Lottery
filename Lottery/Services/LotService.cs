using Lottery.Data;
using Lottery.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Services
{
    public class LotService : ILotService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDrawService _drawService;

        public LotService(ApplicationDbContext context,
            IDrawService drawService)
        {
            _context = context;
            _drawService = drawService;
        }

        public async Task<Ticket> BuyTicketAsync(UserInfo userInfo, int ticketId)
        {
            Draw draw;

            var ticket = await GetTicketByIdAsync(ticketId);

            if(ticket.User != null)
            {
                throw new Exception("Ticket already booked");
            }

            var lot = ticket.Lot;
            
            ticket.User = userInfo;
            ticket.Date = DateTime.Now;

            if((userInfo.Balance - ticket.Lot.TicketPrice) < 0)
            {
                throw new Exception("Insufficient funds");
            }
            else
            {
                userInfo.Balance -= ticket.Lot.TicketPrice;
            }

            _context.Update(ticket);
            _context.Update(userInfo);
            await _context.SaveChangesAsync();

            //ChatHub chatHub = new ChatHub();
            //await chatHub.Send("Recieve", ticket.TicketId, Convert.ToBase64String(userInfo.Photo.Image));

            if (!CheckFreeTickets(lot)) 
            {
                draw = await _drawService.Draw(lot);
                ticket = draw.Ticket;
            }

            return ticket;
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets
                //.Include(t => t.Lot)
                //    .ThenInclude(l => l.Tickets)
                //    .ThenInclude(t => t.User)
                //.Include(t => t.User)
                //    .ThenInclude(u => u.Photo)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        public async Task<Lot> GetLotByIdAsync(int id)
        {
            return await _context.Lots
                .Include(l => l.Photo)
                .Include(t => t.Tickets)
                    .ThenInclude(u => u.User)
                        .ThenInclude(p => p.Photo)
                .FirstOrDefaultAsync(m => m.LotId == id);
        }

        public bool CheckFreeTickets(Lot lot)
        {
            return lot.Tickets.Any(t => t.User == null);
        }
    }
}

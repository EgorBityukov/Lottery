using Lottery.Data;
using Lottery.Models;
using Lottery.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RandomOrg.CoreApi;

namespace Lottery.Services
{
    public class DrawService : IDrawService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DrawService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task ChangeStatusLot(int id, string status)
        {
            var draw = await GetDrawByIdAsync(id);
            draw.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task<Draw> Draw(Lot lot)
        {
            int ticketPosition = 0;

            RandomOrgClient roc = RandomOrgClient.GetRandomOrgClient("e514a76f-a915-4f35-8a8e-c9b68b454c79");
            try
            {
                int[] response = roc.GenerateIntegers(1, 0, lot.TicketCount.Value-1);
                ticketPosition = response[0];
            }
            catch (Exception ex) { }

            var ticket = lot.Tickets.ElementAt(ticketPosition);

            var draw = new Draw()
            {
                Winner = ticket.User,
                Date = DateTime.Now,
                Lot = lot,
                Ticket = ticket,
                Status = "Waiting"
            };

            _context.Draws.Add(draw);
            await _context.SaveChangesAsync();
            return draw;
        }

        public async Task<Draw> GetDrawByIdAsync(int id)
        {
            return await _context.Draws.Where(d => d.DrawId == id).FirstOrDefaultAsync();
        }

        public async Task<List<HistoryViewModel>> GetLastDraws()
        {
            var draws = await _context.Draws.Take(10).OrderByDescending(draw => draw.Date).ToListAsync();
            List<HistoryViewModel> histories = new List<HistoryViewModel>();

            int countTickets;
            decimal ticketsPrice;

            foreach (var draw in draws)
            {
                countTickets = await _context.Tickets.Where(u => u.User == draw.Winner && u.Lot == draw.Lot).CountAsync();
                ticketsPrice = draw.Lot.TicketPrice.Value * countTickets;

                histories.Add(new HistoryViewModel 
                {
                    UserImage = Convert.ToBase64String(draw.Winner.Photo.Image),
                    NickName = (await _userManager.FindByIdAsync(draw.WinnerId)).UserName,
                    LotImage = Convert.ToBase64String(draw.Lot.Photo.Image),
                    LotName = draw.Lot.Name,
                    LotPrice = draw.Lot.Price,
                    CountTickets = countTickets,
                    TicketsPrice = ticketsPrice,
                    DrawDate = draw.Date.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                });
            }
            return histories;
        }

        public async Task<List<HistoryViewModel>> GetUserDraws(string userId)
        {
            var draws = await _context.Draws.Where(d => d.WinnerId.Equals(userId)).OrderByDescending(draw => draw.Date).ToListAsync();
            List<HistoryViewModel> histories = new List<HistoryViewModel>();

            int countTickets;
            decimal ticketsPrice;

            foreach (var draw in draws)
            {
                countTickets = await _context.Tickets.Where(u => u.User == draw.Winner && u.Lot == draw.Lot).CountAsync();
                ticketsPrice = draw.Lot.TicketPrice.Value * countTickets;

                histories.Add(new HistoryViewModel
                {
                    DrawId = draw.DrawId,
                    UserImage = Convert.ToBase64String(draw.Winner.Photo.Image),
                    NickName = (await _userManager.FindByIdAsync(draw.WinnerId)).UserName,
                    LotImage = Convert.ToBase64String(draw.Lot.Photo.Image),
                    LotName = draw.Lot.Name,
                    LotPrice = draw.Lot.Price,
                    CountTickets = countTickets,
                    TicketsPrice = ticketsPrice,
                    DrawDate = draw.Date.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    Status = draw.Status,
                });
            }
            return histories;
        }

        public async Task<List<HistoryViewModel>> GetAllDraws()
        {
            var draws = await _context.Draws.OrderByDescending(draw => draw.Date).ToListAsync();
            List<HistoryViewModel> histories = new List<HistoryViewModel>();

            int countTickets;
            decimal ticketsPrice;

            foreach (var draw in draws)
            {
                countTickets = await _context.Tickets.Where(u => u.User == draw.Winner && u.Lot == draw.Lot).CountAsync();
                ticketsPrice = draw.Lot.TicketPrice.Value * countTickets;

                histories.Add(new HistoryViewModel
                {
                    DrawId = draw.DrawId,
                    UserImage = Convert.ToBase64String(draw.Winner.Photo.Image),
                    NickName = (await _userManager.FindByIdAsync(draw.WinnerId)).UserName,
                    LotImage = Convert.ToBase64String(draw.Lot.Photo.Image),
                    LotName = draw.Lot.Name,
                    LotPrice = draw.Lot.Price,
                    CountTickets = countTickets,
                    TicketsPrice = ticketsPrice,
                    DrawDate = draw.Date.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    Status = draw.Status,
                    AddressId = draw.Winner.AddressId
                });
            }
            return histories;
        }

        public async Task OrderLot(int id)
        {
            var draw = await GetDrawByIdAsync(id);
            draw.Status = "Ordered";
            await _context.SaveChangesAsync();
        }

        public async Task SellLot(int id)
        {
            var draw = await GetDrawByIdAsync(id);
            draw.Status = "Sold";
            var amount = draw.Lot.Price;
            draw.Winner.Balance += amount;
            await _context.SaveChangesAsync();
        }
    }
}

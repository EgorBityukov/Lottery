namespace Lottery.ViewModels
{
    public class LotViewModel
    {
        public int LotId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? TicketCount { get; set; }
        public decimal? TicketPrice { get; set; }
        public IFormFile Image { get; set; }
    }
}

using Lottery.Models;

namespace Lottery.ViewModels
{
    public class HistoryViewModel
    {
        public int DrawId { get; set; }
        public string UserImage { get; set; }
        public string NickName { get; set; }
        public string LotImage { get; set; }
        public string LotName { get; set; }
        public decimal? LotPrice { get; set; }
        public int CountTickets { get; set; }
        public decimal TicketsPrice { get; set; }
        public string DrawDate { get; set; }
        public string Status { get; set; }
        public int? AddressId { get; set; }
    }
}

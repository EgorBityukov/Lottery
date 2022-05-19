namespace Lottery.ViewModels
{
    public class ResponceBuyTicketViewModel
    {
        public ResponceBuyTicketViewModel(string status, string data)
        {
            Status = status;
            Data = data;
        }

        public string Status { get; set; }
        public string Data { get; set; }
    }
}

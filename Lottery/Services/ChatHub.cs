using Microsoft.AspNetCore.SignalR;

namespace Lottery.Services
{
    public class ChatHub : Hub
    {
        public async Task Send(string arg1, string arg2)
        {
            await Clients.Others.SendAsync("Receive", arg1, arg2);
        }
    }
}

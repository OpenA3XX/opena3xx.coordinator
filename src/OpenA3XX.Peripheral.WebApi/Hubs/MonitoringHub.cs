using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace OpenA3XX.Peripheral.WebApi.Hubs
{
   
    public class ChatHub : Hub<IChatHub>
    {
        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.All.MessageReceivedFromHub(message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.NewUserConnected("a new user connected");
        }
    }

    public interface IChatHub
    {
        Task MessageReceivedFromHub(ChatMessage message);

        Task NewUserConnected(string message);
    }
    
    public class ChatMessage
    {
        public string Text { get; set; }
        public string ConnectionId { get; set; }
    }
}
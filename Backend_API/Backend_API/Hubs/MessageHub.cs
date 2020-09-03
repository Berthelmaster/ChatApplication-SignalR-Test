using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Backend_API.Hubs
{
    public class MessageHub : Hub
    {
        public async void SendMessageToAll(string message)
        {
            await Clients.All.SendCoreAsync("Messagelistener", new[] { message });
        }
    }
}

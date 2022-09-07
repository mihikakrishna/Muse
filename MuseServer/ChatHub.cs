using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MuseServer
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("RecieveMessage", message);
        }
    }
}

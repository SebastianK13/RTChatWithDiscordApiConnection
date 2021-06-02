using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTChatWithDiscordApiConnection
{
    [HubName("chat")]
    public class Chat : Hub
    {
        public async Task SendMessage(string user, string msg) =>
            await Clients.All.SendAsync("MesageReceived", user, msg);


    }
}

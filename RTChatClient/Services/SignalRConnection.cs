using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RTChatClient.Services
{
    public class SignalRConnection
    {   
        private HubConnection Connection { get; set; }
        private bool Blocked { get; set; } = false;
        public void Start()
        {
            var url = "http://localhost:5000/chat";

            Connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Connection.StartAsync().Wait();
        }
        public void SendMsg(string author, string message, string dateTime)
        {
            Connection.InvokeCoreAsync("SendMessage", args: new[] { author, message});
            Connection.On("MesageReceived", (string user, string msg) =>
            {
                if (!Blocked)
                {
                    Console.WriteLine(user + " " + dateTime + ":\n" + msg);
                    Blocked = true;
                }
            });
        }
        public void BreakConnection() =>
            Connection.StopAsync().Wait();
        public void Unblock() =>
            Blocked = false;

    }
}

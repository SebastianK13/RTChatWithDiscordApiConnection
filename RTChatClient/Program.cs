using Microsoft.AspNetCore.SignalR.Client;
using RTChatClient.Services;
using System;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;

namespace RTChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SignalRConnection();
            connection.Start();
            ApplicationService applicationService = new ApplicationService(connection);

            var discord = new DiscordService(connection);
            discord.Listner().GetAwaiter().GetResult();

            MessageTyping typing = new MessageTyping(discord);
            typing.Typing().GetAwaiter().GetResult();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(applicationService.OnExitEventHandler);

            Console.Read();
        }
    }
}

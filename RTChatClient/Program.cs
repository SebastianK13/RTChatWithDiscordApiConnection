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
        static async Task Main(string[] args)
        {
            DiscordService discord = new DiscordService();
            await discord.DiscordInitialize();

            TelegramService telegram = new TelegramService();
            telegram.TelegramInitialize();

            discord.SetTeleObject(telegram);
            telegram.SetTeleObject(discord);

            MessageTyping typing = new MessageTyping(discord, telegram);
            await typing.Typing();

            Console.Read();
        }
    }
}

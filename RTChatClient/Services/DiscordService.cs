using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Linq;
using System.Globalization;

namespace RTChatClient.Services
{
    public class DiscordService
    {
        private DiscordSocketClient client;
        private TelegramService teleClient;
        private ulong guildId = 849317331086999612;
        private ulong textChanneldId = 849317331086999615;
        private string uName = "";
        private TimeZoneInfo currentTZ;
        public bool initFinished = false;
        public DiscordService()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            currentTZ = TimeZoneInfo.Local;
        }

        public async Task<bool> DiscordInitialize()
        {
            client = new DiscordSocketClient();
            client.MessageReceived += NewMessageHAndler;
            client.Ready += InitializeMessageList;
            var token = File.ReadAllText("tok.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            return Task.CompletedTask.IsCompleted;
        }
        private Task NewMessageHAndler(SocketMessage msg)
        {
            if (!teleClient.Stop)
            {
                string date = DateTime.Now.ToString();
                string message = msg.Author.Username + " " + date + ":\n" + msg.Content;
                Console.WriteLine(message);
                teleClient.SendMessage(message).GetAwaiter().GetResult();
            }
            else
                teleClient.Stop = false;

            return Task.CompletedTask;
        }
        private Task InitializeMessageList()
        {
            var messages = client.GetGuild(guildId)
                .GetTextChannel(textChanneldId)
                .GetMessagesAsync()
                .FlattenAsync()
                .Result
                .Take(10)
                .ToList();
            messages.Reverse();

            foreach (var message in messages)
            {
                if (!String.IsNullOrEmpty(message.Content))
                    Console.WriteLine(message.Author.Username + " " +
                        TimeZoneInfo.ConvertTimeFromUtc(message.Timestamp.UtcDateTime, currentTZ) + ":\n" + message.Content);
            }

            initFinished = true;

            return Task.CompletedTask;
        }
        public async Task SendMessage(string message) =>
            await client.GetGuild(guildId).GetTextChannel(textChanneldId).SendMessageAsync(message);
        public void SetTeleObject(TelegramService telegram) =>
            teleClient = telegram;
    }
}

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
        private SignalRConnection connection;
        private DiscordSocketClient client;
        private ulong id = 849546282582147102;
        private ulong guildId = 849317331086999612;
        private ulong textChanneldId = 849317331086999615;
        private string uName = "";
        private TimeZoneInfo currentTZ;

        public DiscordService(SignalRConnection connection)
        {
            this.connection = connection;
            Console.ForegroundColor = ConsoleColor.Blue;
            currentTZ = TimeZoneInfo.Local; 
        }

        public async Task Listner()
        {
            client = new DiscordSocketClient();
            client.MessageReceived += NewMessageHAndler;
            client.Ready += InitializeMessageList;
            var token = File.ReadAllText("tok.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

        }
        private Task NewMessageHAndler(SocketMessage msg)
        {
            DateTime date = DateTime.Now;
            string dateTimeZoneLess = date.ToString("d", CultureInfo.InvariantCulture);
            connection.SendMsg(msg.Author.Username, msg.Content, dateTimeZoneLess);
            connection.Unblock();
            return Task.CompletedTask;
        }
        private Task InitializeMessageList()
        {
            uName = client.GetUser(id).Username;
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

            return Task.CompletedTask;
        }
        public async Task SendMessage(string message) =>
            await client.GetGuild(guildId).GetTextChannel(textChanneldId).SendMessageAsync(message);
    }
}

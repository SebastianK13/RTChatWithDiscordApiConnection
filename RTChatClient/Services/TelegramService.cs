using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace RTChatClient.Services
{
    public class TelegramService
    {
        private TelegramBotClient botClient;
        private DiscordService discord;
        public bool Stop = false;
        public bool initializationFinished = false;

        public void TelegramInitialize()
        {
            var token = File.ReadAllText("tokTelegram.txt");
            botClient = new TelegramBotClient(token);
            botClient.OnMessage += NewMessageHandler;
            botClient.StartReceiving();
        }

        public void SetTeleObject(DiscordService discord) =>
            this.discord = discord;
        private void NewMessageHandler(object sender, MessageEventArgs e)
        {
            if (initializationFinished)
            {
                string msg = e.Message.From.Username + " " + e.Message.Date + ":\n" + e.Message.Text;
                Console.WriteLine(msg);
                Stop = true;
                discord.SendMessage(msg).GetAwaiter();
            }
        }
        public async Task SendMessage(string message) =>
            await botClient.SendTextMessageAsync("-514346985", message);
    }
}

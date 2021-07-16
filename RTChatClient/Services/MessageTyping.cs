using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTChatClient.Services
{
    public class MessageTyping
    {
        private DiscordService discord;
        private TelegramService telegram;
        bool decision = false;

        public MessageTyping(DiscordService discord, TelegramService telegram) {
            this.discord = discord;
            this.telegram = telegram;
        }



        public async Task Typing()
        {
            telegram.initializationFinished = true;
            await GetChar();
        }
        public void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        public async Task GetChar()
        {
            do
            {
                Console.WriteLine("Where to send message? Options: Discord - d, Telegram - t, Discord and Telegram - b");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D:
                        Console.WriteLine();
                        await DiscordSending();
                        break;
                    case ConsoleKey.T:
                        Console.WriteLine();
                        await TelegramSending();
                        break;
                    case ConsoleKey.B:
                        Console.WriteLine();
                        await SendBoth();
                        break;
                    case ConsoleKey.Escape:
                        System.Environment.Exit(0);
                        break;
                    default:
                        break;
                }

            } while (!decision);
        }

        private async Task SendBoth()
        {
            if (discord.initFinished)
            {
                Console.WriteLine("Type message:");
                string message = Console.ReadLine();
                if (!String.IsNullOrEmpty(message))
                {
                    ClearLastLine();
                    await telegram.SendMessage(message);
                    await discord.SendMessage(message);
                }
            }
        }

        private async Task TelegramSending()
        {
            if (discord.initFinished)
            {
                Console.WriteLine("Type message:");
                string message = Console.ReadLine();
                if (!String.IsNullOrEmpty(message))
                {
                    ClearLastLine();
                    await telegram.SendMessage(message);
                }
            }
        }

        private async Task DiscordSending()
        {

            if (discord.initFinished)
            {
                Console.WriteLine("Type message:");
                string message = Console.ReadLine();
                if (!String.IsNullOrEmpty(message))
                {
                    ClearLastLine();
                    await discord.SendMessage(message);
                }
            }
        }
    }
}

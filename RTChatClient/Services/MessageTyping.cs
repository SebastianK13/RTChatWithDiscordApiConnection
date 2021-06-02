using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTChatClient.Services
{
    public class MessageTyping
    {
        private DiscordService discord;

        public MessageTyping(DiscordService discord) =>
            this.discord = discord;

        public async Task Typing()
        {
            bool Exit = false;
            do
            {
                string message = Console.ReadLine();
                if (!String.IsNullOrEmpty(message))
                {
                    ClearLastLine();
                    await discord.SendMessage(message);
                }

            } while (!Exit);
        }
        public void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}

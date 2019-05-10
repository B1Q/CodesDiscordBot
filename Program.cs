using System;
using System.Threading.Tasks;
using BotApp.Bot;
using BotApp.Database;
using BotApp.Database.Tables;
using static BotApp.Utility.ConsoleUtilities;

namespace BotApp
{
    internal class Program
    {
        private static void Main(string[] args)
            => new Program().Run().GetAwaiter().GetResult();

        private DiscordBot _bot;

        private async Task Run()
        {
            MySQL.Connect();
            DiscordServers.LoadServers();
            
            _bot = new DiscordBot();
            _bot.Login();
            await Task.Delay(-1);
        }
    }
}
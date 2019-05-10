using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotApp.Structs;
using BotApp.Utility;
using Discord;
using Discord.WebSocket;

namespace BotApp.Bot
{
    public class DiscordBot
    {
        public static DiscordBot Instance { get; set; }

        private readonly DiscordSocketClient _socketClient;

        public DiscordBot()
        {
            if (Instance == null)
                Instance = this;

            _socketClient = new DiscordSocketClient(new DiscordSocketConfig()
            {
                DefaultRetryMode = RetryMode.AlwaysRetry,
                ConnectionTimeout = 15000,
                MessageCacheSize = 1000, // cache up to 1000 messages per channel
            });
            _socketClient.Log += SocketLog;
            _socketClient.MessageReceived += DiscordManager.MessageReceived;
            _socketClient.UserJoined += DiscordManager.UserJoined;
            _socketClient.JoinedGuild += DiscordManager.JoinedGuild;
        }

        private static async Task SocketLog(LogMessage arg)
        {
            ConsoleUtilities.LogCustom(arg.Severity, arg.Message);
            await Task.Delay(100);
        }

        public async void Login()
        {
            await _socketClient.LoginAsync(TokenType.Bot, Constants.ApiToken);
            await _socketClient.StartAsync();
        }

        public void BroadcastMessage(List<SServerIdentity> servers, string message)
        {
            //ConsoleUtilities.LogError($"Broadcasting to {servers.Count} Servers!");
            servers.ForEach(async s =>
            {
                var guild = _socketClient.GetGuild(s.ServerId);
                var channel = guild.GetTextChannel(s.ServerMainChannel);

                await channel.SendMessageAsync(message);

                //ConsoleUtilities.LogError($"Sending to {channel.Name}!");
            });
        }
    }
}
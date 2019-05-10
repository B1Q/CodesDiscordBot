using System.Linq;
using System.Threading.Tasks;
using BotApp.Database.Tables;
using Discord.WebSocket;
using static BotApp.Utility.ConsoleUtilities;

namespace BotApp.Bot
{
    public class DiscordManager
    {
        public static async Task UserJoined(SocketGuildUser user)
        {
        }


        public static async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot) return;
            //await message.Channel.SendMessageAsync($"Member {message.Author.Username} Wrote: {message.Content}");
            //Log($"{message.Author} sent {message.Content} in {message.Channel.Id}");
            if (message.Channel is SocketGuildChannel guild)
                DiscordServers.Broadcast($"[{guild.Guild.Name}]-> {message.Author.Username}: {message.Content}",
                    guild.Guild.Id);
        }


        public static async Task JoinedGuild(SocketGuild guild)
        {
            var channel = guild.TextChannels.OrderBy(i => i.CreatedAt).FirstOrDefault();
            if (channel != null) await channel.SendMessageAsync($"Hello there, I just joined!");

            // insert into the database
            var serverId = guild.Id;
            var serverName = guild.Name;
            var mainChannel = guild.DefaultChannel.Id;
            const string welcomeMessage = "Welcome %user% to %server_name%";
            const int serverMembership = 0;
            var addedBy = guild.OwnerId;

            var serverEntity = new Server()
            {
                ServerId = serverId,
                ServerName = serverName,
                ServerWelcome = welcomeMessage,
                ServerMembership = serverMembership,
                ServerMainChannel = mainChannel,
                AddedBy = addedBy
            };

            DiscordServers.InsertServer(serverEntity);
        }
    }
}
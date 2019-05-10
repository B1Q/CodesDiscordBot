using System.Collections.Generic;
using System.Linq;
using BotApp.Database;
using BotApp.Database.Tables;
using BotApp.Structs;
using BotApp.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using static BotApp.Utility.ConsoleUtilities;

namespace BotApp.Bot
{
    public class DiscordServers
    {
        public static List<Server> Servers;
        public static DiscordBot BotInstance;

        static DiscordServers()
        {
            Servers = new List<Server>();
            BotInstance = DiscordBot.Instance;
        }

        public static void LoadServers()
        {
            lock (Servers)
                Servers = MySQL.Connection.Query<Server>($"select * from {Constants.TablePrefix}servers").ToList();
        }

        public static void InsertServer(Server srv)
        {
            if (HasServer(srv.ServerId)) return;
            if (MySQL.Connection.Insert(srv) <= 0) return;
            lock (Servers)
                Servers.Add(srv);
        }

        public static bool HasServer(ulong id)
        {
            lock (Servers)
                return Servers.Any(i => i.ServerId == id);
        }

        public static void Broadcast(string message, ulong except = 0)
        {
            if (BotInstance == null)
                BotInstance = DiscordBot.Instance;
            lock (Servers)
            {
                var servers = Servers.Where(i => i.ServerId != except)
                    .Select(i => new SServerIdentity(i.ServerId, i.ServerMainChannel)).ToList();
                BotInstance.BroadcastMessage(servers, message);
            }
        }
        
        public static (ulong, string) FormattedWelcomeMessage(ulong server, string user, string serverName = null)
        {
            var empty = (ulong.MinValue, string.Empty);
            if (!HasServer(server)) return empty;

            lock (Servers)
            {
                var serverEntity = Servers.FirstOrDefault(i => i.ServerId == server);
                if (serverEntity == null) return empty;
                var message = serverEntity.ServerWelcome;
                message = message.Replace("%user%", user);
                message = message.Replace("%server_name%", serverName ?? serverEntity.ServerName);
                return (serverEntity.ServerMainChannel, message);
            }
        }
    }
}
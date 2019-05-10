using System;
using BotApp.Utility;
using Dapper.Contrib.Extensions;
using DapperExtensions;
using Dapper.Mapper;

namespace BotApp.Database.Tables
{
    [Table("bot_servers")]
    public class Server
    {
        public int Id { get; set; }
        public ulong ServerId { get; set; }
        public ulong ServerMainChannel { get; set; }
        public string ServerName { get; set; }
        public string ServerWelcome { get; set; }
        public int ServerMembership { get; set; } // free, premium
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ulong AddedBy { get; set; } // whoever invited our bot to their server
    }
}
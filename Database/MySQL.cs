using System;
using System.Data;
using BotApp.Utility;
using MySql.Data.MySqlClient;

namespace BotApp.Database
{
    public class MySQL
    {
        public static bool Connected => Connection != null && Connection.State == ConnectionState.Open;

        public static MySqlConnection Connection => _connection;

        private static MySqlConnection _connection;

        public static async void Connect()
        {
            if (Connected) return;

            var builder = new MySqlConnectionStringBuilder
            {
                Server = Constants.Host,
                UserID = Constants.DbUser,
                Password = Constants.DbPassword,
                Database = Constants.DbName,
                ConnectionTimeout = 10000,
                CharacterSet = "utf8"
            };

            _connection = new MySqlConnection(builder.ConnectionString);
            await Connection.OpenAsync();
            
            if (Connected)
                ConsoleUtilities.Log("Connected to Database!");
        }
        
        
    }
}
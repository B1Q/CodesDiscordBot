using System;
using Discord;

namespace BotApp.Utility
{
    public class ConsoleUtilities
    {
        public static void Log(string message)
        {
            Console.WriteLine($"Info-> [{DateTime.Now.ToShortTimeString()}]: {message}");
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Critical-> [{DateTime.Now.ToShortTimeString()}]: {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }


        public static void LogCustom(LogSeverity type, string message)
        {
            switch (type)
            {
                case LogSeverity.Critical:
                    LogError(message);
                    break;
                case LogSeverity.Error:
                    LogError(message);
                    break;
                case LogSeverity.Warning:
                    break;
                case LogSeverity.Info:
                    Log(message);
                    break;
                case LogSeverity.Verbose:
                    Log(message);
                    break;
                case LogSeverity.Debug:
                    Log(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
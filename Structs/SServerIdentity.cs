namespace BotApp.Structs
{
    public struct SServerIdentity
    {
        public ulong ServerId { get; set; }
        public ulong ServerMainChannel { get; set; }


        public SServerIdentity(ulong id, ulong channel)
        {
            ServerId = id;
            ServerMainChannel = channel;
        }
    }
}
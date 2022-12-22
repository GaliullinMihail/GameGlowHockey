namespace XProtocol
{
    public enum XPacketType
    {
        Register,  // player, nickname from client
        SendCursorPosition, // int id player, int  x, int y from client
        PressPause, // void from client
        Pause,      // void from server
        Resume,     // void from server
        Exit, //player from client
        Win, //from server
        Lose, //from server
        IncreaseScore, //player from server 
        EarlyWin, //player from server
        AddToGame, //player from server
        Handshake,
        StartGame,
        Unknown,
        Exception
    }
}
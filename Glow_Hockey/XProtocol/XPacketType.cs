namespace XProtocol
{
    public enum XPacketType
    {
        Register,  // player, nickname from client
        SendCursorPosition, // int id player, int  x, int y from client
        Pause,      // void from server
        Exit, //player from client
        Win, //from server
        Lose, //from server
        IncreaseScore, //player from server 
        EarlyWin, //player from server
        AddToGame, //player from server
        Handshake,
        StartGame,
        GameInfo, //int firstPlayerX, int firstPlayerY, int secondPLayerX, int secondPlayerY, int puckX, int puckY 
        Unknown,
        Exception
    }
}
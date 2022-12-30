namespace XProtocol
{
    public enum XPacketType
    {
        Register,  // player, nickname from client +
        SendCursorPosition, // int id player, int  x, int y from client +
        Pause,      // void from server +
        ChangedScore, //int firstPlayerScore int secondPlayerScore
        EarlyWin, //player from server -
        AddToGame, //player from server +
        Handshake, // +
        StartGame, // +
        GameInfo, //int firstPlayerX, int firstPlayerY, int secondPLayerX, int secondPlayerY, int puckX, int puckY 
        Unknown,
        Exception // +
    }
}
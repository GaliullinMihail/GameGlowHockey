using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketHandshake
    {
        [XField(1)]
        public int MagicHandshakeNumber;
    }

    public class XPacketWithoutInfo
    {
    }

    public class XPacketAddToGame
    {
        [XField(1)]
        public int Id;
    }

    public class XPacketCursor
    {
        [XField(1)]
        public int Id;

        [XField(2)]
        public int X;

        [XField(3)]
        public int Y;
    }

    public class XPacketGameInfo
    {
        [XField(1)]
        public int firstPlayerX;

        [XField(2)]
        public int firstPlayerY;

        [XField(3)]
        public int secondPlayerX;

        [XField(4)]
        public int secondPlayerY;

        [XField(5)]
        public int puckX;

        [XField(6)]
        public int puckY;
    }
}
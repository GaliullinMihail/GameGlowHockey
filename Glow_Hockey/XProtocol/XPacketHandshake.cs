using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketHandshake
    {
        [XField(1)]
        public int MagicHandshakeNumber;
    }

    public class XPacketRegister
    {
    }

    public class XPacketException
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

    public class XPacketStartGame
    {

    }
}
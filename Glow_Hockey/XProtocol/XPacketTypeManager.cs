using System;
using System.Collections.Generic;

namespace XProtocol
{
    public static class XPacketTypeManager
    {
        private static readonly Dictionary<XPacketType, Tuple<byte, byte>> TypeDictionary =
            new Dictionary<XPacketType, Tuple<byte, byte>>();

        static XPacketTypeManager()
        {
            RegisterType(XPacketType.Exception, 0, 0);
            RegisterType(XPacketType.Handshake, 1, 0);
            RegisterType(XPacketType.Register, 1, 1);
            RegisterType(XPacketType.AddToGame, 1, 2);
            RegisterType(XPacketType.SendCursorPosition, 1, 3);
            RegisterType(XPacketType.StartGame, 1, 4);
            RegisterType(XPacketType.Pause, 1, 5);
            RegisterType(XPacketType.GameInfo, 1, 6);
            RegisterType(XPacketType.ChangedScore, 1, 7);
            
        }

        public static void RegisterType(XPacketType type, byte btype, byte bsubtype)
        {
            if (TypeDictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is already registered.");
            }

            TypeDictionary.Add(type, Tuple.Create(btype, bsubtype));
        }

        public static Tuple<byte, byte> GetType(XPacketType type)
        {
            if (!TypeDictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is not registered.");
            }

            return TypeDictionary[type];
        }

        public static XPacketType GetTypeFromPacket(XPacket packet)
        {
            var type = packet.PacketType;
            var subtype = packet.PacketSubtype;

            foreach (var tuple in TypeDictionary)
            {
                var value = tuple.Value;

                if (value.Item1 == type && value.Item2 == subtype)
                {
                    return tuple.Key;
                }
            }

            return XPacketType.Unknown;
        }
    }
}
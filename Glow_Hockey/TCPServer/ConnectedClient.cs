using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TCPServer.Hockey;
using XProtocol;
using XProtocol.Serializator;

namespace TCPServer
{
    internal class ConnectedClient
    {
        public Socket Client { get; }

        private readonly Game _game;

        private XServer _server;

        private readonly Queue<byte[]> _packetSendingQueue = new Queue<byte[]>();


        public ConnectedClient(Socket client, Game game, XServer server)
        {
            Client = client;
            _game = game;
            _server = server;

            Task.Run((Action)ProcessIncomingPackets);
            Task.Run((Action)SendPackets);
        }

        private void ProcessIncomingPackets()
        {
            while (true) // Слушаем пакеты, пока клиент не отключится.
            {
                var buff = new byte[256]; // Максимальный размер пакета - 256 байт.
                Client.Receive(buff);

                buff = buff.TakeWhile((b, i) =>
                {
                    if (b != 0xFF) return true;
                    return buff[i + 1] != 0;
                }).Concat(new byte[] { 0xFF, 0 }).ToArray();

                var parsed = XPacket.Parse(buff);

                if (parsed != null)
                {
                    ProcessIncomingPacket(parsed);
                }
            }
        }

        private void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);
            try
            {
                switch (type)
                {
                    case XPacketType.Handshake:
                        ProcessHandshake(packet);
                        break;

                    case XPacketType.Register:
                        ProcessRegister(packet);
                        break;

                    case XPacketType.SendCursorPosition:
                        ProcessCursor(packet);
                        break;

                    case XPacketType.Pause:
                        ProcessPause(packet);
                        break;

                    case XPacketType.Unknown:
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                QueuePacketSend(XPacketConverter.Serialize(XPacketType.Exception, new XPacketWithoutInfo()).ToPacket());
            }

            
        }

        private void ProcessPause(XPacket packet)
        {
            _game.IsPause = !_game.IsPause;
            foreach(var client in _server.clients)
            {
                client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Pause, new XPacketWithoutInfo()).ToPacket());
            }
        }

        private void ProcessCursor(XPacket packet)
        {
            Console.WriteLine("Recieved cursor packet");

            var cursorPacket = XPacketConverter.Deserialize<XPacketCursor>(packet);
            _game.ChangePosition(cursorPacket.Id, new Point(cursorPacket.X, cursorPacket.Y));
            Console.WriteLine($"{cursorPacket.Id} - id {cursorPacket.X}- x {cursorPacket.Y} - y");        }

        private void ProcessHandshake(XPacket packet)
        {
            Console.WriteLine("Recieved handshake packet.");

            var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);
            handshake.MagicHandshakeNumber -= 15;

            Console.WriteLine("Answering..");

            QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        }

        private void ProcessRegister(XPacket packet)
        {
            Console.WriteLine("Recieved register packet.");

            _game.AddPlayer();

            Console.WriteLine("Answering..");

            QueuePacketSend(XPacketConverter.Serialize(XPacketType.AddToGame, new XPacketAddToGame { Id = _game.PlayerCount - 1 }).ToPacket());

            Console.WriteLine("Give id packet to client");

            if(_game.PlayerCount == 2)
            {
                Console.WriteLine("give start packets to clients");
                _game.IsPause = false;

                foreach(var client in _server.clients)
                {
                    var startGamePacket = new XPacketWithoutInfo();
                    client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.StartGame, startGamePacket).ToPacket());
                }
                Console.WriteLine("gave start packets to clients");
            }

        }

        public void QueuePacketSend(byte[] packet)
        {
            if (packet.Length > 256)
            {
                throw new Exception("Max packet size is 256 bytes.");
            }

            _packetSendingQueue.Enqueue(packet);
        }

        private void SendPackets()
        {
            while (true)
            {
                if (_packetSendingQueue.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }

                var packet = _packetSendingQueue.Dequeue();
                Client.Send(packet);

                Thread.Sleep(100);
            }
        }
    }
}
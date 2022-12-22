using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using TCPServer.Hockey;
using XProtocol;
using XProtocol.Serializator;

namespace TCPServer
{
    public class XServer
    {
        private Stopwatch watch = new Stopwatch();
        private readonly Socket _socket;
        internal readonly List<ConnectedClient> clients;
        private Game _game;

        private bool _listening;
        private bool _stopListening;

        public XServer()
        {
            watch.Start();


            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<ConnectedClient>();
            _game = new Game();
        }

        public void Start()
        {
            if (_listening)
            {
                throw new Exception("Server is already listening incoming requests.");
            }

            _socket.Bind(new IPEndPoint(IPAddress.Any, 4910));
            _socket.Listen(10);

            _listening = true;
            Task.Run((Action)SendGameInfo);
        }

        public void Stop()
        {
            if (!_listening)
            {
                throw new Exception("Server is already not listening incoming requests.");
            }

            _stopListening = true;
            _socket.Shutdown(SocketShutdown.Both);
            _listening = false;
        }

        private void SendGameInfo()
        {
            while(true)
            {
                if(_game.IsPause)
                {
                    Thread.Sleep(100);
                    continue;
                }

                if(watch.ElapsedMilliseconds > 50)
                {
                    var firstPlayerPos = _game.GetPlayerPosition(0);
                    var secondPlayerPos = _game.GetPlayerPosition(1);
                    var puckPos = _game.PuckPosition;
                    foreach (var client in clients)
                    {

                        client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.GameInfo, new XPacketGameInfo
                        {
                            firstPlayerX = firstPlayerPos.X,
                            firstPlayerY = firstPlayerPos.Y,
                            secondPlayerX = secondPlayerPos.X,
                            secondPlayerY = secondPlayerPos.Y,
                            puckX = puckPos.X,
                            puckY = puckPos.Y
                        }).ToPacket());
                    }
                    watch.Restart();
                }
            }
        }

        public void AcceptClients()
        {
            while (true)
            {
                if (_stopListening)
                {
                    return;
                }

                Socket client;

                try
                {
                    client = _socket.Accept();
                }
                catch { return; }

                Console.WriteLine($"[!] Accepted client from {(IPEndPoint)client.RemoteEndPoint}");

                var c = new ConnectedClient(client, _game, this);
                this.clients.Add(c);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Hockey
{
    public class Game
    {
        public static int radiusPlayer = 10;
        public static int radiusPuck = 5;
        Point firstPlayerPosition = new Point(500, 592);                    
        Point secondPlayerPosition = new Point(1415, 592);                  
        private List<Player> players;
        private GameScore Score;
        private Puck puck;
        public bool IsPause;

        public Game()
        {
            players = new List<Player>();
            Score = new GameScore();
            puck = new Puck();
            IsPause = true;
        }
        public void AddPlayer()
        {
            if(players.Count >= 2)
            {
                throw new Exception("Add more than 2 players");
            }

            players.Add(new Player($"player{players.Count}", players.Count == 0 ? firstPlayerPosition: secondPlayerPosition));
            Console.WriteLine($"add player {players.Count-1}");
        }

        public void ChangePosition(int id, Point position)
        {
            if (players.Count != 2 || id >= 2)
            {
                throw new ArgumentOutOfRangeException("id");
            }
            var player = players[id];
            if (player == null)
                throw new InvalidOperationException("There is no such player in game");
            player.Position = position;
        }

        public bool IsIntersected(Point player, Point puck) => radiusPlayer + radiusPuck < PointLength(player, puck);

        private double PointLength(Point point1, Point point2) => Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + 
            (point1.Y - point2.Y) * (point1.Y - point2.Y));

        public int PlayerCount => players.Count;

        public Point GetPlayerPosition(int id) => players.Count < id + 1? throw new ArgumentOutOfRangeException(): players[id].Position;

        public Point PuckPosition => puck.position;

    }
}

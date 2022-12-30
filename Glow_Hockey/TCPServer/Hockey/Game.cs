using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Hockey
{
    public class Game
    {
        public static int radiusPlayer = 50;
        public static int radiusPuck = 40;
        Point firstPlayerPosition = new Point(500, 592);                    
        Point secondPlayerPosition = new Point(1415, 592);                  
        private List<Player> players;
        public GameScore Score;
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

            players.Add(new Player($"player{players.Count}", 
                players.Count == 0 ? firstPlayerPosition: secondPlayerPosition,
                players.Count == 0 ? 43 + radiusPlayer: 957 + radiusPlayer,
                players.Count == 0 ? 957 - radiusPlayer: 1872 - radiusPlayer));
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


            var prevPosition = player.Position;

            player.Position = position;

            puck.UpdateIntersected(player.Position, new Point(player.Position.X - prevPosition.X, player.Position.Y - prevPosition.Y));
            
            
        }

        public static bool IsIntersected(Point player, Point puck) => radiusPlayer + radiusPuck > PointLength(player, puck);

        private static double PointLength(Point point1, Point point2) => Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + 
            (point1.Y - point2.Y) * (point1.Y - point2.Y));

        public int PlayerCount => players.Count;

        public Point GetPlayerPosition(int id) => players.Count < id + 1? throw new ArgumentOutOfRangeException(): players[id].Position;

        public Point PuckPosition => puck.Position;

        public void UpdateGame(int milliseconds)
        {
            puck.UpdateIntersected(players[0].Position, new Point(-puck.acceleration.X * 100, -puck.acceleration.Y * 100));
            puck.UpdateIntersected(players[1].Position, new Point(-puck.acceleration.X * 100, -puck.acceleration.Y * 100));
            puck.Update(milliseconds);

            if(puck.Position.X >= 1872 - radiusPuck && puck.Position.Y >= 382 && puck.Position.Y <= 812)
            {
                Score.IncreaseFirstPlayerScore();
            }

            if(puck.Position.X <= 43 + radiusPuck && puck.Position.Y >= 382 && puck.Position.Y <= 812)
            {
                Score.IncreaseSecondPlayerScore();
            }
        }

    }
}

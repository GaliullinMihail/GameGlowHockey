using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Hockey
{
    public class Player
    {
        public readonly string Name;
        private readonly int minX;
        private readonly int maxX;
        private Point _position;
        public Point Position { get
            {
                return _position;
            }

            set
            {
                int x = value.X < minX ? minX : value.X > maxX ? maxX : value.X;
                int y = value.Y < 152 + 50 ? 202 : value.Y > 1043 - 50 ? 993 : value.Y;
                _position = new Point(x, y);
            } }

        public Player(string name, Point position, int minX, int maxX)
        {
            this.minX = minX;
            this.maxX = maxX;
            Name = name;
            Position = position;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Hockey
{
    public class Puck
    {
        private Point _position;
        public Point Position { 
            get
            {
                return _position;
            }
            
            set
            {
                int x = value.X < 43 + 40 ? 83 : value.X > 1872 - 40 ? 1832 : value.X;
                int y = value.Y < 152 + 40 ? 192 : value.Y > 1043 - 40 ? 1003 : value.Y;
                _position = new Point(x, y);
            }
        }
        private int radius = 40;
        private int leftBorder = 43;
        private int rightBorder = 1872;
        private int topBorder = 152;
        private int bottomBorder = 1043;
        private Point acceleration;
        public Puck()
        {
            Position = new Point(957, 592);
            acceleration = new Point(0, 0);
        }

        //left border 43px, right border 1872px
        //top border 152px, bottom border 1043px
        public void HitWithPlayer(Player player, Point playerAcceleration)          
        {
        }

        private void HitHeightBorder() => acceleration.Y *= -1;
        private void HitWidthBorder() => acceleration.X *= -1;

        public void HitBorder()
        {
            if (Position.X + radius >= rightBorder || Position.X - radius <= leftBorder)
                HitWidthBorder();

            if(Position.Y + radius >= bottomBorder || Position.Y - radius <= topBorder)
                HitHeightBorder();
        }
    }
}

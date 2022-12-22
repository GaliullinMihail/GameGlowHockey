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
        public Point position;
        private int radius = 40;
        private int leftBorder = 43;
        private int rightBorder = 1872;
        private int topBorder = 152;
        private int bottomBorder = 1043;
        private Point acceleration;
        public Puck()
        {
            position = new Point(957, 592);
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
            if (position.X + radius >= rightBorder || position.X - radius <= leftBorder)
                HitWidthBorder();

            if(position.Y + radius >= bottomBorder || position.Y - radius <= topBorder)
                HitHeightBorder();
        }
    }
}

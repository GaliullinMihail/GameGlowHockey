using Glow_Hockey.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glow_Hockey.Views
{
    public class GraphicsDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeSize = 5;
            canvas.StrokeColor = Colors.Red;
            canvas.FillColor = Colors.Red;
            canvas.FillCircle(XClient.firstPlayer, (double) 50); //first player
            canvas.StrokeColor = Colors.Blue;
            canvas.FillColor = Colors.Blue;
            canvas.FillCircle(XClient.secondPlayer, (double) 50); //second player
            canvas.StrokeColor = Colors.Black;
            canvas.FillColor = Colors.Black;
            canvas.FillCircle(XClient.puck, (double) 40);          //puck

            canvas.DrawLine(43, 152, 1872, 152);    //top border
            canvas.DrawLine(43, 1034, 1872, 1034);  //bottom border
            canvas.DrawLine(43, 152, 43, 382);
            canvas.DrawLine(43, 812, 43, 1034);
            canvas.DrawLine(1872, 152, 1872, 382);
            canvas.DrawLine(1872, 812, 1872, 1034);
            canvas.DrawLine(957, 152, 957, 1034);
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 8;
            canvas.DrawLine(43, 382, 43, 812);
            canvas.StrokeColor = Colors.Blue;
            canvas.DrawLine(1872, 382, 1872, 812);
        }
    }
}

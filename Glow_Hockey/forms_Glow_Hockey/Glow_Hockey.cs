using XProtocol.Serializator;
using XProtocol;
using Glow_Hockey.Client;
using System.Drawing;

namespace forms_Glow_Hockey
{
    public partial class Glow_Hockey : Form
    {
        public Glow_Hockey()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        Graphics g;
        private void StartGame_Click(object sender, EventArgs e)
        {
            
            //if (XClient.IsRegistered)
            //{
            //    XClient.QueuePacketSend(
            //            XPacketConverter.Serialize(
            //                XPacketType.Register,
            //                new XPacketWithoutInfo
            //                {
            //                }).ToPacket());
            //}

            StartGame.Visible = false;
        }

        private void Glow_Hockey_Paint(object sender, PaintEventArgs e)
        {
            g = CreateGraphics();
            g.Clear(Color.White);
            g.DrawEllipse(Pens.Red, XClient.firstPlayer.X-25, XClient.firstPlayer.Y-25, 50, 50);
            g.DrawEllipse(Pens.Blue, XClient.secondPlayer.X-25, XClient.secondPlayer.Y-25, 50, 50);
            g.DrawEllipse(Pens.Black, XClient.puck.X-20, XClient.puck.Y-20, 40, 40);
            g.DrawLine(Pens.Black, 43, 152, 1872, 152);
            g.DrawLine(Pens.Black, 43, 1034, 1872, 1034);  //bottom border
            g.DrawLine(Pens.Black, 43, 152, 43, 382);
            g.DrawLine(Pens.Black, 43, 812, 43, 1034);
            g.DrawLine(Pens.Black, 1872, 152, 1872, 382);
            g.DrawLine(Pens.Black, 1872, 812, 1872, 1034);
            g.DrawLine(Pens.Black, 957, 152, 957, 1034);
            g.DrawLine(Pens.Red, 43, 382, 43, 812);
            g.DrawLine(Pens.Blue, 1872, 382, 1872, 812);
        }

        //canvas.StrokeSize = 5;
        //    canvas.StrokeColor = Colors.Red;
        //    canvas.FillColor = Colors.Red;
        //    canvas.FillCircle(XClient.firstPlayer, (double) 50); //first player
        //    canvas.StrokeColor = Colors.Blue;
        //    canvas.FillColor = Colors.Blue;
        //    canvas.FillCircle(XClient.secondPlayer, (double) 50); //second player
        //    canvas.StrokeColor = Colors.Black;
        //    canvas.FillColor = Colors.Black;
        //    canvas.FillCircle(XClient.puck, (double) 40);          //puck
    }
}
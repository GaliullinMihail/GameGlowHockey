using Glow_Hockey.Client;
using SharpHook;
using SharpHook.Reactive;
using System.Diagnostics;
using System.Reactive.Linq;
using XProtocol;
using XProtocol.Serializator;

namespace Glow_Hockey;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
		
		Task.Run((Action)SendCursorPackets);
	}

    private void PauseBtn_Clicked(object sender, EventArgs e)	//46 пикселей слева 46 пикселей справа 123 пикселя сверху
    {
		
    }

	private void SendCursorPackets()
	{
		var watch = new Stopwatch();
		watch.Start();
        var lastTime = watch.ElapsedMilliseconds;
        while (true)
		{
			if(XClient.GameIsPaused)	
			{
				Thread.Sleep(100);
				continue;
			}
			if (watch.ElapsedMilliseconds - lastTime > 10000)
			{
				var hook = new TaskPoolGlobalHook();
				//var hook = new SimpleReactiveGlobalHook();
				hook.MouseMoved += OnMouseMoved;
				//hook.MousePressed += OnMouseMoved;
				hook.Run();
				lastTime= watch.ElapsedMilliseconds;
			}
        }
		watch.Stop();
	}

	private void OnMouseMoved(object? sender, MouseHookEventArgs e)
	{
		var data = e.Data;
            XClient.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.SendCursorPosition, new XPacketCursor
                    {
                        Id = XClient.Id,
                        X = data.X,  //TODO: change to cursor pos
                        Y = data.Y   //TODO: change to cursor pos 
                    }).ToPacket());

    }


  //  private void backgorundClicked(object sender, TappedEventArgs e)
  //  {
		////var b = Entry.CursorPositionProperty;
		////b.
		//var b = background.GetPropertyIfSet<Point>(Entry.CursorPositionProperty, new Point(0, 0));
  //      XClient.QueuePacketSend(
  //              XPacketConverter.Serialize(
  //                  XPacketType.SendCursorPosition, new XPacketCursor
  //                  {
  //                      Id = XClient.Id,
  //                      X = (int) b.X,  //TODO: change to cursor pos
  //                      Y = (int) b.Y   //TODO: change to cursor pos 
  //                  }).ToPacket());
  //  }
}
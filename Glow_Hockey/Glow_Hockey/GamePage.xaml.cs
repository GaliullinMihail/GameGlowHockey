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
	private Stopwatch watch = new Stopwatch();
	public GamePage()
	{
        watch.Start();
        InitializeComponent();
		
		Task.Run((Action)SendCursorPackets);
	}

    private void PauseBtn_Clicked(object sender, EventArgs e)	//46 пикселей слева 46 пикселей справа 123 пикселя сверху
    {
		
    }

	private void SendCursorPackets()
	{
		var hook = new TaskPoolGlobalHook();
		//var hook = new SimpleReactiveGlobalHook();
		hook.MouseMoved += OnMouseMoved;
		//hook.MousePressed += OnMouseMoved;
		hook.Run();
        
	}

	private void OnMouseMoved(object? sender, MouseHookEventArgs e)
	{
		if (watch.ElapsedMilliseconds > 50)
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
			watch.Restart();
		}

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
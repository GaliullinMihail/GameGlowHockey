using Glow_Hockey.Client;
using SharpHook;
using SharpHook.Reactive;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Timers;
using XProtocol;
using XProtocol.Serializator;

namespace Glow_Hockey;

public partial class GamePage : ContentPage
{
	private Stopwatch watch = new Stopwatch();
	public static GraphicsView graphicsView;
	public GamePage()
	{
        watch.Start();
		graphicsView = BoardGraphicsView;
        InitializeComponent();
		Task.Run((Action)SendCursorPackets);
			
	}

    private void PauseBtn_Clicked(object sender, EventArgs e)
    {
		XClient.QueuePacketSend(
				XPacketConverter.Serialize(
					XPacketType.Pause,
					new XPacketWithoutInfo 
					{ 
					}).ToPacket());
    }

	private void SendCursorPackets()
	{	
		var hook = new TaskPoolGlobalHook();
		hook.MousePressed += OnMouseMoved;
		hook.Run();
        
	}

	private void OnMouseMoved(object? sender, MouseHookEventArgs e)
	{
		if (watch.ElapsedMilliseconds > 10 && !XClient.GameIsPaused)
		{
			var data = e.Data;
			XClient.QueuePacketSend(
				XPacketConverter.Serialize(
					XPacketType.SendCursorPosition, new XPacketCursor
					{
						Id = XClient.Id,
						X = data.X,  
						Y = data.Y   
					}).ToPacket());

            var graphicsView = BoardGraphicsView;

            graphicsView.Invalidate();
            watch.Restart();
		}
    }
}
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
		hook.MouseMoved += OnMouseMoved;
		hook.Run();
        
	}

	private void OnMouseMoved(object? sender, MouseHookEventArgs e)
	{
		if (watch.ElapsedMilliseconds > 50 && !XClient.GameIsPaused)
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
			watch.Restart();
		}
    }
}
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
	private Stopwatch watch1 = new Stopwatch();

    private Point lastFirstPlayerPos;	
    private Point lastSecondPlayerPos;
    private Point lastPuckPos;

	private int winScore = 2;

    public static GraphicsView graphicsView;
	public GamePage()
	{
        watch.Start();
		watch1.Start();
		graphicsView = BoardGraphicsView;
        InitializeComponent();
		Task.Run((Action)SendCursorPackets);
		Task.Run((Action)Redraw);
	}

	private void Redraw()
	{
		while (true)
		{
			if(XClient.firstPlayerScore >= winScore || XClient.secondPlayerScore >= winScore)
			{
				int winnerId = XClient.firstPlayerScore >= winScore ? 0 : 1;
				Dispatcher.Dispatch(() => WinOrLoseGame(winnerId));
				break;
			}

			Dispatcher.Dispatch(() => Score.Text = $"{XClient.firstPlayerScore}     -     {XClient.secondPlayerScore}");
			
			if (watch.ElapsedMilliseconds > 50 && IsNeedToRedraw)
			{
				Dispatcher.Dispatch(RedrawGame);
				lastFirstPlayerPos = XClient.firstPlayer;
				lastSecondPlayerPos = XClient.secondPlayer;
				lastPuckPos = XClient.puck;

				watch.Restart();
			}

		}
	}

	private bool IsNeedToRedraw => lastFirstPlayerPos.X != XClient.firstPlayer.X || lastFirstPlayerPos.Y != XClient.firstPlayer.Y ||
								   lastSecondPlayerPos.X != XClient.secondPlayer.X || lastSecondPlayerPos.Y != XClient.secondPlayer.Y ||
								   lastPuckPos.X != XClient.puck.X || lastPuckPos.Y != XClient.puck.Y;

	private void RedrawGame()
	{
        var graphicsView = BoardGraphicsView;
        graphicsView.Invalidate();
    }

	private void WinOrLoseGame(int winnerId)
	{
		PauseBtn.IsVisible = false;
		BoardGraphicsView.IsVisible = false;
		Score.Text = winnerId == XClient.Id ? "You Win" : "Enemy Win";
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
		if (!XClient.GameIsPaused && watch1.ElapsedMilliseconds > 100)
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

			watch1.Restart();
        }
    }
}
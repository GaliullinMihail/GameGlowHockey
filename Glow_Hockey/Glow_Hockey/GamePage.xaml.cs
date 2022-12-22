using Glow_Hockey.Client;
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
		while(true)
		{
			if(XClient.GameIsPaused)
			{
				Thread.Sleep(100);
				continue;
			}
			XClient.QueuePacketSend(
				XPacketConverter.Serialize(
					XPacketType.SendCursorPosition, new XPacketCursor
					{
						Id = XClient.Id,
						X = 1,	//TODO: change to cursor pos
						Y = 2	//TODO: change to cursor pos 
					}).ToPacket());

		}
	}
}
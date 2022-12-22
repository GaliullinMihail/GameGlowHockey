using Glow_Hockey.Client;
using XProtocol;
using XProtocol.Serializator;

namespace Glow_Hockey;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
        if (XClient.IsRegistered)
        {
            XClient.QueuePacketSend(
                    XPacketConverter.Serialize(
                        XPacketType.Register,
                        new XPacketWithoutInfo
                        {
                        }).ToPacket());
        }
        await Shell.Current.GoToAsync("GamePage");
	}
}


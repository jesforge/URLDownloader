using Avalonia;
using Avalonia.Controls;
using URLDownloader.Services;

namespace URLDownloader.Views;

public partial class MainWindow : Window
{
    private string _url;

    public MainWindow(string url)
    {
        InitializeComponent();

        Opened += (_, _) => MoveToBottomRight();
        
        _url = url;
        UrlText.Text = url;

        CancelButton.Click += (_, _) =>
        {
            Hide();
        };
        
        Mp4Button.Click += async (_, _) =>
        {
            await YoutubeDownloader.DownloadAsync(_url, "mp4");
            Hide();
        };

        Mp3Button.Click += async (_, _) =>
        {
            await YoutubeDownloader.DownloadAsync(_url, "mp3");
            Hide();
        };
    }
    
    private void MoveToBottomRight()
    {
        var screen = Screens.Primary;
        var workingArea = screen.WorkingArea;

        Position = new PixelPoint(
            workingArea.Right - (int)Bounds.Width - 20,
            workingArea.Bottom - (int)Bounds.Height - 20
        );
    }
    
}
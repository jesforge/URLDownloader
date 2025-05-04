using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using URLDownloader.Views;

namespace URLDownloader.Background;

public class ClipboardWatcher
{
    private string lastClipboardText = "";
    private Timer timer;
    private Window host;

    public ClipboardWatcher(Window Host)
    {
        host = Host;
        timer = new Timer(async _ => await CheckClipboard(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    private async Task CheckClipboard()
    {
        try
        {
            var text = await host.Clipboard?.GetTextAsync();

            if (!string.IsNullOrWhiteSpace(text) && text != lastClipboardText && IsYoutubeUrl(text))
            {
                lastClipboardText = text;

                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    var popup = new MainWindow(text);
                    popup.Show();
                });
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    private bool IsYoutubeUrl(string url)
    {
        return Regex.IsMatch(url, @"(https?:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+");
    }
}
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using URLDownloader.Background;

namespace URLDownloader.Views;

public partial class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var hiddenWindow = new Window();
            hiddenWindow.Hide();

            var clipboardWatcher = new ClipboardWatcher(hiddenWindow);
        }

        base.OnFrameworkInitializationCompleted();
    }


    private void Close_OnClick(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Console.WriteLine("Closing application");
            desktop.Shutdown();
        }
    }
}
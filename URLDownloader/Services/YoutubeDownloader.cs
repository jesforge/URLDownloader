using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace URLDownloader.Services;

public static class YoutubeDownloader
{
    public static async Task DownloadAsync(string url, string format)
    {
        var ytdl = new YoutubeDL();
        await YoutubeDLSharp.Utils.DownloadYtDlp();
        await YoutubeDLSharp.Utils.DownloadFFmpeg();

        var data = await ytdl.RunVideoDataFetch(url);
        var title = data.Data.Title;

        try
        {
            if (format == "mp4")
            {
                var res = await ytdl.RunVideoDownload(url);

                string path = res.Data;

                string outputPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads",
                        title + ".mp4");

                File.Move(path, outputPath);

                File.Delete(path);
            }
            else if (format == "mp3")
            {
                var res = await ytdl.RunAudioDownload(
                    url,
                    AudioConversionFormat.Mp3
                );

                string path = res.Data;

                string outputPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads",
                        title + ".mp3");

                File.Move(path, outputPath);

                File.Delete(path);
            }
        }
        catch (IOException ioEx)
        {
        }
        catch (Exception ex)
        {
        }
    }
}
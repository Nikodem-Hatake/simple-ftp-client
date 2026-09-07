using FluentFTP;
using FTP_client.Components.Controls;

namespace FTP_client.Components.Pages;

public partial class Home
{
    public ConnectionItemsComponent ConnectionItemsComponent { get; set; }
    private AsyncFtpClient _ftpClient = new();

    private async Task GetItemsAsync() => await ConnectionItemsComponent.GetItemsAsync();

    private async Task OnPathPicked(string path) => await ConnectionItemsComponent.OnPathPicked(path);
}
using FluentFTP;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class ItemsManagingButtonsComponent(ISnackbar snackbar)
{
    [Parameter]
    public AsyncFtpClient FTPClient { get; set; }

    [Parameter]
    public FtpListItem SelectedItem { get; set; } = new();

    private ISnackbar _snackbar = snackbar;

    public async Task OnDownloadButtonPressed()
    {
        if(string.IsNullOrEmpty(SelectedItem.Name))
        {
            _snackbar.Add("Select item to download first.");
            return;
        }
        else if(SelectedItem.Type == FtpObjectType.Directory)
        {
            _snackbar.Add("Select file only.");
            return;
        }

        try
        {
            await FTPClient.DownloadFile(Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.MyDocuments), SelectedItem.Name), SelectedItem.Name);
            _snackbar.Add("Downloaded successfully");
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}");
        }
    }
}
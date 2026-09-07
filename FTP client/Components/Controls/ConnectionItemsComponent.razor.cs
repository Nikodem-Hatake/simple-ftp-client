using FluentFTP;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class ConnectionItemsComponent(ISnackbar snackbar)
{
    [Parameter]
    public AsyncFtpClient FTPClient { get; set; }

    private FtpListItem[] _items = [];
    private bool _isLoading;
    private ItemsManagingButtonsComponent _itemsManagingButtonsComponent;
    private FtpListItem _selectedItem = new();
    private ISnackbar _snackbar = snackbar;

    public async Task ChangeWorkingDirectory(FtpListItem item)
    {
        try
        {
            await FTPClient.SetWorkingDirectory(item.FullName);
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }
        await GetItemsAsync();
    }

    public async Task GetItemsAsync()
    {
        _isLoading = true;
        try
        {
            _items = await FTPClient.GetListing(await FTPClient.GetWorkingDirectory());
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }
        _itemsManagingButtonsComponent.IsConnected = FTPClient.IsConnected;
        _isLoading = false;
    }

    public async Task OnPathPicked(string path) => _itemsManagingButtonsComponent.DownloadPath = path;

    private string SelectedRowClassFunc(FtpListItem item, int rowNumber)
    {
        if(ReferenceEquals(item, _selectedItem))
        {
            return "selected";
        }
        return string.Empty;
    }
}
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
    private FtpListItem _selectedItem = new();
    private ISnackbar _snackbar = snackbar;

    public async Task GetItemsAsync()
    {
        _isLoading = true;
        try
        {
            _items = await FTPClient.GetListing();
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}");
        }
        _isLoading = false;
    }

    private string SelectedRowClassFunc(FtpListItem item, int rowNumber)
    {
        if(ReferenceEquals(item, _selectedItem))
        {
            return "selected";
        }
        return string.Empty;
    }
}
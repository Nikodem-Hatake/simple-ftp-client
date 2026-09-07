using FluentFTP;
using FTP_client.Components.Controls.ConnectionProfiles;
using FTP_client.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class ItemsManagingButtonsComponent(ISnackbar snackbar, IDialogService dialogService, SQLiteDB SQLiteDB)
{
    private IDialogService _dialogService = dialogService;
    public string DownloadPath { get; set; } = string.Empty;

    [Parameter]
    public AsyncFtpClient FTPClient { get; set; }

    public bool IsConnected { get; set; }

    [Parameter]
    public EventCallback OnItemsChangeEventCallback { get; set; }

    [Parameter]
    public FtpListItem SelectedItem { get; set; } = new();

    private ISnackbar _snackbar = snackbar;

    private async Task ChangeWorkingDirectoryToPrevious()
    {
        try
        {
            await FTPClient.SetWorkingDirectory(Path.GetDirectoryName(await FTPClient.GetWorkingDirectory()));
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }

        await OnItemsChangeEventCallback.InvokeAsync();
    }

    private async Task CreateNewFolder()
    {
        var result = await (await _dialogService.ShowAsync<SaveConnectionProfileComponent>("Create new directory")).Result;
        if(result.Canceled || result.Data is null)
        {
            return;
        }

        try
        {
            await FTPClient.CreateDirectory(result.Data as string);
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }

        _snackbar.Add("Created successfully", Severity.Success);
        await OnItemsChangeEventCallback.InvokeAsync();
    }

    private async Task DeleteItem()
    {
        if(string.IsNullOrEmpty(SelectedItem.Name))
        {
            _snackbar.Add("Select item to delete first.", Severity.Warning);
            return;
        }

        try
        {
            if(SelectedItem.Type == FtpObjectType.Directory)
            {
                await FTPClient.DeleteDirectory(SelectedItem.Name);
            }
            else
            {
                await FTPClient.DeleteFile(SelectedItem.Name);
            }
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }

        _snackbar.Add("Deleted successfully", Severity.Success);
        await OnItemsChangeEventCallback.InvokeAsync();
    }

    private async Task DownloadFile()
    {
        if(string.IsNullOrEmpty(SelectedItem.Name))
        {
            _snackbar.Add("Select item to download first.", Severity.Warning);
            return;
        }
        else if(SelectedItem.Type == FtpObjectType.Directory)
        {
            _snackbar.Add("Select file only.", Severity.Warning);
            return;
        }

        try
        {
            await FTPClient.DownloadFile(Path.Combine(DownloadPath, SelectedItem.Name), SelectedItem.Name);
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }

        _snackbar.Add("Downloaded successfully", Severity.Success);
        await OnItemsChangeEventCallback.InvokeAsync();
    }

    private async Task EditItemName()
    {
        var result = await (await _dialogService.ShowAsync<SaveConnectionProfileComponent>("Edit item name")).Result;
        if(result.Canceled || result.Data is null)
        {
            return;
        }

        try
        {
            await FTPClient.Rename(SelectedItem.Name, result.Data as string);
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }

        _snackbar.Add("Renamed successfully", Severity.Success);
        await OnItemsChangeEventCallback.InvokeAsync();
    }

    private async Task Refresh()
    {
        await OnItemsChangeEventCallback.InvokeAsync();
        IsConnected = FTPClient.IsConnected;
    }

    protected override async Task OnInitializedAsync()
    {
        IsConnected = FTPClient.IsConnected;
        try
        {
            DownloadPath = await SQLiteDB.GetDownloadsPathAsync();
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }
    }
}
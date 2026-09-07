using CommunityToolkit.Maui.Storage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class DownloadPathPickerComponent(ISnackbar snackbar, SQLiteDB SQLiteDB)
{
    private string _path = string.Empty;

    [Parameter]
    public EventCallback<string>OnPathPickedEventCallback { get; set; }

    private ISnackbar _snackbar = snackbar;

    private async Task ChangePathAsync()
    {
        try
        {
            var result = await FolderPicker.PickAsync(_path);
            _path = result.Folder.Path;
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
            return;
        }

        await OnPathPickedEventCallback.InvokeAsync(_path);
        await SQLiteDB.SetDownloadsPathAsync(_path);
    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _path = await SQLiteDB.GetDownloadsPathAsync();
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }
    }
}
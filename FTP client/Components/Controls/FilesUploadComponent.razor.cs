using FluentFTP;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class FilesUploadComponent(ISnackbar snackbar)
{
    private IReadOnlyList<IBrowserFile> _filesToUpload = [];
    public MudFileUpload<IReadOnlyList<IBrowserFile>> FileUploader;

    [Parameter]
    public AsyncFtpClient FTPClient { get; set; }

    private bool _isFilesUploading;

    [Parameter]
    public EventCallback OnUploadedEventCallback { get; set; }

    private ISnackbar _snackbar = snackbar;
    private double _uploadProgress;
    private string _uploadProgressText = string.Empty;

    public async Task OnUploadButtonPressed()
    {
        _isFilesUploading = true;
        _uploadProgress = 0d;
        _uploadProgressText = $"Uploading 0 of {_filesToUpload.Count} files";

        int i = 0;
        try
        {
            for(; i < _filesToUpload.Count; ++i)
            {
                using var stream = _filesToUpload[i].OpenReadStream();
                await FTPClient.UploadStream(stream, _filesToUpload[i].Name);
                _uploadProgress = (double)(i + 1) / (double)_filesToUpload.Count * 100d;
                _uploadProgressText = $"Uploading {i + 1} of {_filesToUpload.Count} files";
                StateHasChanged();
            }
            _uploadProgressText = "Uploaded";
        }
        catch(Exception e)
        {
            _uploadProgressText = $"Uploading failed on file named: {_filesToUpload[i].Name}";
            _snackbar.Add($"Error: {e.Message}");
        }

        await OnUploadedEventCallback.InvokeAsync();
        _filesToUpload = [];
        _isFilesUploading = false;
    }

    public async Task RemoveFileFromUploadFilesAsync(IBrowserFile file) => await FileUploader.RemoveFileAsync(file);
}
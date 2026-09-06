using FluentFTP;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class ConnectionFormComponent(ISnackbar snackbar)
{
    [Parameter]
    public AsyncFtpClient FTPClient { get; set; }

    private bool _isFormDisabled;

    [Parameter]
    public EventCallback OnSuccessfullConnectionEventCallback { get; set; }

    private string _password = string.Empty;
    private string _serverAddress = string.Empty;
    private ISnackbar _snackbar = snackbar;
    private string _userName = string.Empty;

    private async Task Connect()
    {
        _isFormDisabled = true;

        FTPClient.Host = _serverAddress;
        FTPClient.Credentials.UserName = _userName;
        FTPClient.Credentials.Password = _password;

        try
        {
            await FTPClient.Connect();
            await OnSuccessfullConnectionEventCallback.InvokeAsync();
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}");
        }

        _isFormDisabled = false;
    }
}
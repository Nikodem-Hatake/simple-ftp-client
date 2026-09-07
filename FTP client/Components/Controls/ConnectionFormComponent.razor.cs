using FluentFTP;
using FTP_client.Components.Controls.ConnectionProfiles;
using FTP_client.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls;

public partial class ConnectionFormComponent(ISnackbar snackbar, IDialogService dialogService, SQLiteDB SQLiteDB)
{
    private ConnectionParameters _connectionParameters = new();
    private IDialogService _dialogService = dialogService;

    [Parameter]
    public AsyncFtpClient FTPClient { get; set; }

    private bool _isFormDisabled;

    [Parameter]
    public EventCallback OnSuccessfullConnectionEventCallback { get; set; }

    private ISnackbar _snackbar = snackbar;
    private SQLiteDB _SQLiteDB = SQLiteDB;

    private async Task Connect()
    {
        _isFormDisabled = true;

        FTPClient.Host = _connectionParameters.Host;
        FTPClient.Port = _connectionParameters.Port;
        FTPClient.Credentials.UserName = _connectionParameters.UserName;
        FTPClient.Credentials.Password = _connectionParameters.Password;

        try
        {
            await FTPClient.Connect();
            await OnSuccessfullConnectionEventCallback.InvokeAsync();
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }

        _isFormDisabled = false;
    }

    private async Task DeleteConnectionProfile()
    {
        var result = await (await _dialogService.ShowAsync<DeleteConnectionProfileComponent>("Delete connection profile")).Result;
        if(result.Canceled || result.Data is null)
        {
            return;
        }

        try
        {
            await _SQLiteDB.DeleteConnectionParametersAsync(result.Data as ConnectionParameters);
            _snackbar.Add("Connection profile deleted", Severity.Success);
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }
    }

    private async Task LoadConnectionProfile()
    {
        var result = await (await _dialogService.ShowAsync<LoadConnectionProfileComponent>("Load connection profile")).Result;
        if(!result.Canceled && result.Data is not null)
        {
            _connectionParameters = result.Data as ConnectionParameters;
        }
    }

    private async Task SaveConnectionProfile()
    {
        var result = await (await _dialogService.ShowAsync<SaveConnectionProfileComponent>("Save connection profile")).Result;
        if(result.Canceled || string.IsNullOrWhiteSpace(result.Data as string))
        {
            return;
        }

        _connectionParameters.Name = result.Data as string;
        try
        {
            if(await _SQLiteDB.AddConnectionParametersAsync(_connectionParameters))
            {
                _snackbar.Add("Connection profile saved", Severity.Success);
            }
            else
            {
                _snackbar.Add("Connection with same name already exists", Severity.Warning);
            }
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }
    }
}
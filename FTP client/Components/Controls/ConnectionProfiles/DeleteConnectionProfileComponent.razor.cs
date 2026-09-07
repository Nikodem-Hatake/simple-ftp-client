using FTP_client.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls.ConnectionProfiles;

public partial class DeleteConnectionProfileComponent(ISnackbar snackbar, SQLiteDB SQLiteDB)
{
    private ConnectionParameters _connectionParameters;
    private List<ConnectionParameters> _connectionParametersList = [];

    [CascadingParameter]
    public IMudDialogInstance MudDialog { get; set; }

    private ISnackbar _snackbar = snackbar;
    private SQLiteDB _SQLiteDB = SQLiteDB;

    private void Close() => MudDialog.Close(DialogResult.Ok(_connectionParameters));

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _connectionParametersList = await _SQLiteDB.GetConnectionParametersAsync();
            StateHasChanged();
        }
        catch(Exception e)
        {
            _snackbar.Add($"Error: {e.Message}", Severity.Error);
        }
    }
}
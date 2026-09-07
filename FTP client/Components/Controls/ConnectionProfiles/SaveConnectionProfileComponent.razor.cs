using FTP_client.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FTP_client.Components.Controls.ConnectionProfiles;

public partial class SaveConnectionProfileComponent
{
    private string _name = string.Empty;

    [CascadingParameter]
    public IMudDialogInstance MudDialog { get; set; }

    private void Close() => MudDialog.Close(DialogResult.Ok(_name));
}
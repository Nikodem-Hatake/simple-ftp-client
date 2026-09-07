using SQLite;

namespace FTP_client.Models;

public class DownloadsPath
{
    [PrimaryKey]
    public int Id { get; set; }

    public string Path { get; set; } = string.Empty;
}
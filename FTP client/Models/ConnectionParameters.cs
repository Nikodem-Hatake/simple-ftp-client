using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTP_client.Models;

public class ConnectionParameters
{
    public string Host { get; set; } = string.Empty;

    [PrimaryKey]
    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public int Port { get; set; } = 21;
    public string UserName { get; set; } = string.Empty;
}
using FluentFTP;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTP_client.Converters;

public static class ItemIconConverter
{
    public static string Convert(FtpObjectType type)
    {
        switch(type)
        {
            case FtpObjectType.File:
            {
                return Icons.Material.Filled.InsertDriveFile;
            }
            default:
            {
                return Icons.Material.Filled.Link;
            }
        }
    }
}
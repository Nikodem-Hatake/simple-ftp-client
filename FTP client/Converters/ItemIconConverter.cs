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
            case FtpObjectType.Directory:
            {
                return Icons.Material.Filled.Folder;
            }
            default:
            {
                return Icons.Material.Filled.Link;
            }
        }
    }
}
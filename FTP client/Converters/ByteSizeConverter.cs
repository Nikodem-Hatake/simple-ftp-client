using System;
using System.Collections.Generic;
using System.Text;

namespace FTP_client.Converters;

public static class ByteSizeConverter
{
    private const long ONE_GIGABYTE = 1073741824;
    private const long ONE_MEGABYTE = 1048576;
    private const long ONE_KERABYTE = 1024;

    public static string Convert(long bytes)
    {
        if(bytes >= ONE_GIGABYTE)
        {
            return $"{Math.Round((double)bytes / ONE_GIGABYTE, 2)} GB";
        }
        else if(bytes >= ONE_MEGABYTE)
        {
            return $"{Math.Round((double)bytes / ONE_MEGABYTE, 2)} MB";
        }
        else if(bytes >= ONE_KERABYTE)
        {
            return $"{Math.Round((double)bytes / ONE_KERABYTE, 2)} KB";
        }
        else
        {
            return $"{bytes} B";
        }
    }
}
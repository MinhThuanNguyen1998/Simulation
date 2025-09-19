using System.IO;
using System.Linq;
using UnityEngine;

public class FolderNameValidator 
{
    private static readonly string[] ReservedNames =
    {
        "CON", "PRN", "AUX", "NUL",
        "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
        "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
    };
    public static bool Validate(string inputName, out string validName, out string errorMessage)
    {
        validName = inputName?.Trim();
        errorMessage = null;
        // Check null
        if (string.IsNullOrEmpty(validName))
        {
            errorMessage = Config.Text_Bo_Trong_Ten;
            return false;
        }
        // Check valid
        char[] invalidChars = Path.GetInvalidFileNameChars();
        if (validName.IndexOfAny(invalidChars) >= 0)
        {
            errorMessage = Config.Text_Ky_Tu_Khong_Hop_le;
            return false;
        }
        // Check reserved names
        if (ReservedNames.Contains(validName.ToUpper()))
        {
            errorMessage = Config.Text_Ky_Tu_Khong_Hop_le;
            return false;
        }
        // Check length
        if (validName.Length > 13)
            validName = validName.Substring(0, 13) + "...";
        return true;
    }
}

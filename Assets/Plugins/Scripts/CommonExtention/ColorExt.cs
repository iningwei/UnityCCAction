using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExt
{
    /// <summary>
    /// Converts int between 0 and 15 to hexidecimal character.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    static string GetHex(int v)
    {
        char[] alpha = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        string str = "" + alpha[v];
        return str;
    }


    /// <summary>
    /// Converts single hexadecimal character (0…F) to the corresponding int.
    /// </summary>
    /// <param name="hexChar"></param>
    /// <returns></returns>
    static int HexToInt(char hexChar)
    {
        string hex = "" + hexChar;
        switch (hex)
        {
            case "0": return 0;
            case "1": return 1;
            case "2": return 2;
            case "3": return 3;
            case "4": return 4;
            case "5": return 5;
            case "6": return 6;
            case "7": return 7;
            case "8": return 8;
            case "9": return 9;
            case "A":
            case "a":
                return 10;
            case "B":
            case "b":
                return 11;
            case "C":
            case "c":
                return 12;
            case "D":
            case "d":
                return 13;
            case "E":
            case "e":
                return 14;
            case "F":
            case "f":
                return 15;
            default:
                throw new System.Exception();
        }
    }

    /// <summary>
    /// Convert standard web-formatted (hexadecimal) color (000000…FFFFFF) to RGB color with values in 0…1 range.
    /// </summary>
    /// <param name="hexStr"></param>
    /// <returns></returns>
    public static Color HexToRGB(this string hexStr)
    {
        char[] color = hexStr.ToCharArray();
        float red = (HexToInt(color[1]) + HexToInt(color[0]) * 16f) / 255f;
        float green = (HexToInt(color[3]) + HexToInt(color[2]) * 16f) / 255f;
        float blue = (HexToInt(color[5]) + HexToInt(color[4]) * 16f) / 255f;
        var finalColor = new Color();
        finalColor.r = red;
        finalColor.g = green;
        finalColor.b = blue;
        return finalColor;

    }

    /// <summary>
    /// Convert a Unity color to hexadecimal. NOTE: this function assumes that color values are in the 0…1 range. Alpha values are ignored.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string RGBToHex(this Color color)
    {
        float red = color.r * 255;
        float green = color.g * 255;
        float blue = color.b * 255;

        string a = GetHex((int)Math.Floor(red / 16f));
        string b = GetHex((int)Math.Round(red % 16f));
        string c = GetHex((int)Math.Floor(green / 16f));
        string d = GetHex((int)Math.Round(green % 16f));
        string e = GetHex((int)Math.Floor(blue / 16f));
        string f = GetHex((int)Math.Round(blue % 16f));

        string z = a + b + c + d + e + f;
        return z;
    }
}

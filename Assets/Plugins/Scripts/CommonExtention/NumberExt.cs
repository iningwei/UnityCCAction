using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberExt
{
    public static string FormatMK(this float value)
    {
        return FormatMK((int)value);
    }
    public static string FormatMK(this int value)
    {
        string formatStr;
        if (value < 1000)
        {
            formatStr = value.ToString();
        }
        else if (value >= 1000 && value < 1000000)
        {
            formatStr = (value / 1000f).ToString("f1") + "K";
        }
        else
        {
            formatStr = (value / 1000f).ToString("f1") + "M";
        }

        return formatStr;
    }
}

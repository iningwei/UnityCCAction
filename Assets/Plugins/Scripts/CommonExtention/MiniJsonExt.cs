using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MiniJsonExt
{
    public static List<string> ToListString(this List<object> old)
    {
        List<string> target = new List<string>();
        if (old.Count > 0)
        {
            for (int i = 0; i < old.Count; i++)
            {
                target.Add((string)old[i]);
            }
        }
        return target;
    }

    //public static float ToFloat(this string str)
    //{

    //    float f = 0f;
    //    if (str.IndexOf('.') == -1 && str.IndexOf('E') == -1 && str.IndexOf('e') == -1)
    //    {
    //        f = float.Parse(str);
    //    }
    //    else
    //    {
    //      f= (float)(double)str;
    //    }

    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExt
{
    public static int ToInt(this object objValue)
    {
        int intValue = 0;
        try
        {
            intValue = int.Parse(objValue.ToString());
        }
        catch (System.Exception ex)
        {

            Debug.LogError("error, ex:" + ex.Message);
        }

        return intValue;

    }

    public static float ToFloat(this object objValue)
    {
        float floatValue = 0;
        try
        {
            floatValue = float.Parse(objValue.ToString());
        }
        catch (System.Exception ex)
        {

            Debug.LogError("error, ex:" + ex.Message);
        }

        return floatValue;
    }
}

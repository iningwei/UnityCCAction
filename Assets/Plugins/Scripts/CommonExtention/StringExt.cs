using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExt
{
    /// <summary>
    /// 扩展 获取变量名称(字符串)
    /// //PS:C# 6.0可以使用 nameof(变量) 来获得变量的名字
    /// </summary>
    /// <param name="var_name"></param>
    /// <param name="exp"></param>
    /// <returns>return string</returns>
    public static string GetVarName<T>(this T var_name, System.Linq.Expressions.Expression<Func<T>> exp)
    {
        return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
    }

    /// <summary>
    /// 判断字符串是否包含汉字
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool ContainChinese(this string text)
    {
        bool res = false;
        foreach (char t in text)
        {
            if ((int)t > 127)
                res = true;
        }
        return res;
    }


    //反斜杠\ backslash
    //斜杠/  slash
    /// <summary>
    /// 归一化路径中的斜杠或反斜杠为斜杠
    /// </summary>
    /// <param name="inputStr"></param>
    /// <returns></returns>
    public static string UniformSlash(this string inputStr)
    {
        return inputStr.Replace('\\', '/');
    }


    public static int[] ToIntArray(this string[] strArray)
    {
        if (strArray == null || strArray.Length == 0)
        {
            Debug.LogError("error, input stringArray is null or length=0");
        }
        int[] result = new int[strArray.Length];
        for (int i = 0; i < strArray.Length; i++)
        {
            try
            {
                result[i] = int.Parse(strArray[i]);
            }
            catch (Exception ex)
            {

                Debug.LogError("StringArrayToIntArray exception:" + ex.Message);
            }

        }
        return result;
    }


    public static float[] ToFloatArray(this string[] strArray)
    {
        if (strArray == null || strArray.Length == 0)
        {
            Debug.LogError("error, input stringArray is null or length=0");
        }
        float[] result = new float[strArray.Length];
        for (int i = 0; i < strArray.Length; i++)
        {
            try
            {
                result[i] = float.Parse(strArray[i]);
            }
            catch (Exception ex)
            {

                Debug.LogError("StringArrayToFloatArray exception:" + ex.Message);
            }

        }
        return result;
    }
}

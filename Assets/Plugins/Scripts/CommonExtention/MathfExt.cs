using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathfExt
{

    /// <summary>
    /// 根据直角三角形两直角边，获得其斜边长度
    /// </summary>
    /// <param name="cathetus1"></param>
    /// <param name="cathetus2"></param>
    /// <returns></returns>
    public static float GetRightTriangleHypotenuseLength(float cathetus1, float cathetus2)
    {
        if (cathetus1 <= 0 || cathetus2 <= 0)
        {
            Debug.LogError("error input of cathethus");
        }
        return Mathf.Sqrt(cathetus1 * cathetus1 + cathetus2 * cathetus2);
    }
}

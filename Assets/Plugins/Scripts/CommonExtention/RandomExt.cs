using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExt
{
    static int randomTimes = 0;
    static int lastValue = 0;

    static System.Random ran = new System.Random();

    /// <summary>
    /// 允许min值大于max，最终产生的数在一个 左闭右开 的区间
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static int GenerateValue(int v1, int v2)
    {
        if (v1 > v2)
        {
            int tmp = v1;
            v1 = v2;
            v2 = tmp;
        }
        if (v1 == v2)
        {
            Debug.LogError("error, generateValue input is not valid");
            return v1;
        }
        int seed = DateTime.Now.Millisecond + randomTimes + lastValue;
        //Debug.Log("min:" + min + ",max:" + max + ",seed:" + seed);
        int ran = new System.Random(seed).Next(v1, v2);
        randomTimes++;
        lastValue = ran;
        return ran;
    }


    /// <summary>
    /// 产生浮点型随机数
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="precision">精度，默认值为0，默认值情况下，根据输入的浮点数的小数点数来确定精度</param>
    /// <returns></returns>
    public static float GenerateValue(float v1, float v2, UInt16 precision = 0)
    {
        int L;
        if (precision == 0)
        {
            //获得v1,v2小数点后的位数
            string v1Str = v1.ToString();
            string v2Str = v2.ToString();
            int L1 = 0;
            if (v1Str.IndexOf('.') != -1)
            {
                L1 = v1Str.Length - v1Str.IndexOf('.') - 1;
            }

            int L2 = 0;
            if (v2Str.IndexOf('.') != -1)
            {
                L2 = v2Str.Length - v2Str.IndexOf('.') - 1;
            }
            L = L1 > L2 ? L1 : L2;
        }
        else
        {
            L = precision;
        }

        int v1New = (int)(v1 * Math.Pow(10, L));
        int v2New = (int)(v2 * Math.Pow(10, L));
        int ran = GenerateValue(v1New, v2New);

        return (float)(ran / (Math.Pow(10, L)));
    }

}

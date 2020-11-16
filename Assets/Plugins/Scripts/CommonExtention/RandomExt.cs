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
            Debug.LogWarning("error, generateValue input is not valid, v1:" + v1 + ", v2:" + v2);
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


    public static bool Ratio(float r)
    {
        if (r < 0 || r > 1)
        {
            Debug.LogError("error, r should [0,1]");
            return false;
        }
        int v = 0;
        int b = 1;
        if (r >= 0.01 && r <= 1)
        {
            b = 100;
            v = (int)(r * b);
        }
        else if (r >= 0.001 && r < 0.01)
        {
            b = 1000;
            v = (int)(r * b);
        }
        else if (r >= 0.0001 && r < 0.001)
        {
            b = 10000;
            v = (int)(r * b);
        }
        else
        {
            Debug.LogError("r should at least>=0.0001");
        }

        int randomV = GenerateValue(0, b);
        //Debug.LogError("randomV:" + randomV + ", v:" + v);
        if (randomV < v)
        {
            return true;
        }
        return false;
    }

}

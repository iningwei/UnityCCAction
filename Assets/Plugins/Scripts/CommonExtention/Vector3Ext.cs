using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Ext
{
    public static Vector3 ZeroZAxis(this Vector3 inputVec)
    {
        return new Vector3(inputVec.x, inputVec.y, 0);
    }
}

/*
 * file TransformExtension.cs
 */

using System;
using UnityEngine;
/// <summary>
/// Transform扩展方法
/// </summary>
public static class TransformExt
{

    public static void DestroyAllChilds(this Transform transform, bool destroyImmediate,Action beforeDestroyFunc=null)
    {
        int count = transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            if (beforeDestroyFunc != null)
            {
                beforeDestroyFunc();
            }

            if (destroyImmediate)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
            else
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    public static string Hierarchy(this Transform transform)
    {
        Transform parent = transform.parent;
        string hierarchyStr = transform.name;
        while (parent != null)
        {
            hierarchyStr = parent.name + "/" + hierarchyStr;
            parent = parent.parent;
        }
        return hierarchyStr;
    }
    public static Component FindComponent(this Transform transform, Type type, string name, bool includeInactive)
    {
        Component[] targets = transform.GetComponentsInChildren(type, includeInactive);
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].name == name)
                {
                    return targets[i];
                }
            }
        }
        return null;
    }


    public static T FindChildComponent<T>(this Transform transform, string path) where T : Component
    {
        T t = null;
        var child = transform.Find(path);
        if (child != null)
        {
            t = child.GetComponent<T>();
        }
        if (t == null)
        {
            Debug.LogError("error ,FindChild of type:" + t.GetType().Name + " failed, path:" + path);
        }
        return t;
    }


    #region 仿射变换      

    #region position


    /// <summary>
    /// 设置X坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void setX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// 设置Y坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void setY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    /// <summary>
    /// 设置Z坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void setZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    /// <summary>
    /// 设置X、Y坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void setXY(this Transform transform, float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    /// <summary>
    /// 设置Y、Z坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void setYZ(this Transform transform, float y, float z)
    {
        transform.position = new Vector3(transform.position.x, y, z);
    }

    /// <summary>
    /// 设置X、Z坐标
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void setXZ(this Transform transform, float x, float z)
    {
        transform.position = new Vector3(x, transform.position.y, z);
    }

    /// <summary>
    /// 沿着X轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void translateX(this Transform transform, float x)
    {
        transform.position += new Vector3(x, 0, 0);
    }

    /// <summary>
    /// 沿着Y轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void translateY(this Transform transform, float y)
    {
        transform.position += new Vector3(0, y, 0);
    }

    /// <summary>
    /// 沿着Z轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void translateZ(this Transform transform, float z)
    {
        transform.position += new Vector3(0, 0, z);
    }

    /// <summary>
    /// 沿着X、Y轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void translateXY(this Transform transform, float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }

    /// <summary>
    /// 沿着X、Z轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void translateXZ(this Transform transform, float x, float z)
    {
        transform.position += new Vector3(x, 0, z);
    }

    /// <summary>
    /// 沿着Y、Z轴移动
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void translateYZ(this Transform transform, float y, float z)
    {
        transform.position += new Vector3(0, y, z);
    }

    /// <summary>
    /// 将X、Y、Z都设为0
    /// </summary>
    /// <param name="transform"></param>
    public static void resetPosition(this Transform transform)
    {
        transform.position = Vector3.zero;
    }

    public static void ResetTransfrom(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        //transform.setLocalRotation(0, 0, 0);
    }

    /// <summary>
    /// 设置X坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void setLocalX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    /// <summary>
    /// 设置Y坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void setLocalY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    /// <summary>
    /// 设置Z坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void setLocalZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }

    /// <summary>
    /// 设置X、Y坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void setLocalXY(this Transform transform, float x, float y)
    {
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
    }

    /// <summary>
    /// 设置Y、Z坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void setLocalYZ(this Transform transform, float y, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, z);
    }

    /// <summary>
    /// 设置X、Z坐标(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>

    public static void setLocalXZ(this Transform transform, float x, float z)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, z);
    }

    public static void setLocajsw(this Transform transform, float x, float z)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, z);
    }

    /// <summary>
    /// local
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="direction"></param>
    public static void translateLocal(this Transform transform, Vector3 direction)
    {
        transform.localPosition += direction;
    }

    /// <summary>
    /// 沿着X轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void translateLocalX(this Transform transform, float x)
    {
        transform.localPosition += new Vector3(x, 0, 0);
    }

    /// <summary>
    /// 沿着Y轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    public static void translateLocalY(this Transform transform, float y)
    {
        transform.localPosition += new Vector3(0, y, 0);
    }

    /// <summary>
    /// 沿着Z轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="z"></param>
    public static void translateLocalZ(this Transform transform, float z)
    {
        transform.localPosition += new Vector3(0, 0, z);
    }

    /// <summary>
    /// 沿着X、Y轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void translateLocalXY(this Transform transform, float x, float y)
    {
        transform.localPosition += new Vector3(x, y, 0);
    }

    /// <summary>
    /// 沿着X、Z轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void translateLocajsw(this Transform transform, float x, float z)
    {
        transform.localPosition += new Vector3(x, 0, z);
    }

    /// <summary>
    /// 沿着Y、Z轴移动(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void translateLocalYZ(this Transform transform, float y, float z)
    {
        transform.localPosition += new Vector3(0, y, z);
    }

    /// <summary>
    /// 将X、Y、Z都设为0
    /// </summary>
    /// <param name="transform"></param>
    public static void resetLocalPosition(this Transform transform)
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
    #endregion

    #region Scale









    /// <summary>
    /// 设置为原始大小（local）
    /// </summary>
    /// <param name="transform"></param>
    public static void resetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }



    #endregion

    #region Flip






    #endregion

    #region Rotation
    public static void setRotation(this Transform transform, Vector3 angle)
    {
        transform.eulerAngles = angle;
    }

    /// <summary>
    /// 设置x方向旋转角度
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setRotationX(this Transform transform, float angle)
    {
        transform.eulerAngles = new Vector3(angle, 0, 0);
    }

    /// <summary>
    /// 设置Y方向旋转角度
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setRotationY(this Transform transform, float angle)
    {
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    /// <summary>
    /// 设置Z方向旋转角度
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setRotationZ(this Transform transform, float angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public static void setLocalRotation(this Transform transform, float x = 0, float y = 0, float z = 0)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(x, y, z));
    }

    /// <summary>
    /// 设置x方向旋转角度(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setLocalRotationX(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
    }



    /// <summary>
    /// 设置Y方向旋转角度(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setLocalRotationY(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    /// <summary>
    /// 设置Z方向旋转角度(local)
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void setLocalRotationZ(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// 沿着X轴旋转
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void rotateX(this Transform transform, float angle)
    {
        transform.Rotate(new Vector3(angle, 0, 0));
    }

    /// <summary>
    /// 沿着Y轴旋转
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void rotateY(this Transform transform, float angle)
    {
        transform.Rotate(new Vector3(0, angle, 0));
    }

    /// <summary>
    /// 沿着Z轴旋转
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="angle"></param>
    public static void rotateZ(this Transform transform, float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }
    #endregion

    public static Transform[] getChilds(this Transform transform)
    {
        return transform.GetComponentsInChildren<Transform>();
    }

    public static void resetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void resetGlobal(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void ResetParent(this Transform ts, Transform parent)
    {
        if (parent != null)
            ts.SetParent(parent.transform);
        ts.localPosition = Vector3.zero;
        ts.localRotation = Quaternion.identity;
        ts.localScale = Vector3.one;
    }
    public static void ResetParent(this Transform ts, Transform parent, Vector3 pos)
    {
        if (parent != null)
            ts.SetParent(parent.transform);
        ts.localPosition = pos;
        ts.localRotation = Quaternion.identity;
        ts.localScale = Vector3.one;
    }


    #endregion
}


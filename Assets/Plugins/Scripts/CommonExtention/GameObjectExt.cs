
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// GameObject扩展类
/// </summary>
public static class GameObjectExt
{
    /// <summary>
    /// 获得一个组件，不存在则添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T ret = go.GetComponent<T>();
        if (ret == null)
            ret = go.AddComponent<T>();
        return ret;
    }

    public static T GetComponent<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        return comp;
    }

    /// <summary>
    /// 上溯到root，获得路径
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="root"></param>
    /// <param name="isIncludeRoot">路径是否包括root节点</param>
    /// <returns></returns>
    public static string GetUpperPath(this GameObject gameObject, GameObject root, bool isIncludeRoot = false)
    {

        if (root == gameObject)
        {
            Debug.LogError("GetUpperPath error, root is self");
            return "";
        }

        if (gameObject.transform.parent.gameObject == root)
        {
            if (isIncludeRoot)
            {
                return root.name + "/" + gameObject.name;
            }
            {
                return gameObject.name;
            }
        }
        else
        {
            return GetUpperPath(gameObject.transform.parent.gameObject, root, isIncludeRoot) + "/" + gameObject.name;
        }
    }

    /// <summary>
    /// 根据具体路径获得子物体
    /// </summary>
    /// <param name="path">具体路径</param>
    /// <returns></returns>
    public static GameObject FindChild(this GameObject gameObject, string path)
    {
        var child = gameObject.transform.Find(path);
        if (child == null)
        {
            Debug.LogError("error, not findChild:" + path);
        }
        return child.gameObject;
    }

    //lua侧若调用obj.transform.position=CS.UnityEngine.Vector3(x,y,z);进行赋值的话，是一个很耗的操作
    //通过这种包装，可以大幅度降低消耗
    public static void SetPosition(this GameObject gameObject, float x, float y, float z)
    {
        gameObject.transform.position = new Vector3(x, y, z);
    }
    public static void SetLocalPosition(this GameObject gameObject, float x, float y, float z)
    {
        gameObject.transform.localPosition = new Vector3(x, y, z);
    }
    public static void SetLocalScale(this GameObject gameObject, float x, float y, float z)
    {
        gameObject.transform.localScale = new Vector3(x, y, z);
    }
    //public static void SetParent(this GameObject gameObject, GameObject parent)
    //{
    //    gameObject.transform.parent = parent.transform;
    //}




    /// <summary>
    /// 获得所有的子物体
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="includeInactive"></param>
    /// <returns></returns>
    public static List<GameObject> GetChilds(this GameObject obj, bool includeSelf = true, bool includeInactive = true)
    {
        var childs = obj.GetComponentsInChildren<Transform>(includeInactive);
        List<GameObject> childObjs = new List<GameObject>();
        for (int i = 0; i < childs.Length; i++)
        {
            if (includeSelf == false && childs[i].gameObject == obj)
            {
                continue;
            }
            childObjs.Add(childs[i].gameObject);
        }

        return childObjs;
    }

    public static void RemoveComponent<T>(this GameObject obj) where T : Component
    {
        GameObject.Destroy(obj.GetComponent<T>());
    }


    /// <summary> 
    /// 利用反射来判断对象是否包含某个属性 
    /// </summary>
    /// <param name="instance">object</param> 
    /// <param name="propertyName">需要判断的属性</param> 
    /// <returns>是否包含</returns> 
    //public static bool ContainProperty(this object instance, string propertyName)
    //{
    //    if (instance != null && !string.IsNullOrEmpty(propertyName))
    //    {
    //        FieldInfo field = instance.GetType().GetField(propertyName);
    //        return (field != null);
    //    }
    //    return false;
    //}


    /*public static void setLayer(this GameObject obj, string layerName)
    {
        if (obj == null)
            return;
        int layer = LayerMask.NameToLayer(layerName);
        Transform[] childs = obj.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in childs)
            child.gameObject.layer = layer;
    }*/

    public static void SetLayer(this GameObject obj, string layerName, bool isCycle = false)
    {
        if (isCycle)
        {
            foreach (var item in obj.GetComponentsInChildren<Transform>(true))
            {
                item.gameObject.layer = LayerMask.NameToLayer(layerName);
            }
        }
        else
        {
            obj.layer = LayerMask.NameToLayer(layerName);
        }
    }


}


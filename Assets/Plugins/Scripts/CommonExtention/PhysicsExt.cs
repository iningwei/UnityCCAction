using UnityEngine;

public class PhysicsExt
{
    //封装一层，主要供lua侧调用
    //因为Raycast的重载函数太多，导致lua侧弱类型会调用到其它函数
    //参考：https://github.com/Tencent/xLua/blob/master/Assets/XLua/Doc/faq.md
    public static bool RaycastHit(Ray ray, out RaycastHit hit, float maxDis, int layerMask)
    {
        return Physics.Raycast(ray, out hit, maxDis, layerMask);
    }


    /// <summary>
    /// 检测鼠标是否点击到目标物
    /// </summary>
    /// <param name="target"></param>
    /// <param name="camera"></param>
    /// <param name="length"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    public static bool IsMouseRaycastHitTarget(GameObject target, Camera camera, int length, int layerMask)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, length, layerMask))
        {
            if (hit.collider.gameObject == target)
            {
                return true;
            }
        }

        return false;
    }
}

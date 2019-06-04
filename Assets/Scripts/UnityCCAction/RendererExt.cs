using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class RendererExt
{
    /*
     sharedMaterial是共享材质，无论如何操作材质的属性，内存中只会占用一份。
     使用material的话，会new一份新的作用于它，直到Application.LoadLevel()或者Resources.UnloadUnusedAssets()才会释放，所以极易造成内存泄漏。

     但是如果在编辑器里面使用render.sharedMaterial的话，运行的时候如果对属性进行了修改的话，停止后本地的材质球也会停留在运行时的最终状态，因此会对项目管理、版本管理造成额外的工作。

     故这里根据运行平台来设置获得的材质球
         */
    public static Material GetMaterial(this Renderer render)
    {
#if UNITY_EDITOR
        return render.material;
#else
        return render.sharedMaterial;
#endif
    }

    public static Material[] GetMaterials(this Renderer render)
    {
#if UNITY_EDITOR
        return render.materials;
#else
        return render.sharedMaterials;
#endif
    }
}

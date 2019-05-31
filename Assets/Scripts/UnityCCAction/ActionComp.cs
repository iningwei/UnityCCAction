using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZGame.cc
{
    /*  
     * 每个 ActionComp 在执行了Run之后不允许加入新的Action，有需要的话，可以通过为target添加新的ActionComp来解决
     * 
     */
    public class ActionComp : MonoBehaviour
    {
        /// <summary>
        /// ActionComp上挂载的最外层的动作的tag
        /// </summary>
        public int actionTag;

        public bool isFinished = false;

        Action action = null;
        public void AddAction(Action action)
        {
            if (this.action != null)
            {
                Debug.LogError("该ActionComp已经运行，无法中途插入动作");
                return;
            }
            this.actionTag = action.GetTag();
            this.action = action;

            this.action.Run();
        }

        public Action GetAction()
        {
           
            return this.action;
        }


        void Update()
        {
            if (this.action != null)
            {
                if (this.action.Update())
                {
                    Debug.Log(this.gameObject.name + " 动作播完了 ");
                    this.action = null;
                    this.isFinished = true;
                }
            }
        }
    }
}

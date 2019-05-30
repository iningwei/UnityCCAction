using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZGame.cc
{
    /*  
     * 每个FiniteActionComp在执行了Run之后不允许加入新的Action，有需要的话，可以通过为target添加新的FiniteActionComp来解决
     * 
         */
    public class FiniteActionComp : MonoBehaviour
    {

        public FiniteTimeAction curRunningAction = null;
        public void AddAction(FiniteTimeAction action)
        {
            if (this.curRunningAction != null)
            {
                Debug.LogError("该ActionIntervalComp已经运行，无法中途插入动作");
                return;
            }


            this.curRunningAction = action;

            this.run();

        }
        
        void run()
        {                        
            curRunningAction.Run();
        }
    
        void Update()
        {
            if (this.curRunningAction != null)
            {
                if (this.curRunningAction.Update())
                {
                    Debug.Log(this.gameObject.name + " 动作播完了 ");
                    this.curRunningAction = null;
                }
            }
        }
    }
}

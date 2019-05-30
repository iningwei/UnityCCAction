using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class ActionManager : SingletonMonoBehaviour<ActionManager>
    {
        //Dictionary<GameObject,>
        //Dictionary<GameObject,List<Action>>
        public void AddAction(FiniteTimeAction action, GameObject target, bool paused)
        {
            action.SetTarget(target);
            var actionComp = target.AddComponent<FiniteActionComp>();
            actionComp.AddAction(action);
        }

        /// <summary>
        /// 移除所有对象的所有动作
        /// </summary>
        public void RemoveAllActions()
        {

        }

        public void RemoveAllActionsFromTarget(GameObject target, bool forceDelete)
        {

        }


        /// <summary>
        /// 移除指定动作
        /// </summary>
        /// <param name="action"></param>
        public void RemoveAction(Action action)
        {

        }

        /// <summary>
        /// 删除指定对象下具有特定标签的一个动作，将删除首个匹配到的动作
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="target"></param>
        public void RemoveActionByTag(uint tag, GameObject target)
        {

        }


        public Action GetActionByTag(uint tag, GameObject target)
        {
            return null;
        }

        public uint GetNumberOfRunnningActionsInTarget(GameObject target)
        {
            return 0;
        }

        /// <summary>
        /// 暂停指定对象上的动作，所有正在运行的动作和新添加的动作都会暂停
        /// </summary>
        /// <param name="target"></param>
        public void PauseTarget(GameObject target)
        {

        }

        /// <summary>
        /// 让指定目标对象上的动作恢复运行
        /// </summary>
        /// <param name="target"></param>
        public void ResumeTarget(GameObject target)
        {

        }



        private void Update()
        {

        }
    }
}
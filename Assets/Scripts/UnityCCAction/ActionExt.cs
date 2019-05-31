using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public static class ActionExt
    {
        public static void RunAction(this GameObject target, Action action)
        {
            ActionManager.Instance.AddAction(target, action);
        }


        /// <summary>
        /// 停止并移除动作
        /// </summary>
        /// <param name="target"></param>
        /// <param name="action"></param>
        public static void StopAction(this GameObject target, Action action)
        {
            ActionManager.Instance.RemoveAction(target, action);
        }

        /// <summary>
        /// 停止并移除target上指定tag的的动作
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        public static void StopAction(this GameObject target, int tag)
        {
            ActionManager.Instance.RemoveActionByTag(target, tag);
        }

        /// <summary>
        /// 停止并移除target上所有的动作
        /// </summary>
        /// <param name="target"></param>
        public static void StopAllActions(this GameObject target)
        {

        }
    }
}
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
        /// stop and remove action from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="action"></param>
        public static bool StopAction(this GameObject target, Action action)
        {
            return ActionManager.Instance.RemoveAction(target, action);
        }

        /// <summary>
        /// stop and remove action of specific tag from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        public static bool StopAction(this GameObject target, int tag)
        {
            return ActionManager.Instance.RemoveActionByTag(target, tag);
        }

        /// <summary>
        /// stop and remove all actions from target
        /// </summary>
        /// <param name="target"></param>
        public static bool StopAllActions(this GameObject target)
        {
            return ActionManager.Instance.RemoveAllActionsFromTarget(target);
        }

        public static bool PauseAction(this GameObject target, Action action)
        {
            return ActionManager.Instance.PauseAction(target, action);
        }
        public static bool PauseAction(this GameObject target, int tag)
        {
            return ActionManager.Instance.PauseActionByTag(target, tag);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class ActionManager : SingletonMonoBehaviour<ActionManager>
    {

        //Dictionary<GameObject,>
        Dictionary<GameObject, List<ActionComp>> dicOfActions = new Dictionary<GameObject, List<ActionComp>>();

        public void AddAction(GameObject target, Action action)
        {
            action.SetTarget(target);

            var actionComp = target.AddComponent<ActionComp>();
            actionComp.AddAction(action);

            this.addAction(target, actionComp);
        }

        bool addAction(GameObject target, ActionComp actionComp)
        {
            if (!dicOfActions.ContainsKey(target))
            {
                dicOfActions[target] = new List<ActionComp>();
            }
            var actionComps = dicOfActions[target];
            for (int i = 0; i < actionComps.Count; i++)
            {
                if (actionComps[i].GetAction() == actionComp.GetAction())
                {
                    Debug.LogError("dicOfActions have the same action, can not add");
                    return false;
                }
            }
            actionComps.Add(actionComp);
            return true;
        }

        /// <summary>
        /// remove all actions from all targets
        /// </summary>
        public bool RemoveAllActions()
        {
            return true;
        }

        public bool RemoveAllActionsFromTarget(GameObject target, bool forceDelete)
        {
            return true;
        }


        /// <summary>
        /// remove action of target
        /// </summary>
        /// <param name="action"></param>
        public bool RemoveAction(GameObject target, Action action)
        {
            if (target == null)
            {
                Debug.LogError("target is null");
                return false;
            }
            if (action == null)
            {
                Debug.LogError("action is null");
                return false;
            }
            if (!dicOfActions.ContainsKey(target))
            {
                Debug.LogError("error dicOfActions not contain target");
                return false;
            }
            var actionComps = dicOfActions[target];

            return true;

        }


        /// <summary>
        /// remove action by tag from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        public void RemoveActionByTag(GameObject target, int tag)
        {

        }


        public Action GetActionByTag(GameObject target, int tag)
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
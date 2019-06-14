using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZGame.cc
{
    public class ActionManager : SingletonMonoBehaviour<ActionManager>
    {
        Dictionary<GameObject, List<ActionComp>> dicOfActions = new Dictionary<GameObject, List<ActionComp>>();





        public void AddAction(GameObject target, Action action)
        {
            if (this.existSameAction(target, action))
            {
                Debug.LogError(target.name + "已经具有相同tag的action，不可重复添加, tag:" + action.GetTag());

                return;
            }


            action.SetTarget(target);
            action.ActionFinished += Action_ActionFinished;
            var actionComp = target.AddComponent<ActionComp>();
            actionComp.AddAction(action);

            this.addActionComp(target, actionComp);
        }

        private void Action_ActionFinished(object sender, ActionFinishedEventArgs e)
        {
            //Debug.Log("onActionFinished:" + sender.ToString() + ", tareget:" + e.Target.ToString());
            this.RemoveAction(e.Target, e.Action);//目前为每一个action都监听了其播放完毕的事件，并在播放完毕后移除该action，TODO：后续会提供常驻action的功能
        }

        bool existSameAction(GameObject target, Action action)
        {
            if (dicOfActions.ContainsKey(target))
            {
                var actionComps = dicOfActions[target];
                for (int i = 0; i < actionComps.Count; i++)
                {
                    if (actionComps[i].actionTag == action.GetTag())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        bool addActionComp(GameObject target, ActionComp actionComp)
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
        /// remove all actions of all targets
        /// </summary>
        public void RemoveAllActions()
        {
            List<GameObject> allKeys = this.dicOfActions.Keys.ToList();
            foreach (var item in allKeys)
            {
                this.RemoveAllActionsFromTarget(item);
            }

        }

        public bool RemoveAllActionsFromTarget(GameObject target)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return false;
            }
            if (!dicOfActions.ContainsKey(target))
            {
                Debug.LogError("error, dicOfActions not contains target");
                return false;
            }

            var actionComps = dicOfActions[target];
            ActionComp actionComp = null;
            for (int i = actionComps.Count - 1; i >= 0; i--)
            {
                actionComp = actionComps[i];
                GameObject.Destroy(actionComp);
            }
            dicOfActions.Remove(target);
            return true;
        }


        /// <summary>
        /// remove action from target
        /// </summary>
        /// <param name="action"></param>
        public bool RemoveAction(GameObject target, Action action)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return false;
            }

            if (action == null)
            {
                Debug.LogError("action is null");
                return false;
            }
            if (!dicOfActions.ContainsKey(target))
            {
                Debug.LogError("error, dicOfActions not contain target");
                return false;
            }
            var actionComps = dicOfActions[target];
            ActionComp actionComp = null;
            for (int i = actionComps.Count - 1; i >= 0; i--)
            {
                actionComp = actionComps[i];
                if (actionComp.GetAction() == action)
                {
                    actionComps.Remove(actionComp);
                    GameObject.Destroy(actionComp);
                    return true;
                }
            }

            Debug.LogError("target does not have the action");
            return false;

        }


        /// <summary>
        /// remove action by tag from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        public bool RemoveActionByTag(GameObject target, int tag)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return false;
            }

            if (!dicOfActions.ContainsKey(target))
            {
                Debug.LogWarning("error, dicOfActions not contain target");
                return false;
            }
            var actionComps = dicOfActions[target];
            ActionComp actionComp = null;
            for (int i = actionComps.Count - 1; i >= 0; i--)
            {
                actionComp = actionComps[i];
                if (actionComp.actionTag == tag)
                {
                    actionComps.Remove(actionComp);
                    GameObject.Destroy(actionComp);
                    return true;
                }
            }
            Debug.LogWarning("target does not have the action of tag:" + tag);
            return false;
        }




        public Action GetActionByTag(GameObject target, int tag)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return null;
            }

            if (!dicOfActions.ContainsKey(target))
            {
                Debug.LogWarning("error, dicOfActions not contain target");
                return null;
            }
            var actionComps = dicOfActions[target];
            ActionComp actionComp = null;
            for (int i = actionComps.Count - 1; i >= 0; i--)
            {
                actionComp = actionComps[i];
                if (actionComp.actionTag == tag)
                {
                    return actionComp.GetAction();
                }
            }

            Debug.LogWarning("target does not have the action of tag:" + tag);
            return null;
        }



        public uint GetNumberOfRunnningActionsInTarget(GameObject target)
        {
            //TODO:
            return 0;
        }

        /// <summary>
        /// Pause all actions of target.
        /// Even you add action after the pause command, the new action still in pause state.
        /// </summary>
        /// <param name="target"></param>
        public void PauseTarget(GameObject target)
        {

        }

        /// <summary>
        /// Resume all actions from pause state to run state.
        /// </summary>
        /// <param name="target"></param>
        public void ResumeTarget(GameObject target)
        {

        }

        public void PauseAction(GameObject target, Action action)
        {
            if (action != null)
            {
                action.Pause();
            }


        }
        public void PauseActionByTag(GameObject target, int tag)
        {
            Action action = GetActionByTag(target, tag);
            this.PauseAction(target, action);
        }

        public void ResumeAction(GameObject target, Action action)
        {
            if (action != null)
            {
                action.Resume();
            }
        }

        public void ResumeActionByTag(GameObject target, int tag)
        {
            Action action = GetActionByTag(target, tag);
            this.ResumeAction(target, action);
        }
    }
}
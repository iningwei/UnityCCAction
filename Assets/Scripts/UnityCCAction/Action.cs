using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class ActionFinishedEventArgs : EventArgs
    {
        public GameObject Target { get; set; }
        public Action Action { get; set; }

        public ActionFinishedEventArgs(GameObject target, Action action)
        {
            this.Target = target;
            this.Action = action;

        }
    }



    /// <summary>
    /// 所有动作类型的基类
    /// </summary>
    public abstract class Action
    {

        public abstract event EventHandler<ActionFinishedEventArgs> ActionFinished;



        protected GameObject target = null;
        protected int tag = 0;
        protected string actionName = string.Empty;
        /// <summary>
        /// 动作是否完成
        /// 对于次数为1次的动作，一次执行完毕，即完成。否则需要满足执行次数才完成。      
        /// </summary>
        protected bool isDone = false;

        protected bool isPause = false;//是否处于暂停状态
        /// <summary>
        /// 总暂停时间（单位秒）
        /// </summary>
        protected float totalPausedTime;
        /// <summary>
        /// 上次 开始暂停时 的时间点
        /// </summary>
        protected float lastPausedTime;

        public abstract void Run();
        /// <summary>
        /// 返回值指示是否完成动作
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        public abstract void Finish();
        //public abstract void OnActionFinished(ActionFinishedEventArgs args);
        /// <summary>
        /// 返回一个克隆的对象
        /// </summary>
        /// <returns></returns>
        public abstract Action Clone();
        /// <summary>
        /// 动作是否完成，true 是， false 否
        /// </summary>
        /// <returns></returns>
        public abstract bool IsDone();


        /// <summary>
        /// 是否在暂停状态
        /// </summary>
        /// <returns></returns>
        public abstract bool IsPause();

        /// <summary>
        /// 暂停动作
        /// </summary>
        public abstract void Pause();
        /// <summary>
        /// 把动作从暂停中唤醒
        /// </summary>
        public abstract void Resume();

        public abstract float GetTotalPausedTime();

        /// <summary>
        /// Get the tag of an action
        /// </summary>
        /// <returns></returns>
        public abstract int GetTag();

        public abstract string GetActionName();

        /// <summary>
        /// 获得执行当前动作的目标节点
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetTarget();

        /// <summary>
        /// 为动作设置执行的目标节点
        /// </summary>
        /// <param name="target"></param>
        public abstract void SetTarget(GameObject target);

        /// <summary>
        /// 获得原始目标节点
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetOriginalTarget();


    }
}

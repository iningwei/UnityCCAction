using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{


    public class TweenFinishedEventArgs : EventArgs
    {
        public GameObject Target { get; set; }
        public Tween Tween { get; set; }

        public TweenFinishedEventArgs(GameObject target, Tween tween)
        {
            this.Target = target;
            this.Tween = tween;

        }
    }



    /// <summary>
    /// 所有补间类型的基类
    /// </summary>
    public abstract class Tween
    {
        public abstract event EventHandler<TweenFinishedEventArgs> TweenFinished;

        protected GameObject target = null;
        protected int tag = 0;
        protected string tweenName = string.Empty;

        /// <summary>
        /// 该补间开始的时间点
        /// </summary>
        protected float startTime;
        /// <summary>
        /// 该补间真实运行时间
        /// </summary>
        protected float trueRunTime;

        /// <summary>
        /// 补间是否完成
        /// 对于次数为1次的补间，一次执行完毕，即完成。否则需要满足执行次数才完成。      
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
        /// 返回值指示是否完成补间
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        public abstract void Finish();


        /// <summary>
        /// 返回一个克隆的对象
        /// </summary>
        /// <returns></returns>
        public abstract Tween Clone();
        /// <summary>
        /// 补间是否完成，true 是， false 否
        /// </summary>
        /// <returns></returns>
        public abstract bool IsDone();


        /// <summary>
        /// 是否在暂停状态
        /// </summary>
        /// <returns></returns>
        public abstract bool IsPause();

        /// <summary>
        /// 暂停补间
        /// </summary>
        public abstract void Pause();
        /// <summary>
        /// 把补间从暂停中唤醒
        /// </summary>
        public abstract void Resume();

        public abstract float GetTotalPausedTime();

        /// <summary>
        /// Get the tag of an tween
        /// </summary>
        /// <returns></returns>
        public abstract int GetTag();

        public abstract string GetTweenName();

        /// <summary>
        /// 获得执行当前补间的目标节点
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetTarget();

        /// <summary>
        /// 为补间设置执行的目标节点
        /// </summary>
        /// <param name="target"></param>
        public abstract void SetTarget(GameObject target);

        /// <summary>
        /// 获得原始目标节点
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetOriginalTarget();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">参数为update从开始到当前的时间</param>
        /// <returns></returns>
        public abstract Tween OnUpdate(Action<float> callback);
        protected Action<float> updateCallback;
    }
}

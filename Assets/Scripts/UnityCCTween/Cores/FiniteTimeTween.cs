using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 有限时长的补间
    /// </summary>
    public abstract class FiniteTimeTween : Tween
    {
        /// <summary>
        /// 补间持续时间
        /// </summary>
        protected float duration = 0;
       



        /// <summary>
        /// 1表示补间只播放一遍；2、3、4...表示补间播放指定次数; 小于1表示补间循环播放；
        /// </summary>
        protected int repeatTimes = 1;
        /// <summary>
        /// 补间已播放的次数
        /// </summary>
        protected int repeatedTimes = 0;


        /// <summary>
        /// 获得补间持续时间，单位秒
        /// </summary>
        /// <returns></returns>
        public abstract float GetDuration();

        /// <summary>
        /// 设置补间持续时间，单位秒
        /// </summary>
        /// <param name="time"></param>
        public abstract void SetDuration(float time);


        /// <summary>
        /// 为补间设置标签，用于识别补间
        /// </summary>
        /// <param name="tag"></param>
        public abstract FiniteTimeTween SetTag(int tag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times">1表示补间只播放一遍；2、3、4...表示补间播放指定次数; 小于1表示补间循环播放；</param>
        /// <returns></returns>
        public abstract FiniteTimeTween SetRepeatTimes(int times);

        public abstract int GetRepeatTimes();


        public abstract FiniteTimeTween SetTweenName(string name);


        /// <summary>
        /// 由子类重写该类
        /// 某次补间完成后，判断是否完成所有补间次数的播放
        /// </summary>
        protected abstract void OnPartialTweenFinished();
        /// <summary>
        /// 返回一个新补间，新补间的执行与原补间完全相反
        /// </summary>
        public abstract void Reverse();


        public abstract FiniteTimeTween Delay(float time);

        /// <summary>
        /// 补间完成后的一些回调
        /// 若补间多次重复（即repeatTimes>1），那么必须所有补间重复完毕后才会触发回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public abstract FiniteTimeTween OnComplete(Action<object[]> callback, params object[] param);
        protected Action<object[]> completeCallback;
        protected object[] completeCallbackParams;


        
    }
}
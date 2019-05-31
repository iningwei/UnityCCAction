using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 有限时长的动作
    /// </summary>
    public abstract class FiniteTimeAction : Action
    {
        protected int tag = 0;
        /// <summary>
        /// 动作是否完成
        /// 对于次数为1次的动作，一次执行完毕，即完成。否则需要满足执行次数才完成。
        /// 对于无限次数的动作，isDone永远为false
        /// </summary>
        protected bool isDone = false;

        /// <summary>
        /// 动作持续时间
        /// </summary>
        protected float duration = 0;
        /// <summary>
        /// 该动作开始的时间点
        /// </summary>
        protected float startTime;

        /// <summary>
        /// 1表示动作只播放一遍；2、3、4...表示动作播放指定次数; 小于1表示动作循环播放；
        /// </summary>
        protected int repeatTimes = 1;
        /// <summary>
        /// 动作已播放的次数
        /// </summary>
        protected int repeatedTimes = 0;

        /// <summary>
        /// 获得动作的Tag
        /// </summary>
        /// <returns></returns>
        public abstract int GetTag();

        /// <summary>
        /// 为动作设置标签，用于识别动作
        /// </summary>
        /// <param name="tag"></param>
        public abstract FiniteTimeAction SetTag(int tag);
        /// <summary>
        /// 获得动作持续时间，单位秒
        /// </summary>
        /// <returns></returns>
        public abstract float GetDuration();

        /// <summary>
        /// 设置动作持续时间，单位秒
        /// </summary>
        /// <param name="time"></param>
        public abstract void SetDuration(float time);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="times">1表示动作只播放一遍；2、3、4...表示动作播放指定次数; 小于1表示动作循环播放；</param>
        /// <returns></returns>
        public abstract FiniteTimeAction SetRepeatTimes(int times);

        public abstract int GetRepeatTimes();

        /// <summary>
        /// 子类需要重写该类
        /// 某次动作完成后，判断是否完成所有动作次数的播放
        /// </summary>
        public abstract void OnPartialFinished();
        /// <summary>
        /// 返回一个新动作，新动作的执行与元动作完全相反
        /// </summary>
        public abstract void Reverse();


        public abstract FiniteTimeAction Delay(float time);

        public abstract FiniteTimeAction OnComplete(Action<object[]> callback, object[] param);
        protected Action<object[]> completeCallback;
        protected object[] completeCallbackParams;

    }
}
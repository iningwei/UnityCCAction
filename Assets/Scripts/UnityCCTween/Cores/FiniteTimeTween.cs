using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// Tween will finished in certain times
    /// </summary>
    public abstract class FiniteTimeTween : Tween
    {
        /// <summary>
        /// how long will the tween last
        /// </summary>
        protected float duration = 0;

        /// <summary>
        /// 1 means tween only play once；2、3、4...means tween will play specific times;less than 1 means tween will play circle；
        /// </summary>
        protected int repeatTimes = 1;
        /// <summary>
        /// tween has played times
        /// </summary>
        protected int repeatedTimes = 0;

        protected RepeatType repeatType = RepeatType.Clamp;
        public RepeatType GetRepeatType()
        {
            return this.repeatType;
        }
        public abstract FiniteTimeTween SetRepeatType(RepeatType repeatType);

        /// <summary>
        /// 1 means positive dir，-1 means negative dir
        /// </summary>
        protected int tweenDiretion = 1;

        /// <summary>
        /// get tween lasted times,unit by seconds
        /// </summary>
        /// <returns></returns>
        public float GetDuration() {
            return this.duration;
        }

        /// <summary>
        /// set tween's time, unit by seconds
        /// </summary>
        /// <param name="time"></param>
        public abstract void SetDuration(float time);


        /// <summary>
        /// set tag for tween
        /// </summary>
        /// <param name="tag"></param>
        public abstract FiniteTimeTween SetTag(int tag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times">1 means tween only play once；2、3、4...means tween will play specific times;less than 1 means tween will play circle；</param>
        /// <returns></returns>
        public abstract FiniteTimeTween SetRepeatTimes(int times);

        public abstract int GetRepeatTimes();


        public abstract FiniteTimeTween SetTweenName(string name);


        
        protected abstract void OnPartialTweenFinished();
        /// <summary>
        /// reverse the tween
        /// </summary>
        public abstract void Reverse();


        public abstract FiniteTimeTween Delay(float time);

        /// <summary>
        /// call after tween finished.If the tween repeat more than one time,then it only call after all the repeats finished.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public abstract FiniteTimeTween OnComplete(Action<object[]> callback, params object[] param);
        protected Action<object[]> completeCallback;
        protected object[] completeCallbackParams;



    }
}
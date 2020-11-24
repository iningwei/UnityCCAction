using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class TweenFinishedEventArgs : EventArgs
    {
        public GameObject Holder { get; set; }
        public Tween Tween { get; set; }

        public TweenFinishedEventArgs(GameObject holer, Tween tween)
        {
            this.Holder = holer;
            this.Tween = tween;
        }
    }




    public abstract class Tween
    {
        public abstract event EventHandler<TweenFinishedEventArgs> TweenFinished;

        protected GameObject holder = null;
        protected int id = 0;
        protected string tweenName = string.Empty;


        /// <summary>
        /// The tween stated time,unit by seconds
        /// </summary>
        protected float startTime;
        /// <summary>
        /// The real run time of this tween
        /// </summary>
        protected float truePartialRunTime;

        /// <summary>
        /// If the tween has finished.
        /// If the tween repeats more than once,finish means the tween finished the repeats
        /// </summary>
        protected bool isDone = false;

        /// <summary>
        /// Whether in pause status
        /// </summary>
        protected bool isPause = false;
        /// <summary>
        /// Total paused time,unit by seconds
        /// </summary>
        protected float totalPausedTime;
        /// <summary>
        /// Last pause started time,unit by seconds
        /// </summary>
        protected float lastPausedTime;



        protected bool ignoreTimeScale = false;

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
        public Tween SetRepeatType(RepeatType repeatType)
        {
            this.repeatType = repeatType;
            return this;
        }


        public abstract void Run();
        /// <summary>
        /// Tween execute effect in Update function, the returned boolean indicate whether the tween has finished
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        public abstract void Finish();

        protected Func<float, float> easeFunc = EaseTool.Get(Ease.Linear);

        public virtual Tween Easing(Ease ease)
        {
            this.easeFunc = EaseTool.Get(ease);
            return this;
        }


        public bool IsDone()
        {
            return this.isDone;
        }

        public bool IsPause()
        {
            return this.isPause;
        }

        public Tween IgnoreTimeScale(bool ignore)
        {
            this.ignoreTimeScale = ignore;
            return this;
        }


        public abstract void Pause();
        /// <summary>
        /// Wake up tween from pause
        /// </summary>
        public abstract void Resume();

        /// <summary>
        /// Get total paused time,unit by seconds
        /// </summary>
        /// <returns></returns>
        public float GetTotalPausedTime()
        {
            return this.totalPausedTime;
        }


        public int GetId()
        {
            return this.id;
        }

        public void SetId(int id)
        {
            //Debug.LogError("set id:" + id);
            this.id = id;
        }



        public virtual void SetHolder(GameObject holder)
        {
            this.holder = holder;
        }



        public GameObject GetHolder()
        {
            return this.holder;
        }

        public float GetTime()
        {
            if (ignoreTimeScale)
            {
                return Time.realtimeSinceStartup;
            }
            return Time.time;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="times">1 means tween only play once；2、3、4...means tween will play specific times;less than 1 means tween will play circle；</param>
        /// <returns></returns>
        public Tween SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
            return this;
        }



        public int GetRepeatTimes()
        {
            return this.repeatTimes;
        }

        /// <summary>
        /// how long will the tween last
        /// </summary>
        protected float duration = 0;
        /// <summary>
        /// get tween lasted times,unit by seconds
        /// </summary>
        /// <returns></returns>
        public float GetDuration()
        {
            return this.duration;
        }

        /// <summary>
        /// set tween's time, unit by seconds
        /// </summary>
        /// <param name="time"></param>
        public Tween SetDuration(float time)
        {
            this.duration = time;
            return this;
        }

        public Tween SetTweenName(string name)
        {
            this.tweenName = name;
            return this;
        }

        public string GetTweenName()
        {
            return this.tweenName;
        }

        public object fromPara = null;
        public virtual Tween From(object para)
        {
            fromPara = para;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">The float param is the time from update start to now</param>
        /// <returns></returns>
        public abstract Tween OnUpdate(Action<float> callback);
        protected Action<float> updateCallback;



        /// <summary>
        /// call after tween finished.If the tween repeat more than one time,then it only call after all the repeats finished.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public abstract Tween OnComplete(Action<object[]> callback, params object[] param);
        protected Action<object[]> completeCallback;
        protected object[] completeCallbackParams;
    }
}

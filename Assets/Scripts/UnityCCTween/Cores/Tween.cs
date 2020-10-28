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




    public abstract class Tween
    {
        public abstract event EventHandler<TweenFinishedEventArgs> TweenFinished;

        protected GameObject holder = null;
        protected int tag = 0;
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

        public abstract void Run();
        /// <summary>
        /// Tween execute effect in Update function, the returned boolean indicate whether the tween has finished
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        public abstract void Finish();




        public bool IsDone()
        {
            return this.isDone;
        }

        public bool IsPause()
        {
            return this.isPause;
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


        public int GetTag()
        {
            return this.tag;
        }

        public string GetTweenName()
        {
            return this.tweenName;
        }


        public GameObject GetHolder()
        {
            return this.holder;
        }


        public abstract void SetHolder(GameObject holder);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">The float param is the time from update start to now</param>
        /// <returns></returns>
        public abstract Tween OnUpdate(Action<float> callback);
        protected Action<float> updateCallback;
    }
}

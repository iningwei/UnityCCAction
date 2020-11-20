using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class DelayTime : TweenInterval
    {

        public DelayTime(float time)
        {
            this.SetDuration(time);
            this.SetTweenName("DelayTime");

        }

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

       

        /// <summary>
        /// Please do not Set delay for DelayTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public override Tween Delay(float time)
        {
            Debug.LogError("Set delay for DelayTime will not take effect");
            return this;
        }

       

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
            if (this.completeCallback != null)
            {
                this.completeCallback(this.completeCallbackParams);
            }
            this.TweenFinished?.Invoke(this, new TweenFinishedEventArgs(this.GetHolder(), this));
        }

 

        public override Tween OnComplete(Action<object[]> callback, object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
            return this;
        }

        protected override void OnPartialTweenFinished()
        {
            this.repeatedTimes++;
            if (this.repeatedTimes == this.repeatTimes)
            {
                this.Finish();
            }
            else
            {
                this.Run();
            }
        }
 

        public override void Run()
        {
            this.isDone = false;
            this.startTime = this.GetTime() - this.GetTotalPausedTime();
            this.truePartialRunTime = 0;
        }

       
        public override bool Update()
        {
            if (this.IsDone())
            {
                return true;
            }
            if (this.IsPause())
            {
                return false;
            }
            this.truePartialRunTime = this.GetTime() - startTime - this.GetTotalPausedTime();
            if (this.truePartialRunTime > this.duration)
            {
                this.OnPartialTweenFinished();
            }

            this.doUpdateCallback();
            return this.IsDone();
        }

        private void doUpdateCallback()
        {
            if (this.IsDone() || this.updateCallback == null)
            {
                return;
            }

            this.updateCallback(this.truePartialRunTime);
        }



 

      

        public override void Pause()
        {
            if (this.isPause)
            {
                return;
            }

            this.isPause = true;
            this.lastPausedTime = this.GetTime();
        }

        public override void Resume()
        {
            if (this.isPause == false)
            {
                return;
            }
            this.isPause = false;
            this.totalPausedTime += (this.GetTime() - this.lastPausedTime);
        }

        

        public override Tween OnUpdate(Action<float> callback)
        {
            this.updateCallback = callback;
            return this;
        }
 
 
    }
}
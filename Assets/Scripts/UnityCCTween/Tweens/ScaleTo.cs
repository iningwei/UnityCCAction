using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// ScaleTo is driven from TweenInterval
    /// </summary>
    public class ScaleTo : TweenInterval
    {
        public Vector3 startScale = Vector3.zero;
        public Vector3 targetScale;


        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="targetPos">pos is based on target obj's parent node</param>
        public ScaleTo(float duration, Vector3 targetPos)
        {
            if (duration <= 0)
            {
                Debug.LogError("error, duration should >0");
                return;
            }
            this.SetDuration(duration);
            this.targetScale = targetPos;

            this.SetTweenName("ScaleTo");
        }

        public override Tween From(object para)
        {
            if (!(para is Vector3))
            {
                Debug.LogError("wrong type,para should be Vector3");
                return this;
            }
            Vector3 fromScale = (Vector3)para;
            this.holder.transform.localScale = fromScale;
            return this;
        }

        public override Tween Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
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

            if (this.repeatedTimes == 0)
            {
                this.startScale = this.holder.transform.localScale;
            }
            else
            {
                if (this.GetRepeatType() == RepeatType.Clamp)
                {
                    this.tweenDiretion = 1;
                }
                else
                {
                    this.tweenDiretion = -this.tweenDiretion;
                }
            }
            this.startTime = this.GetTime() - this.GetTotalPausedTime();
            this.truePartialRunTime = 0f;
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

            this.doScaleTo();
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



        private void doScaleTo()
        {
            if (this.IsDone())
            {
                return;
            }

            var dir = this.tweenDiretion == 1 ? (this.targetScale - this.startScale) : (this.startScale - this.targetScale);
            float t = this.truePartialRunTime / this.duration;
            t = t > 1 ? 1 : t;
            var desScale = (this.tweenDiretion == 1 ? this.startScale : this.targetScale) + dir * (this.easeFunc(t));

            this.holder.transform.localScale = desScale;

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class DelayTime : ActionInterval
    {

        public DelayTime(float time)
        {
            this.SetDuration(time);
            this.SetActionName("DelayTime");

        }

        public override event EventHandler<ActionFinishedEventArgs> ActionFinished;

        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Please do not Set delay for DelayTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public override FiniteTimeAction Delay(float time)
        {
            Debug.LogError("Set delay for DelayTime will not take effect");
            return this;
        }

        public override ActionInterval Easing(Ease ease)
        {
            throw new System.NotImplementedException();
        }

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
            if (this.completeCallback != null)
            {
                this.completeCallback(this.completeCallbackParams);
            }
            this.ActionFinished?.Invoke(this, new ActionFinishedEventArgs(this.GetTarget(), this));
        }

        public override float GetDuration()
        {
            return this.duration;
        }

        public override GameObject GetOriginalTarget()
        {
            throw new System.NotImplementedException();
        }

        public override int GetRepeatTimes()
        {
            return this.repeatTimes;
        }

        public override int GetTag()
        {
            return this.tag;
        }

        public override GameObject GetTarget()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsDone()
        {
            return this.isDone;
        }

        public override FiniteTimeAction OnComplete(Action<object[]> callback, object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
            return this;
        }

        protected override void OnPartialActionFinished()
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

        public override void Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            this.isDone = false;
            this.startTime = Time.time- this.GetTotalPausedTime();
        }

        public override void SetDuration(float time)
        {
            this.duration = time;
        }

        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
            return this;
        }

        public override FiniteTimeAction SetTag(int tag)
        {
            this.tag = tag;
            return this;

        }

        public override void SetTarget(GameObject target)
        {
            this.target = target;
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

            if (Time.time - startTime - this.GetTotalPausedTime() > this.duration)
            {
                this.OnPartialActionFinished();
            }
            return this.IsDone();
        }

        public override FiniteTimeAction SetActionName(string name)
        {
            this.actionName = name;
            return this;
        }

        public override string GetActionName()
        {
            return this.actionName;
        }



        public override bool IsPause()
        {
            return this.isPause;
        }

        public override void Pause()
        {
            if (this.isPause)
            {
                return;
            }

            this.isPause = true;
            this.lastPausedTime = Time.time;
        }

        public override void Resume()
        {
            if (this.isPause == false)
            {
                return;
            }
            this.isPause = false;
            this.totalPausedTime += (Time.time - this.lastPausedTime);
        }

        public override float GetTotalPausedTime()
        {
            return this.totalPausedTime;
        }
    }
}
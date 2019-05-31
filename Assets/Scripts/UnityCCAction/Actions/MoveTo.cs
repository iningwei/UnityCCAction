using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{

    public class MoveTo : ActionInterval
    {
        Vector3 startPos = Vector3.zero;
        Vector3 targetPos;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="targetPos">坐标系统基于父节点的坐标系</param>
        public MoveTo(float duration, Vector3 targetPos)
        {
            if (this.duration <= 0)
            {
                Debug.LogError("error, duration should >0");
                return;
            }
            this.duration = duration;
            this.targetPos = targetPos;
        }
        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeAction Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

        public override ActionInterval Easing(Ease ease)
        {
            this.easeFunc = EaseTool.Get(ease);
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
        }

        public override float GetDuration()
        {
            throw new System.NotImplementedException();
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
            return this.target;
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

        protected override void OnPartialFinished()
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

            if (this.startPos == Vector3.zero)
            {
                this.startPos = this.target.transform.localPosition;
            }
            this.startTime = Time.time;
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


            if (Time.time - startTime > this.duration)
            {
                this.OnPartialFinished();
            }

            var dir = this.targetPos - this.startPos;
            float t = (Time.time - startTime) / this.duration;
            t = t > 1 ? 1 : t;

            var desPos = this.startPos + dir * (this.easeFunc(t));
            this.target.transform.localPosition = desPos;

            return this.IsDone();
        }
    }
}

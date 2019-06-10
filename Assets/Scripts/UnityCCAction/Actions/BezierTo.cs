using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class BezierTo : ActionInterval
    {
        Vector3 startPos = Vector3.zero;
        Vector3 targetPos;
        Vector3[] controlPoints;

        public override event EventHandler<ActionFinishedEventArgs> ActionFinished;

        /// <summary>
        /// 当前只支持1个和2个控制点的贝塞尔曲线
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="controlPoints"></param>
        /// <param name="targetPos">移动的目标位置，基于</param>
        public BezierTo(float duration, Vector3[] controlPoints, Vector3 targetPos)
        {
            if (duration <= 0)
            {
                Debug.LogError("error, duration should >0");
                return;
            }
            if (controlPoints == null || controlPoints.Length == 0 || controlPoints.Length > 2)
            {
                Debug.LogError("error, currently controlPoints only support 1 or 2 points");
                return;
            }
            if (this.targetPos == null)
            {
                Debug.LogError("error, targetPos is null");
                return;
            }

            this.SetDuration(duration);
            this.controlPoints = controlPoints;
            this.targetPos = targetPos;
            this.SetActionName("BezierTo");

        }
        public override Action Clone()
        {
            throw new NotImplementedException();
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
            this.ActionFinished?.Invoke(this, new ActionFinishedEventArgs(this.GetTarget(), this));
        }

        public override float GetDuration()
        {
            throw new NotImplementedException();
        }

        public override GameObject GetOriginalTarget()
        {
            throw new NotImplementedException();
        }

        public override int GetRepeatTimes()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                this.OnPartialActionFinished();
            }

            this.target.transform.localPosition = this.getBezierPos(Time.time);


            return this.IsDone();
        }

        Vector3 getBezierPos(float time)
        {
            Vector3 pos = Vector3.zero;
            float t = (time - this.startTime) / this.duration;
            t = t > 1 ? 1 : t;
            var t1 = t;

            t = this.easeFunc(t);
            var t2 = t;
            //Debug.Log("t1:" + t1 + ", t2:" + t2);

            if (this.controlPoints.Length == 1)//二阶贝塞尔曲线
            {
                pos = (1 - t) * (1 - t) * this.startPos + 2 * t * (1 - t) * this.controlPoints[0] + t * t * this.targetPos;
            }
            else if (this.controlPoints.Length == 2)//三阶贝塞尔曲线
            {
                pos = (1 - t) * (1 - t) * (1 - t) * this.startPos + 3 * t * (1 - t) * (1 - t) * this.controlPoints[0] + 3 * t * t * (1 - t) * this.controlPoints[1] + t * t * t * this.targetPos;
            }
            else
            {
                Debug.LogError("some thing wrong");
            }

            return pos;
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
    }
}
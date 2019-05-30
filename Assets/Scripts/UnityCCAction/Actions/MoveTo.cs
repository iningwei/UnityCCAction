using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 坐标系统为：基于父节点
    /// </summary>
    public class MoveTo : ActionInterval
    {
        public Vector3 startPos = Vector3.zero;
        public Vector3 targetPos;

        public MoveTo(float duration, Vector3 targetPos)
        {
            this.time = duration;

            this.targetPos = targetPos;

        }
        public override Action Clone()
        {
            throw new System.NotImplementedException();
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

       

        public override void OnPartialFinished()
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
            this.time = time;
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
            if (Time.time - startTime > this.time)
            {
                this.OnPartialFinished();
            }

            var dir = this.targetPos - this.startPos;
            float t = (Time.time - startTime) / this.time;
            t = t > 1 ? 1 : t;

            var desPos = this.startPos + dir * (this.easeFunc(t));
            this.target.transform.localPosition = desPos;

            return this.IsDone();
        }
    }
}

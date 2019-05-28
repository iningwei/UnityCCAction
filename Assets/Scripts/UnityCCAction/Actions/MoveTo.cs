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
        }

        public override float GetDuration()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject GetOriginalTarget()
        {
            throw new System.NotImplementedException();
        }

        public override int GetTag()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject GetTarget()
        {
            return this.target;
        }

        public override bool IsDone()
        {
            return this.isDone;
        }

        public override ActionInterval Repeat(uint times)
        {
            throw new System.NotImplementedException();
        }

        public override ActionInterval RepeatForever()
        {
            throw new System.NotImplementedException();
        }

        public override void Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            if (this.startPos == Vector3.zero)
            {
                this.startPos = this.target.transform.localPosition;
            }

            this.startTime = Time.time;

            this.isDone = false;
        }

        public override void SetDuration(float time)
        {
            this.time = time;
        }

        public override void SetTag(int tag)
        {
            throw new System.NotImplementedException();
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
                this.Finish();
            }

            var dir = this.targetPos - this.startPos;
            float t = (Time.time - startTime) / this.time;

            var desPos = this.startPos + dir * (this.easeFunc(t));
            this.target.transform.localPosition = desPos;

            return this.IsDone();
        }
    }
}

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

        }
        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override ActionInterval Easing(Ease ease)
        {
            throw new System.NotImplementedException();
        }

        public override void Finish()
        {
            this.isDone = true;
        }

        public override float GetDuration()
        {
            return this.time;
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public override bool Update()
        {
            if (this.IsDone())
            {
                return true;
            }

            if (Time.time - startTime > this.time && this.IsDone() == false)
            {
                this.Finish();
            }
            return this.IsDone();
        }
    }
}
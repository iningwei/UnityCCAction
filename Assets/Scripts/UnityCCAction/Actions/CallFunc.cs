using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class CallFunc : ActionInstant
    {
        System.Action func;


        public CallFunc(System.Action func)
        {
            if (func == null)
            {
                Debug.LogError("func can not be null");
            }
            this.func = func;
        }

        public void Call<T>(System.Action<T> func, T param)
        {

        }



        bool isPartialFinished = false;

        public override void Run()
        {
            this.isDone = false;
            this.isPartialFinished = false;
            this.func();
            this.isPartialFinished = true;

        }

        public override Action Clone()
        {
            throw new System.NotImplementedException();
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

        public override void Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override void SetDuration(float time)
        {
            throw new System.NotImplementedException();
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
            if (this.isPartialFinished)
            {
                this.OnPartialFinished();
            }
            return this.IsDone();
        }

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
            if (this.completeCallback != null)
            {
                this.completeCallback(this.completeCallbackParam);
            }
        }


        public override int GetRepeatTimes()
        {
            return this.repeatTimes;
        }

        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
            return this;
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

        public override FiniteTimeAction OnComplete(Action<object> callback, object param)
        {
            this.completeCallback = callback;
            this.completeCallbackParam = param;
            return this;
        }
    }
}

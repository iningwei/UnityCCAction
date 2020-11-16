using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class CallFunc : TweenInstant
    {
        Action<object[]> func;
        object[] paras;


        public CallFunc(System.Action<object[]> func, params object[] paras)
        {
            if (func == null)
            {
                Debug.LogError("func can not be null");
            }
            this.func = func;
            this.paras = paras;
            this.SetTweenName("CallFunc");
        }

        public void Call<T>(System.Action<T> func, T param)
        {

        }



        bool isPartialFinished = false;

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        public override void Run()
        {
            this.isDone = false;
            this.isPartialFinished = false;
            this.func(this.paras);
            this.isPartialFinished = true;
            this.truePartialRunTime = 0f;
        }

 
        

        public override bool Update()
        {
            if (this.isPartialFinished)
            {
                this.OnPartialTweenFinished();
            }
            this.truePartialRunTime = this.GetTime() - startTime - this.GetTotalPausedTime();
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

        public override Tween OnComplete(Action<object[]> callback, object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
            return this;
        }

        public override Tween Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

  

    

        public override void Pause()
        {
            Debug.LogWarning("CallFunc is TweenInstant, can not Pause");
        }

        public override void Resume()
        {
            Debug.LogWarning("CallFunc is TweenInstant, can not Resume");
        }

       

        public override Tween OnUpdate(Action<float> callback)
        {
            this.updateCallback = callback;
            return this;
        }

 
    }
}

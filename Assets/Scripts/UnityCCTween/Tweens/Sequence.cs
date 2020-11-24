using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// It's a holder tween,like Repeat
    /// It has some child-tweens.These tweens will be called one by one.
    /// Also you can use Repeat to reallize Sequence's function.Just set repeat time to 1.
    /// </summary>
    public class Sequence : TweenInterval
    {
        public Tween[] tweenSequences;
        Queue<Tween> legalTweens = new Queue<Tween>();
        Tween curRunningTween = null;

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        /// <summary>
        /// child-tweens will be called one by one.
        ///  SetRepeatTimes for sequence will not work.
        /// </summary>
        public Sequence(params Tween[] tweens)
        {
            if (tweens == null || tweens.Length == 0)
            {
                Debug.LogError("tweens must contain at least one");
                return;
            }
            tweenSequences = tweens;
            this.legalTweens.Clear();
            this.curRunningTween = null;
            foreach (var item in tweens)
            {
                this.legalTweens.Enqueue(item);
            }
            this.SetTweenName("Sequence");
        }



        public override Tween Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }


        /// <summary>
        /// do not set easing for sequence
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        public override Tween Easing(Ease ease)
        {
            Debug.LogError("Sequence set easing will not work");
            return this;
        }

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
            this.curRunningTween = null;
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
            this.curRunningTween = null;
            this.startTime = this.GetTime() - this.GetTotalPausedTime();
            this.truePartialRunTime = 0f;
            if (this.legalTweens.Count > 0)
            {
                this.curRunningTween = this.legalTweens.Dequeue();
                this.curRunningTween.Run();
            }
            else
            {
                this.OnPartialTweenFinished();
            }
        }



        public override void SetHolder(GameObject target)
        {
            this.holder = target;

            //set target for child-tweens
            foreach (var item in this.tweenSequences)
            {
                item.SetHolder(this.holder);
            }

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

            if (this.curRunningTween != null)
            {
                if (this.curRunningTween.Update())
                {
                    this.Run();
                }
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






        public override void Pause()
        {
            if (this.isPause)
            {
                return;
            }

            this.isPause = true;
            this.lastPausedTime = this.GetTime();

            if (this.curRunningTween != null)
            {
                this.curRunningTween.Pause();
            }
        }

        public override void Resume()
        {
            if (this.isPause == false)
            {
                return;
            }
            this.isPause = false;
            this.totalPausedTime += (this.GetTime() - this.lastPausedTime);


            if (this.curRunningTween != null)
            {
                this.curRunningTween.Resume();
            }
        }


        public override Tween OnUpdate(Action<float> callback)
        {
            this.updateCallback = callback;
            return this;
        }


    }
}
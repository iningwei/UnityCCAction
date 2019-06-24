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
        public FiniteTimeTween[] tweenSequences;
        Queue<FiniteTimeTween> legalTweens = new Queue<FiniteTimeTween>();
        FiniteTimeTween curRunningTween = null;

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        /// <summary>
        /// child-tweens will be called one by one.
        ///  SetRepeatTimes for sequence will not work.
        /// </summary>
        public Sequence(params FiniteTimeTween[] tweens)
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

        public override Tween Clone()
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeTween Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }


        /// <summary>
        /// do not set easing for sequence
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        public override TweenInterval Easing(Ease ease)
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
            this.TweenFinished?.Invoke(this, new TweenFinishedEventArgs(this.GetTarget(), this));
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

        public override FiniteTimeTween OnComplete(Action<object[]> callback, object[] param)
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

        public override void Reverse()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            this.isDone = false;
            this.curRunningTween = null;
            this.startTime = Time.time - this.GetTotalPausedTime();
            this.trueRunTime = 0f;
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

        public override void SetDuration(float time)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Do not SetRepeatTimes for Sequence.It is designed for one sequence.
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public override FiniteTimeTween SetRepeatTimes(int times)
        {
            Debug.LogError("SetRepeatTimes for Sequence will not take effect");
            return this;
        }

        public override FiniteTimeTween SetTag(int tag)
        {
            this.tag = tag;
            return this;

        }

        public override void SetTarget(GameObject target)
        {
            this.target = target;

            //set target for child-tweens            
            foreach (var item in this.tweenSequences)
            {
                item.SetTarget(this.target);
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
            this.trueRunTime = Time.time - startTime - this.GetTotalPausedTime();
            this.doUpdateCallback();
            return this.IsDone();
        }
        private void doUpdateCallback()
        {
            if (this.IsDone() || this.updateCallback == null)
            {
                return;
            }

            this.updateCallback(this.trueRunTime);
        }



        public override FiniteTimeTween SetTweenName(string name)
        {
            this.tweenName = name;
            return this;
        }

        public override string GetTweenName()
        {
            return this.tweenName;
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
            this.totalPausedTime += (Time.time - this.lastPausedTime);


            if (this.curRunningTween != null)
            {
                this.curRunningTween.Resume();
            }
        }

        public override float GetTotalPausedTime()
        {
            return this.totalPausedTime;
        }
        public override Tween OnUpdate(Action<float> callback)
        {
            this.updateCallback = callback;
            return this;
        }

        public override RepeatType GetRepeatType()
        {
            return this.repeatType;
        }

        public override FiniteTimeTween SetRepeatType(RepeatType repeatType)
        {
            Debug.LogError("Do not set repeatType for Sequence,it will not work");
            return this;
        }
    }
}
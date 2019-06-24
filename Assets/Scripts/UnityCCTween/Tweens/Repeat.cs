using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// Repeat is a holder tween,like Sequence.
    /// All child-tweens will be called one by one.
    /// </summary>
    public class Repeat : TweenInterval
    {
        FiniteTimeTween[] tweens;
        Queue<FiniteTimeTween> legalTweens = new Queue<FiniteTimeTween>();
        Queue<FiniteTimeTween> cycleTweens = new Queue<FiniteTimeTween>();
        FiniteTimeTween curRunningTween = null;

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times">1 will worked like Sequence；2、3、4...will make all childs be called one by one for specific times; less than 1 means all tweens will continue played until you stop it；</param>
        /// <param name="tweens"></param>
        public Repeat(int times, params FiniteTimeTween[] tweens)
        {
            if (tweens == null || tweens.Length == 0)
            {
                Debug.LogError("error, repeat tweens at leaset have one");
                return;
            }
            this.tweens = tweens;
            this.legalTweens.Clear();
            this.cycleTweens.Clear();
            this.curRunningTween = null;
            this.repeatTimes = times;
            foreach (var item in tweens)
            {
                this.legalTweens.Enqueue(item);
            }

            this.SetTweenName("Repeat");
        }
        public override Tween Clone()
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeTween Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

        public override TweenInterval Easing(Ease ease)
        {
            Debug.LogError("set easing for repeat tween will not work");
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
                //Debug.LogWarning(this.GetTag() + " repeat tween 完成：" + this.repeatedTimes + ", 总次数：" + this.repeatTimes);

                //完成后也要交换一下，防止出现Repeat套Repeat的情况，导致的运行结果出错
                var tmp = this.legalTweens;
                this.legalTweens = this.cycleTweens;
                this.cycleTweens = tmp;

                this.Finish();
            }
            else
            {
                if (this.repeatTimes < 1 || (this.repeatTimes > 1 && repeatedTimes < this.repeatTimes))
                {
                    var tmp = this.legalTweens;
                    this.legalTweens = this.cycleTweens;
                    this.cycleTweens = tmp;


                    //Debug.LogWarning(this.GetTag() + " repeat tween 完成：" + this.repeatedTimes + ", 总次数：" + this.repeatTimes);
                    this.Run();
                }
                else
                {
                    Debug.LogError("something wrong!!");
                }
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

            if (legalTweens.Count > 0)
            {
                this.curRunningTween = legalTweens.Dequeue();
                if (this.repeatTimes != 1)
                {
                    this.cycleTweens.Enqueue(this.curRunningTween);
                }

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

        public override FiniteTimeTween SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
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
            foreach (var item in this.tweens)
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
    }
}

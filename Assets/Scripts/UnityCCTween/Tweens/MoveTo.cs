using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{

    public class MoveTo : TweenInterval
    {
        Vector3 startPos = Vector3.zero;
        Vector3 targetPos;
        Space relativeSpace;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="targetPos"></param>
        public MoveTo(float duration, Vector3 targetPos, Space relativeSpace)
        {
            if (duration < 0)
            {
                Debug.LogError("error,MoveTo duration should >=0");
                return;
            }
            this.SetDuration(duration);
            this.targetPos = targetPos;
            this.relativeSpace = relativeSpace;
            this.SetTweenName("MoveTo");
        }


        public override FiniteTimeTween Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

        public override TweenInterval Easing(Ease ease)
        {
            this.easeFunc = EaseTool.Get(ease);
            return this;
        }


        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

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

        public override int GetRepeatTimes()
        {
            return this.repeatTimes;
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

            if (this.repeatedTimes == 0)
            {
                this.startPos = relativeSpace == Space.Self ? this.holder.transform.localPosition : this.holder.transform.position;
            }
            else
            {
                if (this.GetRepeatType() == RepeatType.Clamp)
                {
                    this.tweenDiretion = 1;
                }
                else
                {
                    this.tweenDiretion = -this.tweenDiretion;
                }
            }
            this.startTime = Time.time - this.GetTotalPausedTime();//当RepeatTimes>1的时候，会再次进入Run()函数，若这之前暂停了游戏，那么这里取得的startTime就需要减去已经暂停的总时间
            this.truePartialRunTime = 0f;//补间运行时间设置为0

        }

        public override void SetDuration(float time)
        {
            this.duration = time;
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

        public override void SetHolder(GameObject target)
        {
            this.holder = target;
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

            this.truePartialRunTime = Time.time - startTime - this.GetTotalPausedTime();

            if (this.truePartialRunTime > this.duration)
            {
                this.OnPartialTweenFinished();
            }
            this.doMove();
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



        private void doMove()
        {
            if (this.IsDone())
            {
                return;
            }

            var dir = this.tweenDiretion == 1 ? (this.targetPos - this.startPos) : (this.startPos - this.targetPos);
            float t = this.truePartialRunTime / this.duration;
            t = t > 1 ? 1 : t;
            var desPos = (this.tweenDiretion == 1 ? this.startPos : this.targetPos) + dir * (this.easeFunc(t));

            if (this.relativeSpace == Space.Self)
            {
                this.holder.transform.localPosition = desPos;
            }
            else
            {
                this.holder.transform.position = desPos;
            }

        }




        public override FiniteTimeTween SetTweenName(string name)
        {
            this.tweenName = name;
            return this;
        }



        public override void Pause()
        {
            if (this.isPause)
            {
                return;
            }

            this.isPause = true;
            this.lastPausedTime = Time.time;
        }

        public override void Resume()
        {
            if (this.isPause == false)
            {
                return;
            }
            this.isPause = false;
            this.totalPausedTime += (Time.time - this.lastPausedTime);
        }



        public override Tween OnUpdate(Action<float> callback)
        {
            this.updateCallback = callback;
            return this;
        }



        public override FiniteTimeTween SetRepeatType(RepeatType repeatType)
        {
            this.repeatType = repeatType;
            return this;
        }
    }
}

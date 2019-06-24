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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="targetPos">坐标系统基于父节点的坐标系</param>
        public MoveTo(float duration, Vector3 targetPos)
        {
            if (duration < 0)
            {
                Debug.LogError("error,MoveTo duration should >=0");
                return;
            }
            this.SetDuration(duration);
            this.targetPos = targetPos;
            this.SetTweenName("MoveTo");
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


            this.TweenFinished?.Invoke(this, new TweenFinishedEventArgs(this.GetTarget(), this));
        }


        public override float GetDuration()
        {
            return this.duration;
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

            if (this.repeatedTimes == 0)
            {
                this.startPos = this.target.transform.localPosition;
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
            this.trueRunTime = 0f;//补间运行时间设置为0

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

            if (this.IsPause())
            {
                return false;
            }

            this.trueRunTime = Time.time - startTime - this.GetTotalPausedTime();

            if (this.trueRunTime > this.duration)
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
            this.updateCallback(this.trueRunTime);
        }



        private void doMove()
        {
            if (this.IsDone())
            {
                return;
            }

            var dir = this.tweenDiretion == 1 ? (this.targetPos - this.startPos) : (this.startPos - this.targetPos);
            float t = this.trueRunTime / this.duration;
            t = t > 1 ? 1 : t;
            var desPos = (this.tweenDiretion == 1 ? this.startPos : this.targetPos) + dir * (this.easeFunc(t));
            this.target.transform.localPosition = desPos;
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
            this.repeatType = repeatType;
            return this;
        }
    }
}

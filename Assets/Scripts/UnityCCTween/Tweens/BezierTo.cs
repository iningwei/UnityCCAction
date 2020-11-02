using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class BezierTo : TweenInterval
    {
        Vector3 startPos = Vector3.zero;
        Vector3 targetPos;
        Vector3[] controlPoints;
        Space relativeSpace;
        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        /// <summary>
        /// 当前只支持1个和2个控制点的贝塞尔曲线
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="controlPoints"></param>
        /// <param name="targetPos">移动的目标位置，基于</param>
        public BezierTo(float duration, Vector3[] controlPoints, Vector3 targetPos, Space relativeSpace)
        {
            if (duration <= 0)
            {
                Debug.LogError("error, duration should >0");
                return;
            }
            if (controlPoints == null || controlPoints.Length == 0 || controlPoints.Length > 2)
            {
                Debug.LogError("error, currently controlPoints only support 1 or 2 points");
                return;
            }
            if (this.targetPos == null)
            {
                Debug.LogError("error, targetPos is null");
                return;
            }

            this.SetDuration(duration);
            this.controlPoints = controlPoints;
            this.targetPos = targetPos;
            this.relativeSpace = relativeSpace;
            this.SetTweenName("BezierTo");

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            this.startTime = Time.time - this.GetTotalPausedTime();
            this.truePartialRunTime = 0f;
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

            this.doBezierTo();
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


        void doBezierTo()
        {
            if (this.IsDone())
            {
                return;
            }


            Vector3 pos = Vector3.zero;
            float t = this.truePartialRunTime / this.duration;
            t = t > 1 ? 1 : t;
            t = this.easeFunc(t);
            if (this.controlPoints.Length == 1)//二阶贝塞尔曲线
            {
                pos = (1 - t) * (1 - t) * (this.tweenDiretion == 1 ? this.startPos : this.targetPos) + 2 * t * (1 - t) * this.controlPoints[0] + t * t * (this.tweenDiretion == 1 ? this.targetPos : this.startPos);
            }
            else if (this.controlPoints.Length == 2)//三阶贝塞尔曲线
            {
                pos = (1 - t) * (1 - t) * (1 - t) * (this.tweenDiretion == 1 ? this.startPos : this.targetPos) + 3 * t * (1 - t) * (1 - t) * this.controlPoints[0] + 3 * t * t * (1 - t) * this.controlPoints[1] + t * t * t * (this.tweenDiretion == 1 ? this.targetPos : this.startPos);
            }
            else
            {
                Debug.LogError("some thing wrong");
            }

            if (this.relativeSpace == Space.Self)
            {
                this.holder.transform.localPosition = pos;
            }
            else
            {
                this.holder.transform.position = pos;
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
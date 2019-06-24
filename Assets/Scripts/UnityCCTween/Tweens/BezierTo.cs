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

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        /// <summary>
        /// 当前只支持1个和2个控制点的贝塞尔曲线
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="controlPoints"></param>
        /// <param name="targetPos">移动的目标位置，基于</param>
        public BezierTo(float duration, Vector3[] controlPoints, Vector3 targetPos)
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
            this.SetTweenName("BezierTo");

        }
        public override Tween Clone()
        {
            throw new NotImplementedException();
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
            this.TweenFinished?.Invoke(this, new TweenFinishedEventArgs(this.GetTarget(), this));
        }

        public override float GetDuration()
        {
            throw new NotImplementedException();
        }

        public override GameObject GetOriginalTarget()
        {
            throw new NotImplementedException();
        }

        public override int GetRepeatTimes()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override void Run()
        {
            this.isDone = false;
            if (this.startPos == Vector3.zero)
            {
                this.startPos = this.target.transform.localPosition;
            }
            this.startTime = Time.time - this.GetTotalPausedTime();
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

            this.updateCallback(this.trueRunTime);
        }


        void doBezierTo()
        {
            if (this.IsDone())
            {
                return;
            }


            Vector3 pos = Vector3.zero;
            float t = this.trueRunTime / this.duration;
            t = t > 1 ? 1 : t;
            var t1 = t;

            t = this.easeFunc(t);
            var t2 = t;
            //Debug.Log("t1:" + t1 + ", t2:" + t2);

            if (this.controlPoints.Length == 1)//二阶贝塞尔曲线
            {
                pos = (1 - t) * (1 - t) * this.startPos + 2 * t * (1 - t) * this.controlPoints[0] + t * t * this.targetPos;
            }
            else if (this.controlPoints.Length == 2)//三阶贝塞尔曲线
            {
                pos = (1 - t) * (1 - t) * (1 - t) * this.startPos + 3 * t * (1 - t) * (1 - t) * this.controlPoints[0] + 3 * t * t * (1 - t) * this.controlPoints[1] + t * t * t * this.targetPos;
            }
            else
            {
                Debug.LogError("some thing wrong");
            }

            this.target.transform.localPosition = pos;
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
    }
}
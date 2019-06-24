using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZGame.cc
{
    /// <summary>
    /// Rotate is driven from an InfiniteTimeTween
    /// </summary>
    public class Rotate : InfiniteTimeTween
    {
        float xValue;
        float yValue;
        float zValue;
        Space relativeSpace;

        /// <summary>
        /// Applies a rotation of zAngle degrees around the z axis, xAngle degrees around
        /// the x axis, and yAngle degrees around the y axis (in that order) each seconds.        
        /// </summary>
        /// <param name="xValue"></param>
        /// <param name="yValue"></param>
        /// <param name="zValue"></param>
        /// <param name="relativeSpace"></param>
        public Rotate(float xValue, float yValue, float zValue, Space relativeSpace)
        {
            this.xValue = xValue;
            this.yValue = yValue;
            this.zValue = zValue;

            this.relativeSpace = relativeSpace;
            this.SetTweenName("Rotate");
        }

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

        public override Tween Clone()
        {
            throw new System.NotImplementedException();
        }

        public override void Finish()
        {
            this.isDone = true;
            this.TweenFinished?.Invoke(this, new TweenFinishedEventArgs(this.GetTarget(), this));
        }

        public override string GetTweenName()
        {
            return this.tweenName;
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

        public override void Run()
        {

        }

        public override InfiniteTimeTween SetTweenName(string name)
        {
            this.tweenName = name;
            return this;
        }

        public override InfiniteTimeTween SetTag(int tag)
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
            this.doRotate();
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

        private void doRotate()
        {
            if (this.IsDone())
            {
                return;
            }
            this.target.transform.Rotate(xValue * Time.deltaTime, yValue * Time.deltaTime, zValue * Time.deltaTime, this.relativeSpace);

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
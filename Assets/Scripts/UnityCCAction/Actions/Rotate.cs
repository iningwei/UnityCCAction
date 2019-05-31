using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZGame.cc
{
    public class Rotate : InfiniteTimeAction
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

        }

        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override void Finish()
        {
            this.isDone = true;
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
            throw new System.NotImplementedException();
        }

        public override InfiniteTimeAction SetTag(int tag)
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
            this.target.transform.Rotate(xValue * Time.deltaTime, yValue * Time.deltaTime, zValue * Time.deltaTime, this.relativeSpace);
            return this.IsDone();
        }
    }
}
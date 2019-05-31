using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 顺序播放动作一次
    /// 也可以使用Repeat设置播放次数为1，来实现Sequence
    /// </summary>
    public class Sequence : ActionInterval
    {
        public FiniteTimeAction[] actionSequences;
        Queue<FiniteTimeAction> legalActions = new Queue<FiniteTimeAction>();
        FiniteTimeAction curRunningAction = null;
        /// <summary>
        /// 顺序播放动作一次
        /// SetRepeatTimes() 设置播放次数会无效
        /// </summary>
        public Sequence(params FiniteTimeAction[] actions)
        {
            if (actions == null || actions.Length == 0)
            {
                Debug.LogError("actions must contain at least one");
                return;
            }
            actionSequences = actions;
            this.legalActions.Clear();
            this.curRunningAction = null;
            foreach (var item in actions)
            {
                this.legalActions.Enqueue(item);
            }

        }

        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeAction Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }


        /// <summary>
        /// do not set easing for Easing
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        public override ActionInterval Easing(Ease ease)
        {
            Debug.LogError("Sequence set easing will not work");
            return this;
        }

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
            this.curRunningAction = null;
            if (this.completeCallback != null)
            {
                this.completeCallback(this.completeCallbackParams);
            }
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

        public override FiniteTimeAction OnComplete(Action<object[]> callback, object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
            return this;
        }

        public override void OnPartialFinished()
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
            this.curRunningAction = null;
            this.startTime = Time.time;
            if (this.legalActions.Count > 0)
            {
                this.curRunningAction = this.legalActions.Dequeue();
                this.curRunningAction.Run();
            }
            else
            {
                this.OnPartialFinished();
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
        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            Debug.LogError("Sequence setRepeatTimes will not take effect");
            return this;
        }

        public override FiniteTimeAction SetTag(int tag)
        {
            this.tag = tag;
            return this;

        }

        public override void SetTarget(GameObject target)
        {
            this.target = target;

            //为Repeat内的子动作设置target
            foreach (var item in this.actionSequences)
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

            if (this.curRunningAction != null)
            {
                if (this.curRunningAction.Update())
                {
                    this.Run();
                }
            }

            return this.IsDone();
        }
    }
}
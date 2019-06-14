﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// It's a holder action,like Repeat
    /// It has some child-actions.These actions will be called one by one.
    /// Also you can use Repeat to reallize Sequence's function.Just set repeat time to 1.
    /// </summary>
    public class Sequence : ActionInterval
    {
        public FiniteTimeAction[] actionSequences;
        Queue<FiniteTimeAction> legalActions = new Queue<FiniteTimeAction>();
        FiniteTimeAction curRunningAction = null;

        public override event EventHandler<ActionFinishedEventArgs> ActionFinished;

        /// <summary>
        /// child-actions will be called one by one.
        ///  SetRepeatTimes for sequence will not work.
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
            this.SetActionName("Sequence");
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
        /// do not set easing for sequence
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
            this.ActionFinished?.Invoke(this, new ActionFinishedEventArgs(this.GetTarget(), this));
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

        protected override void OnPartialActionFinished()
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
            this.startTime = Time.time - this.GetTotalPausedTime();
            if (this.legalActions.Count > 0)
            {
                this.curRunningAction = this.legalActions.Dequeue();
                this.curRunningAction.Run();
            }
            else
            {
                this.OnPartialActionFinished();
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
            Debug.LogError("SetRepeatTimes for Sequence will not take effect");
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

            //set target for child-actions            
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
            if (this.IsPause())
            {
                return false;
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

        public override FiniteTimeAction SetActionName(string name)
        {
            this.actionName = name;
            return this;
        }

        public override string GetActionName()
        {
            return this.actionName;
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

            if (this.curRunningAction != null)
            {
                this.curRunningAction.Pause();
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


            if (this.curRunningAction != null)
            {
                this.curRunningAction.Resume();
            }
        }

        public override float GetTotalPausedTime()
        {
            return this.totalPausedTime;
        }
    }
}
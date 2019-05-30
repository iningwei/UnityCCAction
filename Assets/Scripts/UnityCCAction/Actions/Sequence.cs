using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 顺序播放动作一次
    /// </summary>

    public class Sequence : ActionInterval
    {
        public FiniteTimeAction[] actionSequences;
        public Sequence(params FiniteTimeAction[] actions)
        {
            if (actions == null || actions.Length == 0)
            {
                Debug.LogError("actions must contain at least one");
                return;
            }
            actionSequences = actions;
        }

        public override Action Clone()
        {
            throw new System.NotImplementedException();
        }

        public override ActionInterval Easing(Ease ease)
        {
            throw new System.NotImplementedException();
        }

        public override void Finish()
        {
            this.isDone = true;
            this.repeatedTimes = 0;
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
            throw new System.NotImplementedException();
        }

        public override GameObject GetTarget()
        {
            return this.target;
        }

        public override bool IsDone()
        {
            return this.isDone;
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

        }

        public override void SetDuration(float time)
        {
            throw new System.NotImplementedException();
        }

        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
            return this;
        }

        public override void SetTag(int tag)
        {
            throw new System.NotImplementedException();
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
            return this.IsDone();
        }
    }
}
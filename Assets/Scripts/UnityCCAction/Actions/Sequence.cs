using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{

    public class Sequence : ActionInterval
    {
        public Action[] actionSequences;
        public Sequence(Action action1, params Action[] actions)
        {
            if (action1 == null)
            {
                Debug.LogError("action1 must not null");
                return;
            }
            int count = 1 + (actions == null ? 0 : actions.Length);
            actionSequences = new Action[count];
            actionSequences[0] = action1;
            for (int i = 1; i < count; i++)
            {
                actionSequences[i] = actions[i - 1];
            }

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
        }

        public override float GetDuration()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject GetOriginalTarget()
        {
            throw new System.NotImplementedException();
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

        public override ActionInterval Repeat(uint times)
        {
            throw new System.NotImplementedException();
        }

        public override ActionInterval RepeatForever()
        {
            throw new System.NotImplementedException();
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

        public override void SetTag(int tag)
        {
            throw new System.NotImplementedException();
        }

        public override void SetTarget(GameObject target)
        {
            this.target = target;

            //同时还要为Sequence内子物体设置target
            for (int i = 0; i < this.actionSequences.Length; i++)
            {
                this.actionSequences[i].SetTarget(this.target);
            }

        }

        public override bool Update()
        {
            return this.IsDone();
        }
    }
}
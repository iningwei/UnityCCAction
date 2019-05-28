using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class CallFunc : ActionInstant
    {
        System.Action func;


        public CallFunc(System.Action func)
        {
            this.func = func;
        }

        public void Call<T>(System.Action<T> func, T param)
        {

        }



        public override void Run()
        {
            this.func();
            this.Finish();
        }

        public override Action Clone()
        {
            throw new System.NotImplementedException();
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

        public override void Reverse()
        {
            throw new System.NotImplementedException();
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
        }

        public override bool Update()
        {
            return this.IsDone();
        }

        public override void Finish()
        {
            this.isDone = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 重复播放动作
    /// </summary>
    public class Repeat : ActionInterval
    {
        FiniteTimeAction[] actions;
        Queue<FiniteTimeAction> legalActions = new Queue<FiniteTimeAction>();
        Queue<FiniteTimeAction> cycleActions = new Queue<FiniteTimeAction>();
        FiniteTimeAction curRunningAction = null;

        /// <summary>
        /// 重复播放动作
        /// </summary>
        /// <param name="times">1表示动作只播放一遍；2、3、4...表示动作播放指定次数; 小于1表示动作循环播放；</param>
        /// <param name="actions"></param>
        public Repeat(int times, params FiniteTimeAction[] actions)
        {
            if (times == 0 || times < -1)
            {
                Debug.LogError("error, times not valid, times:" + times);
                return;
            }
            if (actions == null || actions.Length == 0)
            {
                Debug.LogError("error, repeat actions at leaset have one");
                return;
            }
            this.actions = actions;
            this.legalActions.Clear();
            this.cycleActions.Clear();
            this.curRunningAction = null;
            this.repeatTimes = times;
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

        public override ActionInterval Easing(Ease ease)
        {
            Debug.LogError("Repeat set easing will not work");
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
                //Debug.LogWarning(this.GetTag() + " repeat action完成：" + this.repeatedTimes + ", 总次数：" + this.repeatTimes);

                //完成后也要交换一下，防止出现Repeat套Repeat的情况，导致的运行结果出错
                var tmp = this.legalActions;
                this.legalActions = this.cycleActions;
                this.cycleActions = tmp;

                this.Finish();
            }
            else
            {
                if (this.repeatTimes < 1 || (this.repeatTimes > 1 && repeatedTimes < this.repeatTimes))
                {
                    var tmp = this.legalActions;
                    this.legalActions = this.cycleActions;
                    this.cycleActions = tmp;


                    //Debug.LogWarning(this.GetTag() + " repeat action完成：" + this.repeatedTimes + ", 总次数：" + this.repeatTimes);
                    this.Run();
                }
                else
                {
                    Debug.LogError("something wrong!!");
                }
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
            if (legalActions.Count > 0)
            {
                this.curRunningAction = legalActions.Dequeue();
                if (this.repeatTimes != 1)
                {
                    this.cycleActions.Enqueue(this.curRunningAction);
                }

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

        public override FiniteTimeAction SetRepeatTimes(int times)
        {
            this.repeatTimes = times;
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
            foreach (var item in this.actions)
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

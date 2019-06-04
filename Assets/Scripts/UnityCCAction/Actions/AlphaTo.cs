using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class AlphaTo : ActionInterval
    {
        float targetAlpha;

        List<Material> allMaterials = new List<Material>();
        List<float> startAlphas = new List<float>();

        bool includeChilds = false;
        bool includeInactive = false;
        public AlphaTo(float duration, float targetAlpha, bool includeChilds = false, bool includeInactive = false)
        {
            if (duration < 0)
            {
                Debug.LogError("error,AlphaTo duration should >=0");
                return;
            }
            this.SetDuration(duration);
            this.targetAlpha = targetAlpha;
            this.includeChilds = includeChilds;
            this.includeInactive = includeInactive;




            this.SetActionName("AlphaTo");
        }


        public override Action Clone()
        {
            throw new NotImplementedException();
        }

        public override FiniteTimeAction Delay(float time)
        {
            return new Sequence(new DelayTime(time), this);
        }

        public override ActionInterval Easing(Ease ease)
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
        }

        public override string GetActionName()
        {
            return this.actionName;
        }

        public override float GetDuration()
        {
            return this.duration;
        }

        public override GameObject GetOriginalTarget()
        {
            throw new NotImplementedException();
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

        public override FiniteTimeAction OnComplete(Action<object[]> callback, params object[] param)
        {
            this.completeCallback = callback;
            this.completeCallbackParams = param;
            return this;
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
                Renderer[] allRenderers;
                if (includeChilds)
                {
                    allRenderers = this.GetTarget().GetComponentsInChildren<Renderer>(includeInactive);
                }
                else
                {
                    allRenderers = this.GetTarget().GetComponents<Renderer>();
                }

                for (int i = 0; i < allRenderers.Length; i++)
                {
                    var mats = allRenderers[i].GetMaterials();
                    for (int j = 0; j < mats.Length; j++)
                    {
                        if (!allMaterials.Contains(mats[i]))
                        {
                            if (mats[i].shader.name.Contains("Standard"))
                            {
                                Debug.LogWarning("AlphaTo not support  standard shader:" + this.GetTarget());
                                continue;
                            }
                            allMaterials.Add(mats[i]);
                            startAlphas.Add(mats[i].color.a);   //目前只支持Main的Color
                        }
                    }
                }

            }

            Debug.Log(this.GetTarget() + "  AlphaTo 相关mat个数：" + allMaterials.Count);
            this.startTime = Time.time;
        }

        public override FiniteTimeAction SetActionName(string name)
        {
            this.actionName = name;
            return this;
        }

        public override void SetDuration(float time)
        {
            this.duration = time;

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
        }

        public override bool Update()
        {
            if (this.IsDone())
            {
                return true;
            }
            if (Time.time - startTime > this.duration)
            {
                this.OnPartialFinished();
            }

            this.doAlphaTo(Time.time);


            return this.IsDone();
        }

        private void doAlphaTo(float time)
        {
            float t = (time - this.startTime) / this.duration;
            t = t > 1 ? 1 : t;
            Material targetMat;
            for (int i = 0; i < this.allMaterials.Count; i++)
            {
                targetMat = this.allMaterials[i];
                targetMat.color = new Color(targetMat.color.r, targetMat.color.g, targetMat.color.b, this.startAlphas[i] + t * (this.targetAlpha - this.startAlphas[i]));
            }

        }

        protected override void OnPartialFinished()
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
    }
}
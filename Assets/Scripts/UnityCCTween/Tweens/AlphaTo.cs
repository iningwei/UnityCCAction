using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public class AlphaTo : TweenInterval
    {
        float targetAlpha;

        List<Material> allMaterials = new List<Material>();
        List<float> startAlphas = new List<float>();

        bool includeChilds = false;
        bool includeInactive = false;

        public override event EventHandler<TweenFinishedEventArgs> TweenFinished;

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




            this.SetTweenName("AlphaTo");
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

        public override string GetTweenName()
        {
            return this.tweenName;
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

        public override FiniteTimeTween OnComplete(Action<object[]> callback, params object[] param)
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
                        if (!allMaterials.Contains(mats[j]))
                        {
                            //TODO：必须是具有透明通道的shader才支持Alpha改变
                            if (mats[j].shader.name.Contains("Standard"))
                            {
                                Debug.LogWarning("AlphaTo not support  standard shader:" + this.GetTarget());
                                continue;
                            }
                            allMaterials.Add(mats[j]);
                            startAlphas.Add(mats[j].color.a);   //目前只支持Main的Color
                        }
                    }
                }

            }
            else
            {
                if (this.GetRepeatType() == RepeatType.Clamp)
                {
                    this.tweenDiretion = 1;
                }
                else
                {
                    this.tweenDiretion = -this.tweenDiretion;
                }
            }

            //Debug.Log(this.GetTarget() + "  AlphaTo 相关mat个数：" + allMaterials.Count);
            this.startTime = Time.time - this.GetTotalPausedTime();
            this.trueRunTime = 0f;
        }

        public override FiniteTimeTween SetTweenName(string name)
        {
            this.tweenName = name;
            return this;
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

            this.doAlphaTo();
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



        private void doAlphaTo()
        {
            if (this.IsDone())
            {
                return;
            }

            float t = this.trueRunTime / this.duration;
            t = t > 1 ? 1 : t;
            Material targetMat;
            for (int i = 0; i < this.allMaterials.Count; i++)
            {
                targetMat = this.allMaterials[i];
                targetMat.color = new Color(targetMat.color.r, targetMat.color.g, targetMat.color.b, this.startAlphas[i] + t * (this.targetAlpha - this.startAlphas[i]));
            }

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

        public override RepeatType GetRepeatType()
        {
            return this.repeatType;
        }

        public override FiniteTimeTween SetRepeatType(RepeatType repeatType)
        {
            this.repeatType = repeatType;
            return this;
        }
    }
}
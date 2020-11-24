using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace ZGame.TimerTween
{
    class Timer
    {

        private Action onComplete;
        private Action<float> onUpdate;

        private float? timeElapsedBeforePause;
        private float? timeElapsedBeforeCancel;
        private MonoBehaviour autoDestroyOwner;
        private bool hasAutoDestroyOwner;
        private float startTime;
        private float lastUpdateTime;
        private int loopedCount;//Already played count
        private Func<float, float> easeFunc = EaseTool.Get(Ease.Linear);



        private bool isOwnerDestroyed
        {
            get { return this.hasAutoDestroyOwner && this.autoDestroyOwner == null; }
        }


        public float Duration { get; private set; }

        public int Loop { get; private set; }

        public bool IsCompleted { get; private set; }

        public bool UseRealTime { get; private set; }
        //unique id,you can get timer or cancel timer by it
        public int ID { get; private set; }
        //string tag, it is used for you to rectify timer
        public string Tag { get; private set; }

        public bool IsPaused
        {
            get { return this.timeElapsedBeforePause.HasValue; }
        }

        public bool IsCancelled
        {
            get { return this.timeElapsedBeforeCancel.HasValue; }
        }

        public bool IsDone
        {
            get { return this.IsCompleted || this.IsCancelled || this.isOwnerDestroyed; }
        }




        private float getWorldTime()
        {
            return this.UseRealTime ? Time.realtimeSinceStartup : Time.time;
        }
        private float getFireTime()
        {
            return this.startTime + this.Duration;
        }
        private float getTimeDelta()
        {
            return this.getWorldTime() - this.lastUpdateTime;
        }

        public float getTimeElapsed()
        {
            if (this.IsCompleted || this.getWorldTime() >= this.getFireTime())
            {
                return this.Duration;
            }
            return this.timeElapsedBeforeCancel ??
                this.timeElapsedBeforePause ??
                this.getWorldTime() - this.startTime;
        }


        public Timer(float duration, Action onComplete = null, Action<float> onUpdate = null, int loop = 1, bool useRealTime = false, MonoBehaviour autoDestroyOwner = null)
        {
            this.Duration = duration;
            this.onComplete = onComplete;
            this.onUpdate = onUpdate;
            this.Loop = loop;
            this.UseRealTime = useRealTime;
            this.autoDestroyOwner = autoDestroyOwner;
            this.hasAutoDestroyOwner = autoDestroyOwner != null;
            //////this.startTime = this.getWorldTime();
            //////this.lastUpdateTime = this.startTime;
        }



        bool startFlag = false;
        public void Start()
        {
            this.startTime = this.getWorldTime();
            this.lastUpdateTime = this.startTime;
            startFlag = true;
        }


        public Timer SetOnComplete(Action onComplete)
        {
            if (this.onComplete != null)
            {
                Debug.LogWarning("Timer already has onComplete function,you will override it");
            }
            this.onComplete = onComplete;
            return this;
        }
        public Timer SetOnUpdate(Action<float> onUpdate)
        {
            if (this.onUpdate != null)
            {
                Debug.LogWarning("Timer already has onUpdate function,you will override it");
            }
            this.onUpdate = onUpdate;
            return this;
        }


        /// <summary>

        /// </summary>
        /// <param name="loop">
        /// 0 means loop forever
        /// >0 means loop play specific times
        /// <0 is invalid input
        /// </param>
        /// <returns></returns>
        public Timer SetLoop(int loop)
        {
            if (loop < 0)
            {
                Debug.LogError("loop can not less than 0, we force set it to 1");
                loop = 1;
            }
            this.Loop = loop;
            return this;
        }

        public Timer SetUseRealTime(bool useRealTime)
        {
            this.UseRealTime = useRealTime;
            return this;
        }
        public Timer SetOwner(MonoBehaviour owner)
        {
            this.autoDestroyOwner = owner;
            return this;
        }
        public Timer SetEase(Ease ease)
        {
            easeFunc = EaseTool.Get(ease);
            return this;
        }
        public Timer SetTag(string tag)
        {
            this.Tag = tag;
            return this;
        }
        public void SetId(int id)
        {
            this.ID = id;
        }


        public void Update()
        {
            if (this.startFlag == false)
            {
                return;
            }
            if (this.IsDone)
            {
                return;
            }
            if (this.IsPaused)
            {
                this.startTime += this.getTimeDelta();
                this.lastUpdateTime = this.getWorldTime();
                return;
            }

            this.lastUpdateTime = this.getWorldTime();
            if (this.onUpdate != null)
            {
                this.onUpdate(this.easeFunc(this.getTimeElapsed() / this.Duration));
            }
            if (this.getWorldTime() >= this.getFireTime())
            {
                if (this.onComplete != null)
                {
                    this.onComplete();
                }
                loopedCount++;
                if (Loop != 1)
                {
                    if (Loop == 0)
                    {
                        this.startTime = this.getWorldTime();
                    }
                    else
                    {
                        if (loopedCount < Loop)
                        {
                            this.startTime = this.getWorldTime();
                        }
                        else
                        {
                            this.IsCompleted = true;
                        }
                    }

                }
                else
                {
                    this.IsCompleted = true;
                }
            }
        }

        public void Cancel()
        {
            if (this.IsDone)
            {
                return;
            }
            this.timeElapsedBeforeCancel = this.getTimeElapsed();
            this.timeElapsedBeforePause = null;
        }
        public void Pause()
        {
            if (this.IsPaused || this.IsDone)
            {
                return;
            }
            this.timeElapsedBeforePause = this.getTimeElapsed();
        }

        public void Resume()
        {
            if (!this.IsPaused || this.IsDone)
            {
                return;
            }
            this.timeElapsedBeforePause = null;
        }

        public float GetTimeRemaining()
        {
            return this.Duration - this.getTimeElapsed();
        }

        public float GetRatioComplete()
        {
            return this.getTimeElapsed() / this.Duration;
        }
        public float GetRatioRemaining()
        {
            return this.GetTimeRemaining() / this.Duration;
        }
    }
}

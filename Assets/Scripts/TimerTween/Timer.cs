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
        private Action _onComplete;
        private Action<float> _onUpdate;

        private float? _timeElapsedBeforePause;
        private float? _timeElapsedBeforeCancel;
        private MonoBehaviour _autoDestroyOwner;
        private bool _hasAutoDestroyOwner;
        private float _startTime;
        private float _lastUpdateTime;
        private int loopedCount;//Already played count
        private Func<float, float> easeFunc = EaseTool.Get(Ease.Linear);



        private bool isOwnerDestroyed
        {
            get { return this._hasAutoDestroyOwner && this._autoDestroyOwner == null; }
        }


        public float duration { get; private set; }

        public int loop { get; private set; }

        public bool isCompleted { get; private set; }

        public bool useRealTime { get; private set; }
        //unique id,you can get timer or cancel timer by it
        public int id { get; private set; }
        //string tag, it is used for you to rectify timer
        public string tag { get; private set; }

        public bool isPaused
        {
            get { return this._timeElapsedBeforePause.HasValue; }
        }

        public bool isCancelled
        {
            get { return this._timeElapsedBeforeCancel.HasValue; }
        }

        public bool isDone
        {
            get { return this.isCompleted || this.isCancelled || this.isOwnerDestroyed; }
        }




        private float GetWorldTime()
        {
            return this.useRealTime ? Time.realtimeSinceStartup : Time.time;
        }
        private float GetFireTime()
        {
            return this._startTime + this.duration;
        }
        private float GetTimeDelta()
        {
            return this.GetWorldTime() - this._lastUpdateTime;
        }

        public float GetTimeElapsed()
        {
            if (this.isCompleted || this.GetWorldTime() >= this.GetFireTime())
            {
                return this.duration;
            }
            return this._timeElapsedBeforeCancel ??
                this._timeElapsedBeforePause ??
                this.GetWorldTime() - this._startTime;
        }


        public Timer(float duration, Action onComplete = null, Action<float> onUpdate = null, int loop = 1, bool useRealTime = false, MonoBehaviour autoDestroyOwner = null)
        {
            this.duration = duration;
            this._onComplete = onComplete;
            this._onUpdate = onUpdate;
            this.loop = loop;
            this.useRealTime = useRealTime;
            this._autoDestroyOwner = autoDestroyOwner;
            this._hasAutoDestroyOwner = autoDestroyOwner != null;
            this._startTime = this.GetWorldTime();
            this._lastUpdateTime = this._startTime;




        }
        public Timer SetOnComplete(Action onComplete)
        {
            if (this._onComplete != null)
            {
                Debug.LogWarning("Timer already has onComplete function,you will override it");
            }
            this._onComplete = onComplete;
            return this;
        }
        public Timer SetOnUpdate(Action<float> onUpdate)
        {
            if (this._onUpdate != null)
            {
                Debug.LogWarning("Timer already has onUpdate function,you will override it");
            }
            this._onUpdate = onUpdate;
            return this;
        }


        /// <summary>
        /// 0表示一直循环播放
        /// >0表示循环播放对应次数
        /// <0为非法输入
        /// </summary>
        /// <param name="loop"></param>
        /// <returns></returns>
        public Timer SetLoop(int loop)
        {
            if (loop < 0)
            {
                Debug.LogError("loop can not less than 0, we force set it to 1");
                loop = 1;
            }
            this.loop = loop;
            return this;
        }
        public Timer SetUseRealTime(bool useRealTime)
        {
            this.useRealTime = useRealTime;
            return this;
        }
        public Timer SetOwner(MonoBehaviour owner)
        {
            this._autoDestroyOwner = owner;
            return this;
        }
        public Timer SetEase(Ease ease)
        {
            easeFunc = EaseTool.Get(ease);
            return this;
        }
        public Timer SetTag(string tag)
        {
            this.tag = tag;
            return this;
        }
        public void SetId(int id)
        {
            this.id = id;
        }


        public void Update()
        {
            if (this.isDone)
            {
                return;
            }
            if (this.isPaused)
            {
                this._startTime += this.GetTimeDelta();
                this._lastUpdateTime = this.GetWorldTime();
                return;
            }

            this._lastUpdateTime = this.GetWorldTime();
            if (this._onUpdate != null)
            {
                this._onUpdate(this.easeFunc(this.GetTimeElapsed() / this.duration));
            }
            if (this.GetWorldTime() >= this.GetFireTime())
            {
                if (this._onComplete != null)
                {
                    this._onComplete();
                }
                loopedCount++;
                if (loop != 1)
                {
                    if (loop == 0)
                    {
                        this._startTime = this.GetWorldTime();
                    }
                    else
                    {
                        if (loopedCount < loop)
                        {
                            this._startTime = this.GetWorldTime();
                        }
                        else
                        {
                            this.isCompleted = true;
                        }
                    }

                }
                else
                {
                    this.isCompleted = true;
                }
            }
        }

        public void Cancel()
        {
            if (this.isDone)
            {
                return;
            }
            this._timeElapsedBeforeCancel = this.GetTimeElapsed();
            this._timeElapsedBeforePause = null;
        }
        public void Pause()
        {
            if (this.isPaused || this.isDone)
            {
                return;
            }
            this._timeElapsedBeforePause = this.GetTimeElapsed();
        }

        public void Resume()
        {
            if (!this.isPaused || this.isDone)
            {
                return;
            }
            this._timeElapsedBeforePause = null;
        }

        public float GetTimeRemaining()
        {
            return this.duration - this.GetTimeElapsed();
        }

        public float GetRatioComplete()
        {
            return this.GetTimeElapsed() / this.duration;
        }
        public float GetRatioRemaining()
        {
            return this.GetTimeRemaining() / this.duration;
        }



    }
}

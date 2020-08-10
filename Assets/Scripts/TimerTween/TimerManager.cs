using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ZGame.TimerTween
{
    class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        private int id = 0;
        private List<Timer> _timers = new List<Timer>();

        private List<Timer> _timersToAdd = new List<Timer>();
        public void RegisterTimer(Timer timer)
        {
            TimerGlobal.Counter++;
            if (GetTimer(TimerGlobal.Counter) != null)
            {
                Debug.LogError("can not register timer, for duplicated id:" + TimerGlobal.Counter);
                return;
            }

            timer.SetId(TimerGlobal.Counter);
            this._timersToAdd.Add(timer);
        }
        public void CancelAllTimers()
        {
            foreach (Timer timer in this._timers)
            {
                timer.Cancel();
            }

            this._timers = new List<Timer>();
            this._timersToAdd = new List<Timer>();
        }

        public void PauseAllTimers()
        {
            foreach (Timer timer in _timers)
            {
                timer.Pause();
            }
        }

        public void ResumeAllTimers()
        {
            foreach (Timer timer in _timers)
            {
                timer.Resume();
            }
        }

        public void CancelTimer(Timer timer)
        {
            if (this._timersToAdd.Contains(timer))
            {
                this._timersToAdd.Remove(timer);
            }
            if (this._timers.Contains(timer))
            {
                this._timers.Remove(timer);
            }
            timer.Cancel();
        }

        public Timer GetTimer(int id)
        {
            foreach (Timer timer in this._timersToAdd)
            {
                if (timer.id == id)
                {

                    return timer;
                }
            }
            foreach (Timer timer in this._timers)
            {
                if (timer.id == id)
                {
                    return timer;
                }
            }
            return null;
        }

        public void CancelTimer(int id)
        {
            Timer timer = GetTimer(id);
            if (timer != null)
            {
                timer.Cancel();
            }
        }

        private void Update()
        {
            this.UpdateAllTimers();
        }
        void UpdateAllTimers()
        {
            if (this._timersToAdd.Count > 0)
            {
                this._timers.AddRange(this._timersToAdd);
                this._timersToAdd.Clear();
            }
            foreach (Timer timer in _timers)
            {
                timer.Update();
            }

            this._timers.RemoveAll(t => t.isDone);

        }



    }
}

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
        private List<Timer> timers = new List<Timer>();

        private List<Timer> timersToAdd = new List<Timer>();


        public void RegisterTimer(Timer timer)
        {
            TimerGlobal.Counter++;
            if (GetTimer(TimerGlobal.Counter) != null)
            {
                Debug.LogError("can not register timer, for duplicated id:" + TimerGlobal.Counter);
                return;
            }

            timer.SetId(TimerGlobal.Counter);
            this.timersToAdd.Add(timer);
        }

        public void CancelAllTimers()
        {
            foreach (Timer timer in this.timers)
            {
                timer.Cancel();
            }

            this.timers = new List<Timer>();
            this.timersToAdd = new List<Timer>();
        }

        public void PauseAllTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Pause();
            }
        }

        public void ResumeAllTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Resume();
            }
        }

        public void CancelTimer(Timer timer)
        {
            if (this.timersToAdd.Contains(timer))
            {
                this.timersToAdd.Remove(timer);
            }
            if (this.timers.Contains(timer))
            {
                this.timers.Remove(timer);
            }
            timer.Cancel();
        }

        public Timer GetTimer(int id)
        {
            foreach (Timer timer in this.timersToAdd)
            {
                if (timer.ID == id)
                {

                    return timer;
                }
            }
            foreach (Timer timer in this.timers)
            {
                if (timer.ID == id)
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
            this.updateAllTimers();
        }
        void updateAllTimers()
        {
            if (this.timersToAdd.Count > 0)
            {
                this.timers.AddRange(this.timersToAdd);
                this.timersToAdd.Clear();
            }
            foreach (Timer timer in timers)
            {
                timer.Update();
            }

            this.timers.RemoveAll(t => t.IsDone);

        }
    }
}

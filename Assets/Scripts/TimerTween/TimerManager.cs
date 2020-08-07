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
        private List<Timer> _timers = new List<Timer>();

        private List<Timer> _timersToAdd = new List<Timer>();
        public void RegisterTimer(Timer timer)
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZGame.cc
{
    public class ActionEntity
    {
        public Action action;
        public int playCount;
        public ActionEntity(Action _action, int _playCount)
        {
            this.action = _action;
            this.playCount = _playCount;

        }
    }
    public class ActionComp : MonoBehaviour
    {
        /// <summary>
        /// 1表示动作只播放一遍；-1表示动作循环播放；2、3、4...表示动作播放指定次数
        /// </summary>
        public int repeatCount = 1;
        Queue<ActionEntity> legalActions = new Queue<ActionEntity>();
        //Queue<ActionEntity> cycleActions = new Queue<ActionEntity>();
        public ActionEntity curPlayActionEntity = null;
        public void AddAction(Action action)
        {
            legalActions.Enqueue(new ActionEntity(action, 0));
        }
        public void AddAction(Action[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                legalActions.Enqueue(new ActionEntity(actions[i], 0));
            }


        }



        public void Run()
        {
            this.curPlayActionEntity = null;
            if (legalActions.Count > 0)
            {
                this.curPlayActionEntity = legalActions.Dequeue();
                if (this.repeatCount == 1)
                {
                }
                else
                {
                    this.legalActions.Enqueue(this.curPlayActionEntity);
                }

                curPlayActionEntity.action.Run();
            }
            else
            {
                Debug.Log(this.gameObject.name + " 动作播放完毕");
            }
        }

        private void OnDisable()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (this.curPlayActionEntity != null)
            {
                if (this.curPlayActionEntity.action.Update())
                {
                    this.Run();
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZGame.cc
{
    public class TweenManager : SingletonMonoBehaviour<TweenManager>
    {
        Dictionary<GameObject, List<TweenComp>> dicOfObjTweens = new Dictionary<GameObject, List<TweenComp>>();

        int maxCount = 10;


        public int AddTween(GameObject target, Tween tween)
        {
            //Debug.LogError("dicOfObjTweens count:" + dicOfObjTweens.Count);


            if (dicOfObjTweens.Count == maxCount)
            {
                Debug.LogError("can not add tween to target:" + target.name + ", for tween has reached max:" + maxCount);
                return -1;
            }
            int id = getFreeId();
            if (id == -1)
            {
                return id;
            }
            tween.SetHolder(target);
            tween.SetId(id);


            tween.TweenFinished += TweenFinished;
            var tweenComp = target.AddComponent<TweenComp>();
            tweenComp.AddTween(tween);

            this.addTweenComp(target, tweenComp);
            return id;
        }

        int getFreeId()
        {
            for (int i = 1; i <= maxCount; i++)
            {
                bool flag = false;
                foreach (var pair in this.dicOfObjTweens)
                {
                    if (pair.Value.Find((a) => a.GetTween().GetId() == i))
                    {
                        flag = true;
                        continue;
                    }
                }
                if (flag)
                {
                    continue;
                }
                return i;
            }

            Debug.LogError("no free id left");
            return -1;
        }
        private void TweenFinished(object sender, TweenFinishedEventArgs e)
        {
            //Currently TweenManager  listened to each tween's finish event.
            //TweenManager will remove finished tween.
            //TODO:tween can not be remove after finish
            this.RemoveTween(e.Holder, e.Tween);

        }


        bool addTweenComp(GameObject target, TweenComp tweenComp)
        {
            if (!dicOfObjTweens.ContainsKey(target))
            {
                dicOfObjTweens[target] = new List<TweenComp>();
            }
            var tweenComps = dicOfObjTweens[target];
            for (int i = 0; i < tweenComps.Count; i++)
            {
                if (tweenComps[i].GetTween() == tweenComp.GetTween())
                {
                    Debug.LogError("dicOfTweens have the same tween, can not add");
                    return false;
                }
            }
            tweenComps.Add(tweenComp);
            return true;
        }

        /// <summary>
        /// remove all tweens of all targets
        /// </summary>
        public void RemoveAllTweens()
        {
            List<GameObject> allKeys = this.dicOfObjTweens.Keys.ToList();
            foreach (var item in allKeys)
            {
                this.RemoveAllTweensFromTarget(item);
            }

        }

        public bool RemoveAllTweensFromTarget(GameObject target)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return false;
            }
            if (!dicOfObjTweens.ContainsKey(target))
            {
                Debug.LogError("error, dicOfTweens not contains target");
                return false;
            }

            var tweenComps = dicOfObjTweens[target];
            TweenComp tweenComp = null;
            for (int i = tweenComps.Count - 1; i >= 0; i--)
            {
                tweenComp = tweenComps[i];
                GameObject.Destroy(tweenComp);
            }
            dicOfObjTweens.Remove(target);
            return true;
        }


        /// <summary>
        /// remove tween from target
        /// </summary>
        /// <param name="tween"></param>
        public bool RemoveTween(GameObject target, Tween tween)
        {
            if (tween == null)
            {
                Debug.LogError("error, tween is null");
                return false;
            }
            return this.RemoveTweenById(target, tween.GetId());
        }


        /// <summary>
        /// remove tween by id from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="id"></param>
        public bool RemoveTweenById(GameObject target, int id)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return false;
            }

            if (!dicOfObjTweens.ContainsKey(target))
            {
                Debug.LogWarning("error, dicOfTweens not contain target");
                return false;
            }
            var tweenComps = dicOfObjTweens[target];
            TweenComp tweenComp = null;
            for (int i = tweenComps.Count - 1; i >= 0; i--)
            {
                tweenComp = tweenComps[i];
                if (tweenComp.GetTween().GetId() == id)
                {
                    tweenComps.Remove(tweenComp);
                    GameObject.Destroy(tweenComp);

                    if (tweenComps.Count == 0)
                    {
                        dicOfObjTweens.Remove(target);
                        Debug.LogError("remove!!! left:" + dicOfObjTweens.Count);
                    }

                    return true;
                }
            }
            Debug.LogWarning("target does not have the tween of id:" + id);
            return false;
        }




        public Tween GetTweenById(GameObject target, int id)
        {
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return null;
            }

            if (!dicOfObjTweens.ContainsKey(target))
            {
                Debug.LogWarning("error, dicOfTweens not contain target");
                return null;
            }
            var tweenComps = dicOfObjTweens[target];
            TweenComp tweenComp = null;
            for (int i = tweenComps.Count - 1; i >= 0; i--)
            {
                tweenComp = tweenComps[i];
                if (tweenComp.GetTween().GetId() == id)
                {
                    return tweenComp.GetTween();
                }
            }

            Debug.LogWarning("target does not have the tween of id:" + id);
            return null;
        }



        public uint GetNumberOfRunnningTweensInTarget(GameObject target)
        {
            //TODO:
            return 0;
        }

        /// <summary>
        /// Pause all tweens of target.
        /// Even you add tween after the pause command, the new tween still in pause state.
        /// </summary>
        /// <param name="target"></param>
        public void PauseTarget(GameObject target)
        {

        }

        /// <summary>
        /// Resume all tweens from pause state to run state.
        /// </summary>
        /// <param name="target"></param>
        public void ResumeTarget(GameObject target)
        {

        }

        public void PauseTween(GameObject target, Tween tween)
        {
            if (tween != null)
            {
                tween.Pause();
            }
        }

        public void PauseTweenById(GameObject target, int id)
        {
            Tween tween = GetTweenById(target, id);
            this.PauseTween(target, tween);
        }

        public void ResumeTween(GameObject target, Tween tween)
        {
            if (tween != null)
            {
                tween.Resume();
            }
        }

        public void ResumeTweennById(GameObject target, int id)
        {
            Tween tween = GetTweenById(target, id);
            this.ResumeTween(target, tween);
        }
    }
}
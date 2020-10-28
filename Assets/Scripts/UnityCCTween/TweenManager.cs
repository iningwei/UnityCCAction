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


        public void AddTween(GameObject target, Tween tween)
        {
            if (this.existSameTween(target, tween))
            {
                Debug.LogError(target.name + "already have tag:" + tween.GetTag() + ", you can not add");

                return;
            }


            tween.SetHolder(target);
            tween.TweenFinished += TweenFinished;
            var tweenComp = target.AddComponent<TweenComp>();
            tweenComp.AddTween(tween);

            this.addTweenComp(target, tweenComp);
        }

        private void TweenFinished(object sender, TweenFinishedEventArgs e)
        {
            //Currently TweenManager  listened to each tween's finish event.
            //TweenManager will remove finished tween.
            //TODO:tween can not be remove after finish            
            this.RemoveTween(e.Target, e.Tween);

        }

        bool existSameTween(GameObject target, Tween tween)
        {
            if (dicOfObjTweens.ContainsKey(target))
            {
                var tweenComps = dicOfObjTweens[target];
                for (int i = 0; i < tweenComps.Count; i++)
                {
                    if (tweenComps[i].tweenTag == tween.GetTag())
                    {
                        return true;
                    }
                }
            }

            return false;
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
            if (target == null)
            {
                Debug.LogError("error, target is null");
                return false;
            }

            if (tween == null)
            {
                Debug.LogError("tween is null");
                return false;
            }
            if (!dicOfObjTweens.ContainsKey(target))
            {
                Debug.LogError("error, dicOfTweenss not contain target");
                return false;
            }
            var tweenComps = dicOfObjTweens[target];
            TweenComp tweenComp = null;
            for (int i = tweenComps.Count - 1; i >= 0; i--)
            {
                tweenComp = tweenComps[i];
                if (tweenComp.GetTween() == tween)
                {
                    tweenComps.Remove(tweenComp);
                    GameObject.Destroy(tweenComp);
                    return true;
                }
            }

            Debug.LogError("target does not have the tween");
            return false;

        }


        /// <summary>
        /// remove tween by tag from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        public bool RemoveTweenByTag(GameObject target, int tag)
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
                if (tweenComp.tweenTag == tag)
                {
                    tweenComps.Remove(tweenComp);
                    GameObject.Destroy(tweenComp);
                    return true;
                }
            }
            Debug.LogWarning("target does not have the tween of tag:" + tag);
            return false;
        }




        public Tween GetTweenByTag(GameObject target, int tag)
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
                if (tweenComp.tweenTag == tag)
                {
                    return tweenComp.GetTween();
                }
            }

            Debug.LogWarning("target does not have the tween of tag:" + tag);
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

        public void PauseTweenByTag(GameObject target, int tag)
        {
            Tween tween = GetTweenByTag(target, tag);
            this.PauseTween(target, tween);
        }

        public void ResumeTween(GameObject target, Tween tween)
        {
            if (tween != null)
            {
                tween.Resume();
            }
        }

        public void ResumeTweennByTag(GameObject target, int tag)
        {
            Tween tween = GetTweenByTag(target, tag);
            this.ResumeTween(target, tween);
        }
    }
}
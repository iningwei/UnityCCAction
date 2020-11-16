using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public static class TweenExt
    {
        public static int RunTween(this GameObject target, Tween tween)
        {
            int id = TweenManager.Instance.AddTween(target, tween);
            return id;
        }


        /// <summary>
        /// stop and remove tween from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tween"></param>
        public static bool RemoveTween(this GameObject target, Tween tween)
        {
            return TweenManager.Instance.RemoveTween(target, tween);
        }

        /// <summary>
        /// stop and remove tween of specific id from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="id"></param>
        public static bool RemoveTween(this GameObject target, int id)
        {
            return TweenManager.Instance.RemoveTweenById(target, id);
        }

        /// <summary>
        /// stop and remove all tweens from target
        /// </summary>
        /// <param name="target"></param>
        public static bool RemoveAllTweens(this GameObject target)
        {
            return TweenManager.Instance.RemoveAllTweensFromTarget(target);
        }

        public static void PauseTween(this GameObject target, Tween tween)
        {
            TweenManager.Instance.PauseTween(target, tween);
        }
        public static void PauseTween(this GameObject target, int id)
        {
            TweenManager.Instance.PauseTweenById(target, id);
        }

        public static void ResumeTween(this GameObject target, Tween tween)
        {
            TweenManager.Instance.ResumeTween(target, tween);
        }
        public static void ResumeTween(this GameObject target, int id)
        {
            TweenManager.Instance.ResumeTweennById(target, id);
        }
    }
}
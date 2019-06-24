using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public static class TweenExt
    {
        public static void RunTween(this GameObject target, Tween tween)
        {
            TweenManager.Instance.AddTween(target, tween);
        }


        /// <summary>
        /// stop and remove tween from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tween"></param>
        public static bool StopTween(this GameObject target, Tween tween)
        {
            return TweenManager.Instance.RemoveTween(target, tween);
        }

        /// <summary>
        /// stop and remove tween of specific tag from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        public static bool RemoveTween(this GameObject target, int tag)
        {
            return TweenManager.Instance.RemoveTweenByTag(target, tag);
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
        public static void PauseTween(this GameObject target, int tag)
        {
            TweenManager.Instance.PauseTweenByTag(target, tag);
        }

        public static void ResumeTween(this GameObject target, Tween tween)
        {
            TweenManager.Instance.ResumeTween(target, tween);
        }
        public static void ResumeTween(this GameObject target, int tag)
        {
            TweenManager.Instance.ResumeTweennByTag(target, tag);
        }
    }
}
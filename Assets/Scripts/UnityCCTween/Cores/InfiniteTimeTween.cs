using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public abstract class InfiniteTimeTween : Tween
    {
        /// <summary>
        /// Set tag for an tween.
        /// </summary>
        /// <param name="tag"></param>
        public abstract InfiniteTimeTween SetTag(int tag);

        public abstract InfiniteTimeTween SetTweenName(string name);

    }
}
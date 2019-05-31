using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public abstract class InfiniteTimeAction : Action
    {
        protected int tag = 0;
        /// <summary>
        /// Get the tag of an action
        /// </summary>
        /// <returns></returns>
        public abstract int GetTag();

        /// <summary>
        /// Set tag for an action.
        /// </summary>
        /// <param name="tag"></param>
        public abstract InfiniteTimeAction SetTag(int tag);
    }
}
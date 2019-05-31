using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public abstract class InfiniteTimeAction : Action
    {
      

        /// <summary>
        /// Set tag for an action.
        /// </summary>
        /// <param name="tag"></param>
        public abstract InfiniteTimeAction SetTag(int tag);

        public abstract InfiniteTimeAction SetActionName(string name);
        
    }
}
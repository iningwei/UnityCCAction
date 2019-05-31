using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public abstract class InfiniteTimeAction : Action
    {
        protected int tag = 0;
        /// <summary>
        /// 获得动作的Tag
        /// </summary>
        /// <returns></returns>
        public abstract int GetTag();

        /// <summary>
        /// 为动作设置标签，用于识别动作
        /// </summary>
        /// <param name="tag"></param>
        public abstract InfiniteTimeAction SetTag(int tag);
    }
}
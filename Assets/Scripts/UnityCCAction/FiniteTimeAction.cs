using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 有限时长的动作
    /// </summary>
    public abstract class FiniteTimeAction : Action
    {
        /// <summary>
        /// 动作持续时间
        /// </summary>
        public float time = 0;
        public float startTime;//该动作开始的时间点
      

        /// <summary>
        /// 获得动作持续时间，单位秒
        /// </summary>
        /// <returns></returns>
        public abstract float GetDuration();

        /// <summary>
        /// 设置动作持续时间，单位秒
        /// </summary>
        /// <param name="time"></param>
        public abstract void SetDuration(float time);

        /// <summary>
        /// 返回一个新动作，新动作的执行与元动作完全相反
        /// </summary>
        public abstract void Reverse();



    }
}
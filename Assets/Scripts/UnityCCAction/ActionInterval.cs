using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// 在指定时间间隔内完成的动作
    /// </summary>
    public abstract class ActionInterval : FiniteTimeAction
    {
        protected Func<float, float> easeFunc = EaseTool.Get(Ease.Linear);
         
        public abstract ActionInterval Easing(Ease ease);
    }
}

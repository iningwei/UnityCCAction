using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// Tween can finished in certain time
    /// </summary>
    public abstract class TweenInterval : FiniteTimeTween
    {
        protected Func<float, float> easeFunc = EaseTool.Get(Ease.Linear);

        public abstract TweenInterval Easing(Ease ease);
    }
}

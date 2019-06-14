using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// Action can finished in certain time
    /// </summary>
    public abstract class ActionInterval : FiniteTimeAction
    {
        protected Func<float, float> easeFunc = EaseTool.Get(Ease.Linear);

        public abstract ActionInterval Easing(Ease ease);
    }
}

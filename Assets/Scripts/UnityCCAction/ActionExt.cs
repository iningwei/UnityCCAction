using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    public static class ActionExt
    {
        public static void RunAction(this GameObject target, Action action)
        {
            ActionManager.Instance.AddAction(action, target, false);
        }
    }
}
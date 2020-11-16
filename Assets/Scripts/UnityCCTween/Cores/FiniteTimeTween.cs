using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGame.cc
{
    /// <summary>
    /// Tween will finished in certain times
    /// </summary>
    public abstract class FiniteTimeTween : Tween
    {
       

   

        /// <summary>
        /// 1 means positive dir，-1 means negative dir
        /// </summary>
        protected int tweenDiretion = 1;

       
 
     





        protected abstract void OnPartialTweenFinished();
    


        public abstract Tween Delay(float time);




    }
}
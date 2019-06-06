using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame.cc;
using cc = ZGame.cc;

public class TestAction : MonoBehaviour
{

    Vector3 originPos;



    cc.FiniteTimeAction alphaAction;
    void Start()
    {
        this.originPos = this.gameObject.transform.position;

        //this.alphaAction = this.getAlphaAction();
        //this.gameObject.RunAction(this.alphaAction);
    }

    private FiniteTimeAction getAlphaAction()
    {
        return new cc.AlphaTo(2, 0f, true).SetRepeatTimes(0);
    }




    private void Reset()
    {
        this.gameObject.transform.position = originPos;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 120, 40), "延迟调用"))
        {
            this.gameObject.RunAction(new CallFunc((a) =>
            {
                Debug.Log("我是延迟2秒调用的");
            }).Delay(2));
        }



        if (GUI.Button(new Rect(10, 70, 120, 40), "移动"))
        {
            this.gameObject.RunAction(new MoveTo(2, new Vector3(2, 2, 0)).OnComplete((a) =>
            {
                Debug.Log("移动结束！");
                this.Reset();
            }));
        }

        if (GUI.Button(new Rect(10, 130, 120, 40), "缩放"))
        {
            this.gameObject.RunAction(new ScaleTo(3, new Vector3(2, 2, 0)).SetRepeatTimes(2).OnComplete((a) =>
            {
                this.Reset();
            }));
        }

        if (GUI.Button(new Rect(10, 190, 120, 40), "旋转"))
        {
            this.gameObject.RunAction(new Rotate(60, 0, 0, Space.Self).SetTag(100));
        }





        if (GUI.Button(new Rect(10, 250, 120, 40), "移除 旋转"))
        {
            this.gameObject.StopAction(100);
        }


        if (GUI.Button(new Rect(10, 310, 120, 40), "移除所有Action"))
        {
            this.gameObject.StopAllActions();
        }
    }
}

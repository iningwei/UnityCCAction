﻿using System;
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
        Debug.Log("originPos:" + this.originPos);

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
        if (GUI.Button(new Rect(10, 10, 80, 25), "延迟调用"))
        {
            this.gameObject.RunAction(new CallFunc((a) =>
            {
                Debug.Log("我是延迟2秒调用的");
            }).Delay(2));
        }


        #region 移动
        if (GUI.Button(new Rect(10, 50, 70, 25), "移动"))
        {
            this.gameObject.RunAction(new MoveTo(3, new Vector3(2, 3, 0)).OnComplete((a) =>
            {
                Debug.Log("移动结束！");
                this.Reset();
            }).SetTag(999).SetRepeatTimes(3));
        }
        if (GUI.Button(new Rect(90, 50, 70, 25), "暂停移动"))
        {
            this.gameObject.PauseAction(999);
        }
        if (GUI.Button(new Rect(170, 50, 70, 25), "恢复移动"))
        {
            this.gameObject.ResumeAction(999);
        }
        #endregion






        if (GUI.Button(new Rect(10, 90, 70, 25), "缩放"))
        {
            this.gameObject.RunAction(new ScaleTo(3, new Vector3(2, 2, 0)).SetRepeatTimes(2).OnComplete((a) =>
            {
                this.Reset();
            }));
        }


        #region 旋转
        if (GUI.Button(new Rect(10, 130, 70, 25), "旋转"))
        {
            this.gameObject.RunAction(new Rotate(60, 0, 0, Space.Self).SetTag(100));
        }

        if (GUI.Button(new Rect(90, 130, 70, 25), "暂停旋转"))
        {
            this.gameObject.PauseAction(100);
        }
        if (GUI.Button(new Rect(170, 130, 70, 25), "恢复旋转"))
        {
            this.gameObject.ResumeAction(100);
        }
        if (GUI.Button(new Rect(250, 130, 70, 25), "移除 旋转"))
        {
            this.gameObject.RemoveAction(100);
        }
        #endregion






        if (GUI.Button(new Rect(10, 170, 120, 25), "移除所有Action"))
        {
            this.gameObject.RemoveAllActions();
        }
    }
}

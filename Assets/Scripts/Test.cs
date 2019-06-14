using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame.cc;
using cc = ZGame.cc;

public class Test : MonoBehaviour
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
        if (GUI.Button(new Rect(10, 70, 70, 25), "移动"))
        {
            this.gameObject.RunAction(new MoveTo(3, new Vector3(2, 3, 0)).OnComplete((a) =>
            {
                Debug.Log("移动结束！");
                this.Reset();
            }).SetTag(999).SetRepeatTimes(3));
        }
        if (GUI.Button(new Rect(90, 70, 70, 25), "暂停"))
        {
            this.gameObject.PauseAction(999);
        }
        if (GUI.Button(new Rect(170, 70, 70, 25), "恢复"))
        {
            this.gameObject.ResumeAction(999);
        }
        #endregion






        if (GUI.Button(new Rect(10, 130, 70, 25), "缩放"))
        {
            this.gameObject.RunAction(new ScaleTo(3, new Vector3(2, 2, 0)).SetRepeatTimes(2).OnComplete((a) =>
            {
                this.Reset();
            }));
        }


        #region 旋转
        if (GUI.Button(new Rect(10, 190, 70, 25), "旋转"))
        {
            this.gameObject.RunAction(new Rotate(60, 0, 0, Space.Self).SetTag(100));
        }

        if (GUI.Button(new Rect(90, 190, 70, 25), "暂停旋转"))
        {
            this.gameObject.PauseAction(100);
        }
        if (GUI.Button(new Rect(170, 190, 70, 25), "恢复旋转"))
        {
            this.gameObject.ResumeAction(100);
        }
        if (GUI.Button(new Rect(250, 190, 70, 25), "移除 旋转"))
        {
            this.gameObject.RemoveAction(100);
        }
        #endregion

        #region 顺序序列
        if (GUI.Button(new Rect(10, 250, 70, 25), "顺序序列"))
        {
            this.gameObject.RunAction(new Sequence(
                new BezierTo(2, new Vector3[] { new Vector3(-2, 2) }, new Vector3(4, 1, 0)).OnComplete((a) =>
                {
                    Debug.Log("bezier finished");
                }),
                new DelayTime(1).OnComplete((a) =>
                {
                    Debug.Log("dealy finished");
                }),
                new CallFunc((a) =>
                {
                    Debug.Log("callFunc finished");
                }),
                new MoveTo(1, new Vector3(-2, -2, 0)).Easing(Ease.InBack).OnComplete((a) =>
                {
                    Debug.Log("moveTo finished");
                }).SetRepeatTimes(3)).SetTag(1000));
        }

        if (GUI.Button(new Rect(90, 250, 70, 25), "暂停"))
        {
            this.gameObject.PauseAction(1000);
        }
        if (GUI.Button(new Rect(170, 250, 70, 25), "恢复"))
        {
            this.gameObject.ResumeAction(1000);
        }
        #endregion

        #region 重复序列
        if (GUI.Button(new Rect(10, 310, 70, 25), "重复序列"))
        {
            this.gameObject.RunAction(new Repeat(2,
                new BezierTo(5, new Vector3[] { new Vector3(-2, 2) }, new Vector3(4, 1, 0)).OnComplete((a) =>
                {
                    Debug.Log("bezier finished");
                }),
                new DelayTime(2).OnComplete((a) =>
                {
                    Debug.Log("dealy finished");
                }),
            new CallFunc((a) =>
            {
                Debug.Log("callFunc finished");
            }),
            new MoveTo(2, new Vector3(-2, -2, 0)).OnComplete((a) =>
            {
                Debug.Log("moveTo finished");
            }).SetRepeatTimes(3)).SetTag(2000));
        }

        if (GUI.Button(new Rect(90, 310, 70, 25), "暂停"))
        {
            this.gameObject.PauseAction(2000);
        }
        if (GUI.Button(new Rect(170, 310, 70, 25), "恢复"))
        {
            this.gameObject.ResumeAction(2000);
        }
        #endregion

        if (GUI.Button(new Rect(10, 370, 120, 25), "移除所有Action"))
        {
            this.gameObject.RemoveAllActions();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame.cc;
using cc = ZGame.cc;

public class Test : MonoBehaviour
{
    cc.FiniteTimeTween alphaTween;


    private FiniteTimeTween getAlphaTween()
    {
        return new cc.AlphaTo(2, 0f, true).SetRepeatTimes(0);
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 80, 25), "延迟调用"))
        {
            this.gameObject.RunTween(new CallFunc((a) =>
            {
                Debug.Log("我是延迟5秒调用的");
            }).Delay(5).OnUpdate((a) =>
            {
                Debug.Log(a.ToString());
            }));
        }


        #region 移动
        if (GUI.Button(new Rect(10, 70, 70, 25), "移动"))
        {
            //new MoveTo(3, new Vector3(2, 3, 0))
            this.gameObject.RunTween(
                new BezierTo(2, new Vector3[] { new Vector3(-3, 6) }, new Vector3(7, -1, 0)).OnComplete((a) =>
                {
                    Debug.Log("bezier finished");
                }).SetTag(999).SetRepeatTimes(3).SetRepeatType(RepeatType.PingPong));
        }
        if (GUI.Button(new Rect(90, 70, 70, 25), "暂停"))
        {
            this.gameObject.PauseTween(999);
        }
        if (GUI.Button(new Rect(170, 70, 70, 25), "恢复"))
        {
            this.gameObject.ResumeTween(999);
        }
        #endregion

        if (GUI.Button(new Rect(10, 130, 70, 25), "alpha"))
        {
            this.gameObject.RunTween(new AlphaTo(3, 0, true, true).SetRepeatTimes(2).OnComplete((a) =>
              {

              }).SetRepeatType(RepeatType.PingPong).SetRepeatTimes(3));
        }

        #region 旋转
        if (GUI.Button(new Rect(10, 190, 70, 25), "旋转"))
        {
            this.gameObject.RunTween(new Rotate(60, 0, 0, Space.Self).SetTag(100));
        }

        if (GUI.Button(new Rect(90, 190, 70, 25), "暂停旋转"))
        {
            this.gameObject.PauseTween(100);
        }
        if (GUI.Button(new Rect(170, 190, 70, 25), "恢复旋转"))
        {
            this.gameObject.ResumeTween(100);
        }
        if (GUI.Button(new Rect(250, 190, 70, 25), "移除 旋转"))
        {
            this.gameObject.RemoveTween(100);
        }
        #endregion

        #region 顺序序列
        if (GUI.Button(new Rect(10, 250, 70, 25), "顺序序列"))
        {
            this.gameObject.RunTween(new Sequence(
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
            this.gameObject.PauseTween(1000);
        }
        if (GUI.Button(new Rect(170, 250, 70, 25), "恢复"))
        {
            this.gameObject.ResumeTween(1000);
        }
        #endregion

        #region 重复序列
        if (GUI.Button(new Rect(10, 310, 70, 25), "重复序列"))
        {
            this.gameObject.RunTween(new Repeat(2,
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
            this.gameObject.PauseTween(2000);
        }
        if (GUI.Button(new Rect(170, 310, 70, 25), "恢复"))
        {
            this.gameObject.ResumeTween(2000);
        }
        #endregion

        if (GUI.Button(new Rect(10, 370, 120, 25), "移除所有Tween"))
        {
            this.gameObject.RemoveAllTweens();
        }

        if (GUI.Button(new Rect(10, 430, 120, 50), "RotateTo"))
        {
            this.gameObject.RunTween(new RotateTo(5, new Vector3(400, 0, 0)).SetRepeatTimes(2).OnUpdate((t) =>
            {
                Debug.Log("time:" + t + ", localEulerAngles:" + this.gameObject.transform.localEulerAngles);
            }));
        }
    }
}

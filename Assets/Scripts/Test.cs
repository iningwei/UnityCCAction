using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame;
using ZGame.cc;
using cc = ZGame.cc;

public class Test : MonoBehaviour
{
    cc.Tween alphaTween;


    private Tween getAlphaTween()
    {
        return new cc.AlphaTo(2, 0f, true).SetRepeatTimes(0);
    }



    int moveId;
    int rotateId;

    int sequenceId;
    int repeatId;

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
            moveId = this.gameObject.RunTween(
                new BezierTo(2, new Vector3[] { new Vector3(-3, 6) }, new Vector3(7, -1, 0), Space.Self).OnComplete((a) =>
                 {
                     Debug.Log("bezier finished");
                 }).SetRepeatTimes(3).SetRepeatType(RepeatType.PingPong));
        }
        if (GUI.Button(new Rect(90, 70, 70, 25), "暂停"))
        {
            this.gameObject.PauseTween(moveId);
        }
        if (GUI.Button(new Rect(170, 70, 70, 25), "恢复"))
        {
            this.gameObject.ResumeTween(moveId);
        }
        #endregion

        if (GUI.Button(new Rect(10, 130, 70, 25), "alpha"))
        {
            this.gameObject.RunTween(new AlphaTo(3, 0, true, true).SetRepeatTimes(2).OnComplete((a) =>
              {

              }).SetRepeatType(RepeatType.PingPong));
        }



        #region 旋转
        if (GUI.Button(new Rect(10, 190, 70, 25), "旋转"))
        {
            rotateId = this.gameObject.RunTween(new Rotate(60, 0, 0, Space.Self));
        }

        if (GUI.Button(new Rect(90, 190, 70, 25), "暂停旋转"))
        {
            this.gameObject.PauseTween(rotateId);
        }
        if (GUI.Button(new Rect(170, 190, 70, 25), "恢复旋转"))
        {
            this.gameObject.ResumeTween(rotateId);
        }
        if (GUI.Button(new Rect(250, 190, 70, 25), "移除 旋转"))
        {
            this.gameObject.RemoveTween(rotateId);
        }
        #endregion




        #region 顺序序列
        if (GUI.Button(new Rect(10, 250, 70, 25), "顺序序列"))
        {
            sequenceId = this.gameObject.RunTween(new Sequence(
                new BezierTo(2, new Vector3[] { new Vector3(-2, 2) }, new Vector3(4, 1, 0), Space.Self).OnComplete((a) =>
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
                new MoveTo(1, new Vector3(-2, -2, 0), Space.Self).Easing(Ease.InBack).OnComplete((a) =>
                 {
                     Debug.Log("moveTo finished");
                 }).SetRepeatTimes(3)));
        }

        if (GUI.Button(new Rect(90, 250, 70, 25), "暂停"))
        {
            this.gameObject.PauseTween(sequenceId);
        }
        if (GUI.Button(new Rect(170, 250, 70, 25), "恢复"))
        {
            this.gameObject.ResumeTween(sequenceId);
        }
        #endregion

        #region 重复序列
        if (GUI.Button(new Rect(10, 310, 100, 35), "repeated tween"))
        {
            repeatId = this.gameObject.RunTween(new Repeat(2,
                new BezierTo(5, new Vector3[] { new Vector3(-2, 2) }, new Vector3(4, 1, 0), Space.Self).OnComplete((a) =>
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
            new MoveTo(2, new Vector3(-2, -2, 0), Space.Self).OnComplete((a) =>
             {
                 Debug.Log("moveTo finished");
             }).SetRepeatTimes(3)));
        }

        if (GUI.Button(new Rect(190, 310, 70, 25), "pause"))
        {
            this.gameObject.PauseTween(repeatId);
        }
        if (GUI.Button(new Rect(270, 310, 70, 25), "resume"))
        {
            this.gameObject.ResumeTween(repeatId);
        }
        #endregion

        if (GUI.Button(new Rect(10, 370, 120, 25), "remove all Tweens"))
        {
            this.gameObject.RemoveAllTweens();
        }

        if (GUI.Button(new Rect(10, 430, 120, 50), "RotateTo"))
        {
            this.gameObject.RunTween(new RotateTo(5, new Vector3(400, 0, 0), Space.Self).SetRepeatTimes(2).OnUpdate((t) =>
             {
                 Debug.Log("time:" + t + ", localEulerAngles:" + this.gameObject.transform.localEulerAngles);
             }));
        }

        if (GUI.Button(new Rect(10, 490, 220, 40), "MoveTo+IgnoreTimeScale"))
        {
            this.gameObject.RunTween(new MoveTo(11, new Vector3(5, 0, 0), Space.World).OnComplete((a) =>
            {
                Debug.LogError("move finished");
            }).IgnoreTimeScale(true));
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Time.timeScale >= 0.2f)
            {
                Time.timeScale -= 0.2f;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }
    }
}

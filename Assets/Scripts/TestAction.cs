using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame.cc;
using cc = ZGame.cc;

public class TestAction : MonoBehaviour
{
    cc.FiniteTimeAction action;
    cc.FiniteTimeAction scaleAction;
    cc.InfiniteTimeAction rotateAction;
    cc.FiniteTimeAction alphaAction;
    void Start()
    {
        //this.action = this.getAction();
        this.scaleAction = this.getScaleAction();
        this.rotateAction = this.getRotateAction();
        this.alphaAction = this.getAlphaAction();

        //this.gameObject.RunAction(this.action);
        this.gameObject.RunAction(this.rotateAction);
        this.gameObject.RunAction(this.scaleAction);
        this.gameObject.RunAction(this.alphaAction);
    }

    private FiniteTimeAction getAlphaAction()
    {
        return new cc.AlphaTo(2, 0.2f, true).SetRepeatTimes(3);
    }

    private InfiniteTimeAction getRotateAction()
    {
        return new cc.Rotate(80, 0, 0, Space.Self);
    }

    FiniteTimeAction getScaleAction()
    {
        return new cc.Sequence(new cc.ScaleTo(20, new Vector3(2f, 1.5f, 1.5f)),
            new cc.ScaleTo(20, Vector3.one));


    }
    cc.FiniteTimeAction getAction()
    {

        //  return new cc.CallFunc(() =>
        //{
        //    Debug.Log("aaa@");
        //}).SetRepeatTimes(3);

        //return new cc.MoveTo(2, new Vector3(2, 5, 0)).SetRepeatTimes(0);

        //  Debug.Log("xx");
        //  return new cc.CallFunc(() =>
        //{
        //    Debug.Log("aaa@" + Time.time);
        //}).SetRepeatTimes(0);


        //return new cc.MoveTo(2, new Vector3(2, 3, 0));


        //return new cc.MoveTo(2, new Vector3(2, 3, 0)).Easing(Ease.Linear).SetRepeatTimes(2);

        //return new cc.DelayTime(1).SetRepeatTimes(2);


        //return new cc.Repeat(2,
        //    new cc.MoveTo(2, new Vector3(2, 3, 0)).Easing(Ease.Linear),
        //    new cc.CallFunc((a) =>
        //    {
        //        Debug.Log("hello");
        //    }).SetRepeatTimes(3).Delay(2)
        //    );



        //return new cc.Repeat(2,
        //    new cc.MoveTo(3, new Vector3(2, 5, 0)).Easing(Ease.Linear),
        //    new cc.CallFunc(() =>
        //    {
        //        Debug.Log("hello");
        //    }).SetRepeatTimes(3),
        //    new cc.MoveTo(2, new Vector3(1, 1, 0)).Easing(Ease.Linear)
        //    ).SetRepeatTimes(3);


        //return new cc.Repeat(2,
        //    new cc.CallFunc(() =>
        //    {
        //        Debug.Log("hello1");
        //    }),
        //    new cc.Repeat(2,
        //        new cc.MoveTo(2, new Vector3(0, 0, 0)).SetTag(111),
        //        new cc.CallFunc(() =>
        //        {
        //            Debug.Log("yes");
        //        })
        //        ).SetTag(2),
        //    new cc.DelayTime(2),
        //    new cc.CallFunc(() =>
        //    {
        //        Debug.Log("hello2");
        //    })
        //    ).SetTag(1);




        //return new cc.Sequence(
        //    new cc.DelayTime(1),
        //    new cc.CallFunc(() =>
        //    {
        //        Debug.Log("hello1");

        //    }).SetRepeatTimes(2),
        //    new cc.DelayTime(5),
        //    new cc.Repeat(2,
        //        new cc.MoveTo(1, new Vector3(1, 1)),
        //        new cc.Repeat(2,
        //            new cc.CallFunc(() =>
        //            {
        //                Debug.Log("dot");
        //            }))
        //     ),
        // new cc.CallFunc(() =>
        // {
        //     Debug.Log("hello2");
        // })).SetRepeatTimes(2);


        return new cc.BezierTo(3, new Vector3[] { new Vector3(-2, 2, 0) }, new Vector3(2, 5, 0)).Easing(Ease.InBack).SetRepeatTimes(3).OnComplete((a) =>
        {
            Debug.Log("finished bezierTo");
        });
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "移除ScaleAction"))
        {
            this.gameObject.StopAction(this.scaleAction);
        }
        if (GUI.Button(new Rect(10, 60, 100, 30), "移除所有Action"))
        {
            this.gameObject.StopAllActions();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame.cc;
using cc = ZGame.cc;

public class TestAction : MonoBehaviour
{
    cc.FiniteTimeAction action;

    void Start()
    {
        this.action = this.getAction();
        this.gameObject.RunAction(this.action);
    }

    cc.FiniteTimeAction getAction()
    {
        //  return new cc.CallFunc(() =>
        //{
        //    Debug.Log("aaa@");
        //}).SetRepeatTimes(3);


        //return new cc.MoveTo(2, new Vector3(2, 3, 0));


        //return new cc.MoveTo(2, new Vector3(2, 3, 0)).Easing(Ease.Linear).SetRepeatTimes(2);

        //return new cc.DelayTime(1).SetRepeatTimes(2);

        //return new cc.Sequence(new cc.DelayTime(1),
        //    new cc.CallFunc(() =>
        // {
        //     Debug.Log("fuch!!!");

        // }),
        // new cc.DelayTime(5),
        // new cc.CallFunc(() =>
        // {
        //     Debug.Log("ffff");
        // }));



        return new cc.Repeat(2,
            new cc.MoveTo(3, new Vector3(2, 5, 0)).Easing(Ease.Linear),
            new cc.CallFunc(() =>
            {
                Debug.Log("hello");
            }).SetRepeatTimes(3),
            new cc.MoveTo(2, new Vector3(1, 1, 0)).Easing(Ease.Linear)
            );


        //return new cc.Repeat(2,
        //    new cc.MoveTo(2, new Vector3(2, 3, 0)).SetRepeatTimes(2),
        //    new cc.CallFunc(() =>
        //    {
        //        Debug.Log("hello");
        //    }).SetRepeatTimes(3),
        //    new cc.Repeat(2,
        //        new cc.MoveTo(3, new Vector3(0, 0, 0)).SetRepeatTimes(2),
        //        new cc.CallFunc(() =>
        //        {
        //            Debug.Log("yes");
        //        })
        //        )
        //    );
    }
    // Update is called once per frame
    void Update()
    {

    }
}

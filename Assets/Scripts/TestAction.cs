using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGame.cc;
using cc = ZGame.cc;

public class TestAction : MonoBehaviour
{
    cc.Action action;

    void Start()
    {
        this.action = this.getAction();
        this.gameObject.RunAction(this.action);
    }

    cc.Action getAction()
    {
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


        return new cc.Sequence(
            new cc.MoveTo(5, new Vector3(10, 10, 0)).Easing(Ease.OutCirc),
            new cc.CallFunc(() =>
            {
                Debug.LogError("fuck@@");
            })
            );

    }
    // Update is called once per frame
    void Update()
    {

    }
}

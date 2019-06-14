# UnityCCAction

UnityCCAction is inspired by CocosCreator's action system which is really easy use and powerfull.

## Usage
Just copy folder "Scripts/UnityCCAction" to yourself project.Then you can enjoy the fluent feeling of realizing demands about time.Such as:delay an certain time to do something or move one gameobject to target position in 2 seconds.

This project is made by unity-2018.3.0f2.
I have made a scene called Demo,which will let you have an clear idea of what this project is.

## Case
``` csharp
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


```
The upper code run a sequence action,which was tag with 1000.The sequence action hold 4 child-actions,they will run one by one.

Firstly the gameobject will move to target postion (4,1,0) in 2 seconds with an trace of Bezier style.

After that,it will delay 1 seconds,then log "delay finished".

Then,it call a function.

Then,it move to (-2,-2,0) in an easing style of inback.And this "MoveTo" action will repeat 3 times.

## API
### Finished
- Holder actions:

    ``Repeat、Sequence``

- Finite actions:

    ``Repeat、Sequence、BezierTo、CallFunc、DelayTime、MoveTo、ScaleTo``

- Infinite actions:

    ``Rotate``
### NOT-Finished
``AlphaTo``
### TODO
``RotateTo、RotateBy``

## Any problem you get,please let me know.
This project use Unity2019.4.0f1(Android)

# UnityCCTween

UnityCCTween is inspired by CocosCreator's action system which is really easy use and powerfull.


## Usage
``This project is made by unity-2018.4.0f1``

Just copy folders ``Scripts/UnityCCTween`` and ``Plugins/Scripts`` to yourself project.Then you can enjoy the fluent feeling of realizing demands about time.

Such as:delay an certain time to do something or move one gameobject to target position in 2 seconds.

I have made a scene called Demo,which will let you have an clear idea of what this project is.

## Case
``` csharp
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


```
The upper code run a sequence tween,which was tag with 1000.The sequence tween hold 4 child-tweens,they will run one by one.

Firstly the gameobject will move to target postion (4,1,0) in 2 seconds with an trace of Bezier style.

After that,it will delay 1 seconds,then log "delay finished".

Then,it call a function.

Then,it move to (-2,-2,0) in an easing style of inback.And this "MoveTo" tween will repeat 3 times.

## API
### Finished
- Holder tweens:

    ``Repeat、Sequence``

- Finite tweens:

    ``Repeat、Sequence、BezierTo、CallFunc、DelayTime、MoveTo、ScaleTo、AlphaTo、RotateTo``

- Infinite tweens:

    ``Rotate``
### NOT-Finished

### TODO
- Implement ``RotateBy``.
- It relay on a singleton,not a good idea.
- Add TimesManager's some usefull functions.
- Compare with iTween,LeanTween,HotTween,DoTween.

# TimerTween
## TODO
- Implement ease function
- Implement Repeat()
- Implement Timer id

# Any problem you get,please let me know.
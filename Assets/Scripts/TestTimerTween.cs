using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ZGame;
using ZGame.TimerTween;

class TestTimerTween : MonoBehaviour
{
    public GameObject obj;

    float y;
    float z;
    private void Awake()
    {
        Application.targetFrameRate = 30;
        y = obj.transform.position.y;
        z = obj.transform.position.z;
    }
    private void Start()
    {
        //////Timer timer = TimerTween.Value(-50, 50, 2, 0.03f,
        //////      (v) =>
        //////  {
        //////      obj.transform.position = new Vector3(v, y, z);
        //////  },
        //////      () =>
        //////  {
        //////      Debug.Log("onComplete!");
        //////  }).SetEase(Ease.InOutBounce).SetLoop(0);

        //////int count = 0;
        //////TimerTween.RepeatCount(0.8f, 10, () =>
        //////{
        //////    count++;
        //////    Debug.Log("ticked:" + count);
        //////    //if (count > 5)
        //////    //{
        //////    //    return false;
        //////    //}
        //////    return true;
        //////});
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.LogError("now---->time:" + Time.time + ",real:" + Time.realtimeSinceStartup);
            TimerTween.Delay(5, () =>
            {
                Debug.LogError("delay---->time:" + Time.time + ",real:" + Time.realtimeSinceStartup);
            }).SetUseRealTime(true).Start();
        }
    }
}

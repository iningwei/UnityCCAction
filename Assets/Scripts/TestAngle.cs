using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngle : MonoBehaviour
{
    float xAngle=0;
    float yAngle=0;
    float zAngle=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(xAngle, yAngle, zAngle);
        //transform.localRotation = Quaternion.Euler(this.xAngle, this.yAngle, this.zAngle);
        Debug.Log("x:" + transform.localEulerAngles.x);

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 50), "x +"))
        {
            xAngle += 10;
        }
        if (GUI.Button(new Rect(250, 100, 100, 50), "x -"))
        {
            xAngle -= 10;
        }

        if (GUI.Button(new Rect(100, 200, 100, 50), "y +"))
        {
            yAngle += 10;
        }
        if (GUI.Button(new Rect(250, 200, 100, 50), "y -"))
        {
            yAngle -= 10;
        }

        if (GUI.Button(new Rect(100, 300, 100, 50), "z +"))
        {
            zAngle += 10;
        }
        if (GUI.Button(new Rect(250, 300, 100, 50), "z -"))
        {
            zAngle -= 10;
        }

    }
}

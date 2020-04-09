using UnityEngine;
using System.Collections;

public class CameraColor : MonoBehaviour {


    public static CameraColor instance;
    private GoTween tween;
    public Color defaultColor
    {
        get;
        set;
    }

    void Start()
    {
        defaultColor = Camera.main.backgroundColor;
        if (instance == null) {instance = this;}
    }

    public void SetColor(float time,GoEaseType ease,Color cor,int repetition = -1)
    {
        tween = Go.to(Camera.main, time, new GoTweenConfig().colorProp("backgroundColor", cor, false).setIterations(repetition, GoLoopType.PingPong).setEaseType(ease));
    }
    public void SetDefault()
    {
        Stop();
        Go.to(Camera.main, 0.1f, new GoTweenConfig().colorProp("backgroundColor", defaultColor, false));
    }
    public void Stop()
    {
        if (tween == null)
            return;
        tween.complete();
        tween.destroy();
    }
}

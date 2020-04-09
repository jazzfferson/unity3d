using UnityEngine;
using System.Collections;

public class ButtonSelectLevel : MonoBehaviour {

    public static ButtonSelectLevel instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Animacao()
    {
        Go.to(transform, 0.2f, new GoTweenConfig().scale(0.1f, true).setIterations(4, GoLoopType.PingPong).setEaseType(GoEaseType.CubicInOut));
    }
  

}

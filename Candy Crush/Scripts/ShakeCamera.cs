using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

    public void Shake()
    {
        Vector3 mag = new Vector3(0,0,2f);
        Go.to(GetComponent<Transform>(), 0.5f, new GoTweenConfig().shake(mag, GoShakeType.Eulers, 3, false));
    }
}

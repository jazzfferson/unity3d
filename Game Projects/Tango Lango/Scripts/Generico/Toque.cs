using UnityEngine;
using System.Collections;

public class Toque : MonoBehaviour
{
    public static Touch[] touches;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Toque.touches = Input.touches;
        }
        else
        {
            Toque.touches = null;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisoresManager : MonoBehaviour
{
    public delegate void ColisoresManagerEventHandler();
    public event ColisoresManagerEventHandler OnBallFallOut;

    public BoxTrigger[] canaletaTriggers;
    

    void Awake()
    {
        foreach (BoxTrigger boxTrigger in canaletaTriggers)
        {
            boxTrigger.OnCollisionEnterEvent += BoxTrigger_OnEnterCollision;
        }
    }

    private void BoxTrigger_OnEnterCollision(Collision other)
    {
        if (OnBallFallOut != null)
            OnBallFallOut();
    }
}

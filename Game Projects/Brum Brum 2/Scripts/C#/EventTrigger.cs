using UnityEngine;
using System.Collections;
using System;

public class EventTrigger : MonoBehaviour {

    public delegate void EventTriggerDelegate(EventTrigger trigger, Collider other);
    public event EventTriggerDelegate Enter, Stay, Exit;

    private bool hasEnter;
    /// <summary>
    /// O nome do Trigger
    /// </summary>
    public string Name;
    /// <summary>
    /// Mais de uma id possível 
    /// caso o trigger for utilizado 
    /// para mais de uma função.
    /// </summary>
    public int[] IDs;
    [SerializeField]
    protected bool useOnEnter, useOnExit, useOnStay;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!hasEnter && useOnEnter)
        {
            if (Enter != null)
            {
                Enter(this, other);              
            }

            hasEnter = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (Exit != null && useOnExit)
        {
            Exit(this, other);
           
        }

        hasEnter = false;
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (Stay != null && useOnStay)
        {
            Stay(this, other);
        }
    }
}

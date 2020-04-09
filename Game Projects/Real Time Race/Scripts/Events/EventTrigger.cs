using UnityEngine;
using System.Collections;
using System;

public class EventTrigger : MonoBehaviour {

    public delegate void EventTriggerDelegate(string Name,int[]IDs);
    public event EventTriggerDelegate Enter, Stay, Exit;
    bool hasEnter;
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
    public bool useOnEnter, useOnExit, useOnStay;

    void OnTriggerEnter(Collider other)
    {
        if (!hasEnter && useOnEnter)
        {
            if (Enter != null)
            {
                Enter(Name, IDs);              
            }

            hasEnter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (Exit != null && useOnExit)
        {
            Exit(Name, IDs);
           
        }

        hasEnter = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (Stay != null && useOnStay)
        {
            Stay(Name, IDs);
        }
    }
}

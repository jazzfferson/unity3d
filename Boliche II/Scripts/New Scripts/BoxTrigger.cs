using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{

    public string[] tagsToCompare;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (OnEnterTrigger == null)
            return;

            foreach (string tag in tagsToCompare)
            {
                if(other.gameObject.CompareTag(tag))
                {
                   OnEnterTrigger(other.gameObject);
                }
            }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (OnExitTrigger == null)
            return;

        foreach (string tag in tagsToCompare)
        {
            if (other.gameObject.CompareTag(tag))
            {
                OnExitTrigger(other.gameObject);
            }
        }
    }
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (OnCollisionEnterEvent == null)
            return;

        foreach (string tag in tagsToCompare)
        {
            if (other.gameObject.CompareTag(tag))
            {
                OnCollisionEnterEvent(other);
            }
        }
    }
    public delegate void TriggerEventHandler(GameObject other);
    public delegate void CollisionEventHandler(Collision other);
    public event TriggerEventHandler OnEnterTrigger, OnExitTrigger;
    public event CollisionEventHandler OnCollisionEnterEvent;
}

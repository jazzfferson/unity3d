using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTrigger : BoxTrigger
{

    public float angularDrag = 50f, drag = 50f;


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            otherRigidbody.angularDrag = angularDrag;
            otherRigidbody.drag = drag;
        }
    }
}

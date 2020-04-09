using UnityEngine;
using System.Collections;

public class Section : EventTrigger {

    void Awake()
    {
        useOnEnter = true;
        useOnStay = false;
        useOnExit = false;    
    }

    private int direction = 1;
    public bool IsInvertedDirection
    {
        get
        {
            return direction == 1 ? false : true;
        }
        set
        {
            if(value)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }
    }
    public bool isValidPass;

    public bool sectionEnable = true;
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (!sectionEnable)
            return;

        Vector3 otherColliderDirection = other.attachedRigidbody.velocity.normalized;
        Vector3 myDirection = transform.forward * direction;

        if(Vector3.Dot(otherColliderDirection, myDirection)>0)
        {
            Debug.Log("Valid pass " + gameObject.name);
            isValidPass = true;
        } 
        else
        {
            Debug.Log("Invalid pass " + gameObject.name);
            isValidPass = false;
        }

        base.OnTriggerEnter(other);
    }
}

using UnityEngine;
using System.Collections;

public class SmoothFollowReplay : MonoBehaviour
{

    public Transform target;
    public float damping = 6.0f;
    public bool dinamic = true;


    void FixedUpdate()
    {

        if (!dinamic)
            return;
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * damping);


    }
}

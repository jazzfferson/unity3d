using UnityEngine;
using System.Collections;

public class ShadowCastPosition : MonoBehaviour
{
    public Transform lightTransform;
    public float dist = 12;

    void FixedUpdate()
    {
        transform.position = transform.parent.position + lightTransform.TransformDirection(Vector3.back) * dist;
        transform.LookAt(transform.parent);
    }
}

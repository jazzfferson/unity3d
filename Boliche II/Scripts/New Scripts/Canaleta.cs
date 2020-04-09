using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canaleta : MonoBehaviour
{
    public float force = 10;
    public ForceMode forceMode = ForceMode.Force;
    protected virtual void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Bola"))
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force), forceMode);
    }
}

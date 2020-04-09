using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour
{
    private const float trackGrip = 1f;
    private const float grassGrip = 0.6f;
    private const float grassDrag = 50f;

    private WheelCollider wheel;
    private WheelHit hit;
    private WheelFrictionCurve originalSideWay, originalForwardWay;
    private Rigidbody carBody;

    void Awake()
    {
        wheel = GetComponent<WheelCollider>();
        originalForwardWay = wheel.forwardFriction;
        originalSideWay = wheel.sidewaysFriction;
        carBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        #region  Detectando a superficie
        float grip = 1;
        if (wheel.GetGroundHit(out hit))
        {
            string name = hit.collider.sharedMaterial.name;
           
            if (name == "Track")
            {
                grip = trackGrip;
                carBody.drag = 0;
            }
            else if(name == "Grass")
            {
                grip = grassGrip;

                 //  Vector3 force = wheel.transform.InverseTransformDirection();
                //   force = new Vector3(0, 0, -force.z);
                   carBody.AddForceAtPosition(-carBody.velocity * grassDrag, hit.point,ForceMode.Force);
            }
        }
        else
        {
            carBody.drag = 0;
        }
        #endregion

        var newForward = originalForwardWay;
        newForward.stiffness *= grip;

        var newSide = originalSideWay;
        newSide.stiffness *= grip;

        wheel.forwardFriction = newForward;
        wheel.sidewaysFriction = newSide;
    }
}

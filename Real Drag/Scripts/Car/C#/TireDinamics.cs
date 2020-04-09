using UnityEngine;
using System.Collections;

public class TireDinamics : MonoBehaviour {

    public bool printvalue;
    
    public AnimationCurve gripCurve;
    public float forwardComp;
    public float degrigForwardFactorMultiplier = 1f, degrigSideFactorMultiplier=1f;
    public float degripCurveFactor = 1f;
    public float minimunGripForward, minimunGripSide = 0.1f;
    private WheelCollider wheel;
    private WheelHit hit;
    private WheelFrictionCurve tireFriqSide, tireFriqForw;
    float tempFriq = 1;
    float originalSlipSide, originalSlipForw;
    float value = 0;
    public TireSound tireSound;

	void Start () {

       
        wheel = GetComponent<WheelCollider>();
        tireFriqSide = wheel.sidewaysFriction;
        tireFriqForw = wheel.forwardFriction;
        originalSlipSide = tireFriqSide.stiffness;
        originalSlipForw = tireFriqForw.stiffness;
	}
	
	
	void Update () {


        Dinamics();
        FinalFriction(tireFriqForw, tireFriqSide);

      
	}

    void FinalFriction(WheelFrictionCurve forward , WheelFrictionCurve side)
    {
        wheel.forwardFriction = forward;
        wheel.sidewaysFriction = side;
    }

    void Dinamics()
    {
       
        
        RaycastHit ht;
        Vector3 ColliderCenterPoint = wheel.transform.TransformPoint(wheel.center);
        if (Physics.Raycast(ColliderCenterPoint, -wheel.transform.up, out ht, 10f))
        {
            if (!ht.collider.gameObject.CompareTag("Pista"))
            {
                tireFriqSide.stiffness = originalSlipSide /2;
                tireFriqForw.stiffness = originalSlipForw /2;

                tireSound.SetSound(0, 5);
            }

            else
            {
                tireFriqSide.stiffness = originalSlipSide;
                tireFriqForw.stiffness = originalSlipForw;
                tireSound.SetSound(1, 1);
            }
        }
    }
}

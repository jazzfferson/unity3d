using UnityEngine;
using System.Collections;

public class PossesController : MonoBehaviour {

    public delegate void FloatDelegate(float floatValue);
    public delegate void SimpleDelegate();
    public FloatDelegate throttleDelegate;
    public FloatDelegate brakeDelegate;
    public FloatDelegate steeringDelegate;
    public SimpleDelegate gearUpDelegate;
    public SimpleDelegate gearDownDelegate;

    protected bool isLeftSteering;
    protected bool isRightSteering;
   
}

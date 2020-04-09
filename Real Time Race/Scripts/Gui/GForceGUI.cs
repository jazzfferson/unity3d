using UnityEngine;
using System.Collections;

public class GForceGUI : MonoBehaviour {

    [SerializeField]
    private LineRenderer lineRender;
    [SerializeField]
    private float damping;
    [SerializeField]
    private float clampGcircleMagnitude;
    [SerializeField]
    private Transform gCircle;
    [SerializeField]
    private float scale;
    private Rigidbody rigidBody;
    private Vector3 acceleration,lastVelocity,gForce;
    private Vector3 gforce2D;
    Vector3 velocity;
    Vector3 gCirclePosition;
    MathHelper mathHelper;
   

	void Start () {
        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);
        mathHelper = new MathHelper();
	}

    void Instance_OnReferencesReady()
    {
        rigidBody = StaticReferences.Instance.car.GetComponent<Rigidbody>();
    }

    int loop;
	void FixedUpdate () {

        if (!StaticReferences.Instance.ready)
            return;
            Vector3 globalGforce = mathHelper.GforceByAcceleration(mathHelper.AccelerationByPosition(rigidBody.position, Time.fixedDeltaTime), Physics.gravity.magnitude);
            gForce = rigidBody.transform.InverseTransformDirection(globalGforce);
            gforce2D = gForce * scale;
            gforce2D = new Vector3(-gforce2D.x, -gforce2D.z, 0);
            
        
	}
    void Update()
    {
        gCirclePosition = Vector3.SmoothDamp(gCircle.transform.localPosition, gforce2D, ref velocity, damping);
        gCircle.transform.localPosition = Vector3.ClampMagnitude(gCirclePosition,clampGcircleMagnitude);
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, gCircle.position);
        
    }
}

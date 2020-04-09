using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public AnimationCurve smoothByVelocityMagnitude,distanceCurve;
    public Transform target;
    
    private Vector3 velocity;
    public float magnitude;
    private MathHelper mathHelper;
    private Camera cameraGame;
    float offsetDistance;
    Vector3 offsetCamera;
    void Start()
    {
        mathHelper = new MathHelper();
        cameraGame = GetComponent<Camera>();
        offsetCamera = transform.position - target.position;
        offsetDistance = distanceCurve.Evaluate(magnitude);
        transform.position = target.position + (offsetCamera * offsetDistance);
        
    }
	
	
	void FixedUpdate () {

        Vector3 velocityTarget = mathHelper.VelocityByPosition(target.position, Time.deltaTime);

        if (MathHelper.IsValid(velocityTarget))
        {
            magnitude = velocityTarget.magnitude;
        }

        offsetDistance = distanceCurve.Evaluate(magnitude);
        Interpolate(smoothByVelocityMagnitude.Evaluate(magnitude));
	}

    void Interpolate(float smooth)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + (offsetCamera), ref velocity, smooth);
    }
}

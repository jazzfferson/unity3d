using UnityEngine;
using System.Collections;
using UnitySampleAssets.Cameras;

public class CameraZoom : MonoBehaviour {

    public float dampSmooth;
    public float minZoom, maxZoom;
    public float minSpeed;
    public float lookAtSmoothness;
    public float lookAtVelocityMultiplier = 1;
    public float maxLookAtVelocity = 5;
    public float maxCloseDistanceFromTarget = 5;
    public float autoCamNewMoveSpeed = 0.1f;
    public CarManager carManager;
    public AutoCam autoCam;

    private Camera m_Camera;
    private float zoomSpeedRef;
    private Vector3 followSpeedRef;
    private Vector3 lookAtPosition;
    private Vector3 targetVelocity;
    private MathHelper mathHelper;
    private float originalMoveSpeedAutoCam;

    void Awake()
    {
        m_Camera = GetComponent<Camera>();
        mathHelper = new MathHelper();
        originalMoveSpeedAutoCam = autoCam.MoveSpeed;

    }

    Vector3 velocityPosition;
    void FixedUpdate()
    {
        
    }


	void LateUpdate ()
    {
        autoCam.ManualUpdate();

        float targetZoom = 0;

        if (carManager.SpeedKmh < minSpeed)
            targetZoom = minZoom;
        else
            targetZoom = maxZoom;

        m_Camera.fieldOfView = Mathf.SmoothDamp(m_Camera.fieldOfView, targetZoom, ref zoomSpeedRef, dampSmooth);

        targetVelocity = mathHelper.VelocityByPosition(carManager.transform.position, Time.deltaTime);
        velocityPosition = new Vector3(targetVelocity.x, 0, targetVelocity.z) * lookAtVelocityMultiplier;
        Vector3 velocityPositionClamped = Vector3.ClampMagnitude(velocityPosition, maxLookAtVelocity);

        lookAtPosition = Vector3.SmoothDamp(lookAtPosition, carManager.transform.position + velocityPositionClamped, ref followSpeedRef, lookAtSmoothness);

        transform.LookAt(lookAtPosition);
    }

    private float DistanceToTarget()
    {
        Vector3 camPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPosition = new Vector3(carManager.transform.position.x, 0, carManager.transform.position.z);
        return Vector3.Distance(camPosition, targetPosition); 
    }

    private bool IsBadAngle()
    {
        if (DistanceToTarget()<maxCloseDistanceFromTarget)
            return true;
        else
            return false;
    }
}

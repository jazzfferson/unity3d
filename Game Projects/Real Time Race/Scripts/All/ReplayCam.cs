using UnityEngine;
using System.Collections;
using System;

public class ReplayCam : MonoBehaviour
{

    public static ReplayCam instance;
    public Camera cameraTarget;
    public float dampingZoom = 6.0f;


    private AnimationCurve _zoomCurve;
    private Transform _target;
    private Vector3 _position;
    private int _targetFieldOfView;
    private float _distance;
    private bool _lookTarget, _dinamicZoom;


    Vector2 _rotationRange;
    float _followSpeed;
    Vector3 _followAngles,_followVelocity;
    Quaternion _originalRotation;


    void Start()
    {
       
    }

    void Instance_OnReferencesReady()
    {
        _target = StaticReferences.Instance.car.transform;
    }

    void Awake()
    {
        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);

        if (instance == null)
        {
            instance = this;
        }
    }

    void LateUpdate()
    {
        if (!StaticReferences.Instance.ready)
            return;
        if (_dinamicZoom) { SmartZoom();}
        if (_lookTarget) {FollowTarget(Time.deltaTime); }
    }

    void SmartZoom()
    {
        _distance = Vector3.Distance(_target.transform.position, transform.position);
        _targetFieldOfView = Mathf.RoundToInt(_zoomCurve.Evaluate(_distance));
        FieldOfView();
    }

    void FieldOfView()
    {
        cameraTarget.fieldOfView = Mathf.Lerp(cameraTarget.fieldOfView, _targetFieldOfView, Time.deltaTime * dampingZoom);
    }

    void FollowTarget(float deltaTime)
    {
        // we make initial calculations from the original local rotation
        transform.localRotation = _originalRotation;

        // tackle rotation around Y first
        Vector3 localTarget = transform.InverseTransformPoint(_target.position);
        float yAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        yAngle = Mathf.Clamp(yAngle, -_rotationRange.y * 0.5f, _rotationRange.y * 0.5f);
        transform.localRotation = _originalRotation * Quaternion.Euler(0, yAngle, 0);

        // then recalculate new local target position for rotation around X
        localTarget = transform.InverseTransformPoint(_target.position);
        float xAngle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg;
        xAngle = Mathf.Clamp(xAngle, -_rotationRange.x * 0.5f, _rotationRange.x * 0.5f);
        var targetAngles = new Vector3(_followAngles.x + Mathf.DeltaAngle(_followAngles.x, xAngle), _followAngles.y + Mathf.DeltaAngle(_followAngles.y, yAngle));

        // smoothly interpolate the current angles to the target angles
        _followAngles = Vector3.SmoothDamp(_followAngles, targetAngles, ref _followVelocity, _followSpeed);
        // and update the gameobject itself
        transform.localRotation = _originalRotation * Quaternion.Euler(-_followAngles.x, _followAngles.y, 0);

    }

    public void SetConfig(CameraSessionTriggerEvent triggerCameraEvent)
    {
        if (triggerCameraEvent.AlignWithTarget)
        {
            triggerCameraEvent.Align(_target.position);
        }

        _targetFieldOfView = triggerCameraEvent.PerspectiveFov;
        cameraTarget.orthographicSize = triggerCameraEvent.OrthogonalSize;
        cameraTarget.orthographic = triggerCameraEvent.IsOrthogonal;
        cameraTarget.fieldOfView = _targetFieldOfView;
        _zoomCurve = triggerCameraEvent.AnimationCurve.AnimationCurvePreset;
        _lookTarget = triggerCameraEvent.LookTarget;
        _dinamicZoom = triggerCameraEvent.DinamicZoom;

        transform.position = triggerCameraEvent.CameraLocation.position;
        _originalRotation = triggerCameraEvent.CameraLocation.localRotation;
        SetCameraLookAtRotation();

        _rotationRange = triggerCameraEvent.RotationRange;
        _followSpeed = triggerCameraEvent.FollowSpeed;

       

    
    }

    void SetCameraLookAtRotation()
    {
        // we make initial calculations from the original local rotation
        transform.localRotation = _originalRotation;

        // tackle rotation around Y first
        Vector3 localTarget = transform.InverseTransformPoint(_target.position);
        float yAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        yAngle = Mathf.Clamp(yAngle, -_rotationRange.y * 0.5f, _rotationRange.y * 0.5f);
        transform.localRotation = _originalRotation * Quaternion.Euler(0, yAngle, 0);

        // then recalculate new local target position for rotation around X
        localTarget = transform.InverseTransformPoint(_target.position);
        float xAngle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg;
        xAngle = Mathf.Clamp(xAngle, -_rotationRange.x * 0.5f, _rotationRange.x * 0.5f);
        var targetAngles = new Vector3(_followAngles.x + Mathf.DeltaAngle(_followAngles.x, xAngle), _followAngles.y + Mathf.DeltaAngle(_followAngles.y, yAngle));

        // smoothly interpolate the current angles to the target angles
        _followAngles = targetAngles;
        // and update the gameobject itself
        transform.localRotation = _originalRotation * Quaternion.Euler(-_followAngles.x, _followAngles.y, 0);
       
    }
   

}

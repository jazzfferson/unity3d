using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CarCamera : MonoBehaviour
{
    GameObject[] visuals;
    Transform[] staticPositions;

    [SerializeField]
    private int[] fieldOfViews;

    public Transform target;

    private ECameraMode mode;

    [SerializeField]
    private ECameraPosition eCameraPosition;

    [SerializeField]
    private Transform pivoCamera;

    [SerializeField]
    private float heightOffset;

    //O script está sendo usado ?
    private bool isBeingUsing;

    public bool IsBeingUsing
    {
        get { return isBeingUsing; }
        set { isBeingUsing = value; }
    }

    [SerializeField]
    private float distance;

    [SerializeField]
    private float height;
    // How much we 
    [SerializeField]
    private float heightDamping;

    [SerializeField]
    private float rotationDampingY;

    Transform cameraPosition;

    public int ActualCameraView
    {
        get;
        set;
    }

    public int Views
    {
        get;
        private set;
    }

    float cameraRoll;

    public float CameraRoll
    {
        get { return cameraRoll; }
        set { cameraRoll = value; }
    }

    int fieldOfView;

    void Start()
    {
        if (pivoCamera == null)
        {
            Debug.LogError("NULO");
        }

        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);

        fieldOfViews = MainDefinitions.CameraFovs;
        heightDamping = MainDefinitions.CameraHeightDamping;
        rotationDampingY = MainDefinitions.CameraRotationDamping;

    }

    void Instance_OnReferencesReady()
    {
        CarCameraLink cameraLink = StaticReferences.Instance.car.GetComponent<CarCameraLink>();

        distance = cameraLink.distanceFromCar;
        visuals = cameraLink.Visuals;
        target = StaticReferences.Instance.car.GetComponent<Rigidbody>().transform;
        staticPositions = cameraLink.StaticPositions;
        height = cameraLink.heightFromCar;
        cameraPosition = staticPositions[0];
        ActualCameraView = 2;
        Views = staticPositions.Length + 1;
        if (pivoCamera != null)
            pivoCamera.position = cameraPosition.position;
        CameraView(ActualCameraView);
        SetInitialCameraPosition();
    }

    void LateUpdate()
    {

        if (!StaticReferences.Instance.ready || !isBeingUsing)
            return;

        if (mode == ECameraMode.Dinamic)
        {

            var wantedRotationAngleY = target.rotation.eulerAngles.y;
            var wantedRotationAngleX = target.rotation.eulerAngles.x;
            var wantedHeight = target.transform.position.y + height;

            var currentRotationAngleY = transform.eulerAngles.y;
            var currentRotationAngleX = transform.eulerAngles.x;
            var currentHeight = pivoCamera.position.y;

            // Damp the rotation around the y-axis

            currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDampingY * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, cameraRoll);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            pivoCamera.position = target.position;
            pivoCamera.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            pivoCamera.position = new Vector3(pivoCamera.position.x, currentHeight, pivoCamera.position.z);

            pivoCamera.LookAt(target.position + new Vector3(0, heightOffset, 0));

        }
        else if (mode == ECameraMode.Static)
        {
            pivoCamera.position = cameraPosition.position;
            pivoCamera.rotation = cameraPosition.rotation;
        }

        float vel = 0;
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.SmoothDamp(gameObject.GetComponent<Camera>().fieldOfView, fieldOfView, ref vel, 0.08f);
    }

    public void Roll(float amounth)
    {
        transform.rotation = Quaternion.Euler(pivoCamera.rotation.eulerAngles.x, pivoCamera.rotation.eulerAngles.y, amounth);
    }

    void ChanceMode(ECameraMode modeCamera)
    {
        mode = modeCamera;
    }

    void ChangePosition(int indexPosition)
    {
        if (indexPosition != 2)
            cameraPosition = staticPositions[indexPosition];
        eCameraPosition = (ECameraPosition)indexPosition;
        print("Actual Camera Position is : " + eCameraPosition + " index " + indexPosition);
    }

    public void ChangeCamera()
    {
        ActualCameraView++;

        if (ActualCameraView > staticPositions.Length)
        {
            ActualCameraView = 0;
        }

        CameraView(ActualCameraView);
    }

    void CameraView(int i)
    {
        if (i < 0 || i > staticPositions.Length)
            return;

        switch (i)
        {


            case 0:
                ChangePosition(0);
                ChanceMode(ECameraMode.Static);
                Visuals(false);
                fieldOfView = fieldOfViews[0];
                break;

            case 1:
                ChangePosition(1);
                ChanceMode(ECameraMode.Static);
                Visuals(true);
                fieldOfView = fieldOfViews[1];
                break;

            case 2:
                ChangePosition(2);
                cameraPosition = null;
                ChanceMode(ECameraMode.Dinamic);
                Visuals(true);
                fieldOfView = fieldOfViews[2];
                break;
        }
    }

    void Visuals(bool enable)
    {
        foreach (GameObject obj in visuals)
        {
            obj.SetActive(enable);
        }

    }

    void SetInitialCameraPosition()
    {

        gameObject.GetComponent<Camera>().fieldOfView = fieldOfView;
        ///TESTE
        var wantedRotationAngleY = target.rotation.eulerAngles.y;
        var wantedRotationAngleX = target.rotation.eulerAngles.x;
        var wantedHeight = target.position.y + height;

        var currentRotationAngleY = transform.eulerAngles.y;
        var currentRotationAngleX = transform.eulerAngles.x;
        var currentHeight = pivoCamera.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngleY = wantedRotationAngleY;

        currentHeight = wantedHeight;
        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, cameraRoll);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        pivoCamera.position = target.position;
        pivoCamera.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        pivoCamera.position = new Vector3(pivoCamera.position.x, currentHeight, pivoCamera.position.z);

        pivoCamera.LookAt(target.position);
    }


}

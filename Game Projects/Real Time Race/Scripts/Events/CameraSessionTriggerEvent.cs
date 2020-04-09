using UnityEngine;
using System.Collections;

public class CameraSessionTriggerEvent : EventTrigger {

    //Variáveis
    [SerializeField]
    private bool dinamicZoom, isOrthogonal, lookTarget, alignWithTarget;
    [SerializeField]
    private int orthogonalSize, perspectiveFov;
    [SerializeField]
    private AnimaCurvePreset animationCurve;
    [SerializeField]
    private Transform cameraLocation;
    [SerializeField]
    private Vector2 rotationRange;  
    [SerializeField]
    private float followSpeed;


    //Propriedades
    public Transform CameraLocation
    {
        get { return cameraLocation; }
    }
    public int PerspectiveFov
    {
        get { return perspectiveFov; }
    }
    public int OrthogonalSize
    {
        get { return orthogonalSize; }
    }
    public AnimaCurvePreset AnimationCurve
    {
        get { return animationCurve; }
    }
    public bool LookTarget
    {
        get { return lookTarget; }
    }
    public bool IsOrthogonal
    {
        get { return isOrthogonal; }
    }
    public bool DinamicZoom
    {
        get { return dinamicZoom; }
    }
    public Vector2 RotationRange
    {
        get { return rotationRange; }
    }
    public float FollowSpeed
    {
        get { return followSpeed; }
    }
    public bool AlignWithTarget
    {
        get { return alignWithTarget; }
    }
    //Metodo
    public void Align(Vector3 target)
    {
        cameraLocation.LookAt(target);
    }
}

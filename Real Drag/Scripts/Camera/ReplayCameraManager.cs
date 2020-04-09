using UnityEngine;
using System.Collections;

public class ReplayCameraManager : MonoBehaviour {

    
    public EstadoCameraReplay[] estadoCamera;
    public static ReplayCameraManager instance;
    public Transform target;
  
    public Camera cameraReplay;
    public SmoothFollowReplay smoothCamera;
    
    public float minDistance;
    int indexLast = -1;

    public bool ReplayManager
    {
        get;
        set;
    }
    
	void Start () {

        if (instance == null) { instance = this;}
	}
	
	
	void Update () {


        if (!ReplayManager)
            return;

        for (int i = 0; i < estadoCamera.Length; i++)
        {
            if (Vector3.Distance(target.position, estadoCamera[i].checkPoint.position) < estadoCamera[i].raioCheckPoint && i != indexLast)
            {
                indexLast = i;

                cameraReplay.transform.position = estadoCamera[i].cameraTargetTransform.transform.position;
                cameraReplay.transform.rotation = estadoCamera[i].cameraTargetTransform.rotation;
                smoothCamera.dinamic = estadoCamera[i].dinamic;
                smoothCamera.damping = estadoCamera[i].damping;
                cameraReplay.fieldOfView = estadoCamera[i].fieldOfView;
            }
        }
       
	
	}

    [System.Serializable]
    public class EstadoCameraReplay
    {
        public Transform checkPoint;
        public Transform cameraTargetTransform;
        public int raioCheckPoint = 20;
        public int fieldOfView = 30;
        public bool dinamic = true;
        public float damping = 15;

    } 

}

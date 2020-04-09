using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField]
    private List<CinemachineVirtualCamera> cameraList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(int cameraIndex)
    {
        for (int i = 0; i < cameraList.Count; i++)
        {
            if(i == cameraIndex)
            cameraList[i].Priority = 10;
            else
            cameraList[i].Priority = 0;
        }
    }
}

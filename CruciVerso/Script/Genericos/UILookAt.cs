using UnityEngine;
using System.Collections;

public class UILookAt : MonoBehaviour {

    public Transform alvo;
	// Use this for initialization
	void Start () {

        alvo =  GameObject.Find("CameraJogo").transform;

	}
	
	// Update is called once per frame
	void Update () {

        if (alvo != null)
        {
            gameObject.transform.LookAt(alvo);
           
        }
        

	}
}

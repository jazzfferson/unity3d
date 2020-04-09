using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    public Rigidbody target;
    public UnityStandardAssets.Cameras.AutoCam autoCam;

	
	// Update is called once per frame
	void LateUpdate () {

        autoCam.ManualUpdate();
        transform.LookAt(target.transform.position);
    }
}

using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {


    Transform actualTransform;
    static Transform gameCam, replayCam;
	void Start () {

        gameCam = StaticReferences.Instance.MainCamera.transform;
        replayCam = StaticReferences.Instance.ReplayCamera.transform;

        if (ReplayControlGui.Replaying)
            actualTransform = replayCam;
        else
            actualTransform = gameCam;

        StaticReferences.Instance.OnReplayCameraChange += new StaticReferences.SimpleDelegate(Instance_OnReplayCameraChange);

	}

    void Instance_OnReplayCameraChange()
    {
        BillBoard(actualTransform.position);
    }
 
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 15 == 0)
            BillBoard(actualTransform.position);
	}
    void BillBoard(Vector3 position)
    {
        Vector3 tempPos = new Vector3(position.x, transform.position.y, position.z);
        transform.LookAt(tempPos);
    }
}

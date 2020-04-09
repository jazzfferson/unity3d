using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

    [Range(0,1)]
    public float speed;
    public float size;

	void Start () {
	
       
	}
	
	
	void Update () {
	
	}

    void OnDrawGizmos()
    {


        Gizmos.color = PathColor.nodeSpeedColor.Evaluate(speed);
        Gizmos.DrawSphere(transform.position, size);

    }
}

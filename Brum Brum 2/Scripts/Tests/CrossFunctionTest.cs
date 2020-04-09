using UnityEngine;
using System.Collections;

public class CrossFunctionTest : MonoBehaviour {

    public Transform obj1;
    public Transform obj2;

	void Update () {
       

        Debug.Log(Vector3.Dot(obj1.forward,obj2.forward));
	
	}
}

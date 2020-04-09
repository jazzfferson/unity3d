using UnityEngine;
using System.Collections;

public static class GenerateCircle {

	static Vector3 RandomCircle(Vector3 center, float radius,float angle){
		
    var ang = angle;
    Vector3 pos;
    pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
    pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
    pos.z = center.z;
    return pos;
}
	public static GameObject[] Create(Vector3 center , float radius,int number,GameObject prefab,Vector3 orientation,int InitialAngle,int FinalAngle)
	{
			GameObject[] objetos =  new GameObject[number];
			
   			for (int i = 0; i < number; i++){
        	var pos = RandomCircle(center, radius,(InitialAngle + i * FinalAngle)/number);
        	objetos[i] = (GameObject)MonoBehaviour.Instantiate(prefab, pos, Quaternion.identity);
    	}
		
		return objetos;
	}
	/*public GameObject[] CreateWithLookAt(Vector3 center , float radius,int number,GameObject prefab,Vector3 orientation,int range)
	{
			GameObject[] objetos =  new GameObject[number];
			
   			for (int i = 0; i < number; i++){
        	var pos = RandomCircle(center, radius,(i * 360)/number);
        	var rot = Quaternion.FromToRotation(orientation, center-pos);
        	objetos[i] = (GameObject)Instantiate(prefab, pos, rot);
    	}
		
		return objetos;
	}*/
}

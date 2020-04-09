using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	
	public Material shielMaterial;
	public Color cor;
	
	void OnCollisionStay(Collision other)
	{
		//if(other.gameObject.tag!="Bala")
			
		shielMaterial.color =cor;

	}
	
	void OnCollisionExit(Collision other)
	{
		//if(other.gameObject.tag!="Bala")
	    shielMaterial.colorTo(0.2f,new Color(0,0,0,0),GoMaterialColorType.Color);    
	}

		
}


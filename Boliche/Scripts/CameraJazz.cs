using UnityEngine;
using System.Collections;

public class CameraJazz : MonoBehaviour {

[HideInInspector]public Transform target ;
// The distance in the x-z plane to the target
public float distance;
float distanceRef;
public float distanceFinal;
public float velocidadeDistancia;
// the height we want the camera to be above the target
public float height = 5.0f;
// How much we 
public float heightDamping = 2.0f;
public float rotationDamping = 3.0f;
public float limiteSeguir;
public float velocidadeAbertura;
public float limiteAbertura;
[HideInInspector]public bool ativa;

Vector3 posOriginal;
Vector3 rotOriginal;

float fieldView;
	
	
	public GUISkin guiSkin;

void Start()
{
    fieldView = gameObject.GetComponent<Camera>().fieldOfView;
    posOriginal = gameObject.transform.position;
    rotOriginal = gameObject.transform.rotation.eulerAngles;
    distanceRef = distance;
}

void LateUpdate () {
	// Early out if we don't have a target
	if (!target)
		return;
		
    if (ativa && gameObject.transform.position.z < limiteSeguir)
    {

        if (gameObject.GetComponent<Camera>().fieldOfView < limiteAbertura)
            gameObject.GetComponent<Camera>().fieldOfView += velocidadeAbertura * Time.deltaTime;
			
			if(distance>distanceFinal)
			{
				distance-= velocidadeDistancia * Time.deltaTime;
			}
			
        
        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
       // transform.LookAt(target);

      
    }
 /*   else
    {
        gameObject.transform.position = posOriginal;
        gameObject.transform.rotation = Quaternion.Euler(rotOriginal);
    }*/
}


    public void ResetarCamera()
    {
		distance= distanceRef;
        Hashtable ht = iTween.Hash("from", GetComponent<Camera>().fieldOfView, "to", fieldView, "time", 1.5f, "onupdate", "updateFromValue", "easetype", iTween.EaseType.easeOutExpo);
        iTween.MoveTo(gameObject,iTween.Hash("x", posOriginal.x, "y", posOriginal.y, "z", posOriginal.z, "time", 1, "looptype", iTween.LoopType.none, "easetype", iTween.EaseType.easeInCubic));
        iTween.RotateTo(gameObject, iTween.Hash("x", rotOriginal.x, "y", rotOriginal.y, "z", rotOriginal.z, "time", 1, "looptype", iTween.LoopType.none, "easetype", iTween.EaseType.easeInCubic));
        iTween.ValueTo(gameObject, ht);
    }
    public void updateFromValue(float newValue)
    {
        gameObject.GetComponent<Camera>().fieldOfView = newValue;    
    }
	
	void OnGUI()
	{
		
		
	    Matrix4x4 oldMatrix;
		Matrix4x4 tMatrix;
	    int width = 480; //Reference resolution
		int height = 800;
 
		oldMatrix = GUI.matrix; //Store current matrix
		tMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1.0f*Screen.width/width, 1.0f*Screen.height/height, 1.0f)); //Construct matrix to scale to actual view size
		GUI.matrix = tMatrix; //Set the GUI matrix to the scaling matrix
 
		GUI.skin = guiSkin;
		GUI.Label(new Rect (100, 0, 100, 100), "Soy"); 
 
		GUI.matrix = oldMatrix; //Set the original matrix back
		
		
		
	}
	
	
}

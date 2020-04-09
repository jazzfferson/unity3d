using UnityEngine;
using System.Collections;

public class RedCircle : MonoBehaviour {
	// for mouse controller
	public float mousePos;
	// for 2d position calc
	public Camera cam;
	
	
	// prefab to be instantieated
	public GameObject redBulletPrefab;
	public float redBulletXSpeed;
	public float redBulletYSpeed;
	public float bulletSpeed = 3;
	
	
	// Use this for initialization
	void Start () {
		cam = Camera.main.GetComponent<Camera>(); // get the Main Camera instance
	}
	
	// Update is called once per frame
	void Update () {
		// get the positions of mouse
		//mousePos = Input.GetAxis("Mouse X");
		
		// our red circle object ˝follows˝ the mouse
		attachObjectToMouse ();
		
		// if left mouse clicks instantiate our bullets
		if (Input.GetMouseButtonDown(0)){
            
			// 4 directions 
			for (int i = 0; i < 4; i++) {
				
				// calc the direction of the bullet
				redBulletXSpeed = bulletSpeed * Mathf.Cos(i * Mathf.PI / 2); //redBullet.xSpeed=bulletSpeed*Math.cos(i*Math.PI/2);
            	redBulletYSpeed = bulletSpeed * Mathf.Sin(i * Mathf.PI / 2); //redBullet.ySpeed=bulletSpeed*Math.sin(i*Math.PI/2);
				
				// create a instance of bullet
				GameObject clone = Instantiate(redBulletPrefab, transform.position, transform.rotation) as GameObject;
				clone.SendMessage("setBulletXSpeed", redBulletXSpeed);
				clone.SendMessage("setBulletYSpeed", redBulletYSpeed);
			
			}
			
		}
	}
	
	
	private void attachObjectToMouse () {
		Vector3 screenPos = Input.mousePosition; 
		
		// if we want to set fixed positions, like z or y:
		//screenPos.z = transform.position.z; //3f;
		//screenPos.y = this.gameObject.transform.position.y; //200 is it in px?;
		
		// need convert 2d coordenates (from mouse) to 3d world coordenates (unities)
		//Vector3 worldPos = new Vector3(cam.ScreenToWorldPoint(screenPos).x,transform.position.y,transform.position.z); 
		Vector3 worldPos = new Vector3(cam.ScreenToWorldPoint(screenPos).x, cam.ScreenToWorldPoint(screenPos).y, cam.ScreenToWorldPoint(screenPos).z); 
		
		// the red circle object position
		gameObject.transform.position = worldPos;
		//gameObject.transform.Translate(new Vector3(worldPos.x,0,0) );
		//gameObject.transform.position(worldPos.x,worldPos.y,worldPos.z);// * playerSpeed * Time.deltaTime);
	}
	
}

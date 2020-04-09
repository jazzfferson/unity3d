/*
using UnityEngine;
using System.Collections;

public class CircleChain : MonoBehaviour {
	
	//public GameObject greenCirclePrefab; //used to instanciate a new green circle
	public float circleSpeed = 0.5f; // controlls green circle speed
	public float screenX = 1.28f;	// screen size. looks confusing but its because my camera size = 1 and far=10 and near=0
	public float screenY = 0.95f;
	public float speedX;
	public float speedY;
	public Camera cam;
	public GameObject fake;

	// Use this for initialization
	void Start () {
		resetCircle();
		cam = Camera.main.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//print(Camera.current.ScreenToWorldPoint( new Vector3 (Screen.width, 0, 0)).x);
		print(cam.ScreenToWorldPoint( new Vector3 (Screen.width, 0, 0)).x);
		
		transform.Translate(new Vector3(speedX, speedY, 0) * circleSpeed * Time.deltaTime); //this.gameObject.transform.position;
		
		if(transform.position.x > screenX){
			transform.position = new Vector3(-screenX, transform.position.y, transform.position.z);
		}
		if(transform.position.x < -screenX){
			transform.position = new Vector3(screenX, transform.position.y, transform.position.z);
		}
		if(transform.position.y > screenY){
			transform.position = new Vector3(transform.position.x, -screenY, transform.position.z);
		}
		if(transform.position.y < -screenY){
			transform.position = new Vector3(transform.position.x, screenY, transform.position.z);
		}
		
	}
	
	public void resetCircle(){
//		float circleX = Random.Range(-screenX, screenX);//Math.random()*2*Math.PI;
//		float circleY = Random.Range(-screenY, screenY);//Math.random()*2*Math.PI;
		float direction = Mathf.PI * Random.Range(0, 359) / 180 ;
		speedX = 	Mathf.Cos(direction);
		speedY = 	Mathf.Sin(direction);
		
		this.transform.position = new Vector3(speedX, speedY, 10); //this.gameObject.transform.position;
		
	}
}

 */
///*
using UnityEngine;
using System.Collections;

public class CircleChain : MonoBehaviour {
	
	// for screen resolution calcs - we need convert pixels to unites positions (used in the 3D world)
	public Camera cam;
	public float screenX; // = Camera.current.ScreenToWorldPoint( new Vector3 (Screen.width, 0, 0)).x;
	public float screenY; // = Camera.current.ScreenToWorldPoint( new Vector3 (0, Screen.height, 0)).y;
	
	// some controllers
	public float circleSpeed = 1f; // controlls green circle speed
	public float speedX;
	public float speedY;
	
	
	// prefab to be instantieated
	public GameObject greenBulletPrefab;
	public float greenBulletXSpeed;
	public float greenBulletYSpeed;
	public float bulletSpeed = 3;
	

	// Use this for initialization
	void Start () {
		cam = Camera.main.GetComponent<Camera>(); // get the Main Camera instance
		resetCircle(); // for Random position and direction of the green circles
		getCurrentMaxWorldScreen(); // to get the bounderies of screen. before I hardcoded, but itś better now.
	}
	
	// Update is called once per frame
	void Update () {
		// update the green circle movement
		transform.Translate(new Vector3(speedX, speedY, 0) * circleSpeed * Time.deltaTime); //this.gameObject.transform.position;
		
		// if the GreenCircle goes out the screen we manage to put it back
		if(transform.position.x > (screenX)){
			transform.position = new Vector3(-screenX, transform.position.y, transform.position.z);
		}
		if(transform.position.x < -screenX){
			transform.position = new Vector3(screenX, transform.position.y, transform.position.z);
		}
		if(transform.position.y > screenY){
			transform.position = new Vector3(transform.position.x, -screenY, transform.position.z);
		}
		if(transform.position.y < -screenY){
			transform.position = new Vector3(transform.position.x, screenY, transform.position.z);
		}
		
		// check if the screen bonderies changed
		getCurrentMaxWorldScreen();
		
		
	}
	
	// gives GreenCircle obj random position and direction.
	public void resetCircle(){
		// random degrees (360) and give back as Radians
		float direction = Mathf.PI * Random.Range(0, 359) / 180 ;
		// new directions positions
		speedX = 	Mathf.Cos(direction);
		speedY = 	Mathf.Sin(direction);
		// set positions. the position Z doesn matter and I hardcoded to 10, like the value in Main Camera
		this.transform.position = new Vector3(speedX, speedY, 0); //this.gameObject.transform.position;
		
	}
	
	// get our screen limits
	public void getCurrentMaxWorldScreen(){
		screenX = cam.ScreenToWorldPoint( new Vector3 (Screen.width, 0, 0)).x;//Camera.current.ScreenToWorldPoint( new Vector3 (Screen.width, 0, 0)).x;
		screenY = cam.ScreenToWorldPoint( new Vector3 (0, Screen.height, 0)).y;
	}
	
	
	// check for collisions
	void OnTriggerEnter(Collider obj) {
		// for debug
		//print(obj.gameObject.name);
		
		// if this object collides with the red/green Bullet
		if (obj.gameObject.name == "RedBulletPrefab(Clone)" || obj.gameObject.name == "GreenBulletPrefab(Clone)") {
			
			// calc the new bullets positions
			for (int i = 0; i < 4; i++) {
				
				// calc the direction of the bullet
				greenBulletXSpeed = bulletSpeed * Mathf.Cos(i * Mathf.PI / 2); //redBullet.xSpeed=bulletSpeed*Math.cos(i*Math.PI/2);
            	greenBulletYSpeed = bulletSpeed * Mathf.Sin(i * Mathf.PI / 2); //redBullet.ySpeed=bulletSpeed*Math.sin(i*Math.PI/2);
				
				// create a instance of bullet (green)
				GameObject clone = Instantiate(greenBulletPrefab, transform.position, transform.rotation) as GameObject;
				clone.SendMessage("setBulletXSpeed", greenBulletXSpeed);
				clone.SendMessage("setBulletYSpeed", greenBulletYSpeed);
			
			}
			// after all, destroy the green circle
			Destroy(this.gameObject);
		}
		
	}
}

// */
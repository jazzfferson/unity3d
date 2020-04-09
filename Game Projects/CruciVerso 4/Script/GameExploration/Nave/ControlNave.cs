using UnityEngine;
using System.Collections;

public class ControlNave : MonoBehaviour {
	
public static GameObject referenciaNave; 
		
[SerializeField]
private PlayerControl leftjoyStick;
	
[SerializeField]	
private GameObject naveGrafica;
	
[SerializeField]
private float maxSpeed;
[SerializeField]
private float acceleration;
[SerializeField]	
private float desacceleration;
	
[SerializeField]	
private float rotationSpeed;
	
private float movmentAmout;
private float force;
public GameObject ring;
	
	#region Propriedades
	
	public float Acceleration
{
	get {return acceleration;}
	set {acceleration = value;}
}
	public float Desacceleration
{
	get {return desacceleration;}
	set {desacceleration = value;}
}
	public float RotationSpeed	
{
	get {return rotationSpeed;}
	set {rotationSpeed = value;}
}
	public float MaxSpeed	
{
	get {return maxSpeed;}
	set {maxSpeed = value;}
}
	
	#endregion
	
void Start () {
		
		referenciaNave = gameObject;
}

void FixedUpdate () {

	MovNave(leftjoyStick.ButtonPosition);

}

void MovNave(Vector2 controlPosition)
{

	
	/*if(controlPosition.magnitude>0.5f)
	{
		force+= Time.fixedDeltaTime  * acceleration * controlPosition.magnitude;
	}
		
	else
	{
		force-=Time.fixedDeltaTime * desacceleration;
	}
	
		
	force = Mathf.Clamp(force,0,maxSpeed);
		
		
	rigidbody.velocity = transform.forward * force;
	
		*/
	
	Vector3 lastLookPosition = new Vector3(controlPosition.x,0,controlPosition.y) + transform.position;
		
	Vector3 actualLookPosition = transform.position + transform.forward;
	
	
	transform.LookAt(Vector3.Lerp(actualLookPosition,lastLookPosition,Time.deltaTime * rotationSpeed));
	
	//transform.rotation = Q = transform.position + new Vector3(acelerador,0,volante).normalized;
	
		
		
	//Atualizar posicao e rota	
	//naveGrafica.transform.eulerAngles = new Vector3(Mathf.Clamp(controlPosition.magnitude* 50,0,50),naveGrafica.transform.eulerAngles.y,90 - rigidbody.angularVelocity.y * 10);
			naveGrafica.transform.eulerAngles = Vector3.Lerp(naveGrafica.transform.eulerAngles,
			new Vector3(Mathf.Clamp(controlPosition.magnitude* 8,0,12),
			naveGrafica.transform.eulerAngles.y ,GetComponent<Rigidbody>().angularVelocity.y * 10),Time.fixedDeltaTime * 2);

}
	void Play()
	{
		Instanciador.instancia.PlaySfx(0,1);
		GameObject ojb = (GameObject)Instantiate(ring,transform.position,Quaternion.identity);
		ojb.transform.parent = transform;
	}
}

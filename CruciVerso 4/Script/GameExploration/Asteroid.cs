using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public GameObject asteroidSecundario;
	public float velocityRotation = 60f;
	Vector3 randomVelocity;
	Vector3 direction;
	float randomVelocityPosition = 5;
	public int life = 10;
	
	void Start () {
	
		
		Init();
		
	}
	
	public void Init()
	{
		direction.Normalize();
		randomVelocity = new Vector3(Random.Range(-velocityRotation,velocityRotation),Random.Range(-velocityRotation,velocityRotation),Random.Range(-velocityRotation,velocityRotation));
		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-randomVelocityPosition,randomVelocityPosition),0,Random.Range(-randomVelocityPosition,randomVelocityPosition));
	}

	void Update () {
		
		transform.Rotate(randomVelocity*Time.deltaTime);
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if(other.name=="BoundsWorld")
		{
			AsteroidGenerator.instance.OutOfWorld(gameObject);
		}
	}	
	Quaternion RandQuaternion()
	{
		return new Quaternion(Random.Range(-360,360),Random.Range(-360,360),Random.Range(-360,360),Random.Range(-360,360));
	}
	void InstaciarAsteroid()
	{
		Instantiate(asteroidSecundario,transform.position,Quaternion.identity);
	}
	void InstanciarDiamante()
	{
		GameObject diamante = PoolManager.Spawn("DiamanteRoxo");
		diamante.transform.position = transform.position;
		diamante.GetComponent<Follow>().Initialize();
	
	}
	public void Hited(Projetil proj)
	{
		 life-=proj.powerOfDestruction;
		 if (life <= 0)
          {		
			 InstanciarDiamante();
			 InstaciarAsteroid();
			 PoolManager.Despawn(gameObject);
			 Instanciador.instancia.Instanciar(1,transform.position,transform.rotation);
           }
		else
		{
			GetComponent<Rigidbody>().AddForceAtPosition((transform.position-proj.gameObject.transform.position).normalized * 15,proj.gameObject.transform.position);
		}
		
		
	}
}

using UnityEngine;
using System.Collections;


public class Missil : MonoBehaviour {
	
	public int powerOfDestruction;
	public float timeToFinalSpeed;
	Vector3 targetPosition;
	Projetil projetil;
	public Transform particles;
	
	public float velocidadeAcelerada
	{
		get;
		set;
	}
	
	float velocidadeGirar;
	bool hasTarget;
	Transform tar;
	
	public void Init (float speed,float speedStoped,Transform target = null) {
		
		velocidadeAcelerada = speedStoped;
		
		Go.to(this,timeToFinalSpeed,new GoTweenConfig().floatProp("velocidadeAcelerada",speed,false).setEaseType(
			GoEaseType.CubicInOut));
		
		if(target!=null)
		{
			StartCoroutine(targetRotina());
			velocidadeGirar = 5;
			tar = target;
		}
		
	    projetil.powerOfDestruction = powerOfDestruction;
		projetil.gameObject = gameObject;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		 
		if(tar!=null)
		{
		  velocidadeGirar = Mathf.Clamp(velocidadeGirar-=Time.deltaTime,1,100);
		  targetPosition = Vector3.Lerp(transform.position+transform.forward.normalized,tar.position,Time.deltaTime/velocidadeGirar);
		  transform.LookAt(tar);
		}
		
		 transform.Translate(Vector3.forward * Time.deltaTime * velocidadeAcelerada);
		
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.name=="BoundsWorld")
		{
			//ReleaseParticles();
			PoolManager.Despawn(gameObject);
		}
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Asteroid")
		{
			//Instanciador.instancia.PlaySfx(2,1);
			//Instanciador.instancia.Instanciar(0,transform.position,Quaternion.identity);
			PoolManager.Despawn(gameObject);
			other.SendMessage("Hited",projetil,SendMessageOptions.DontRequireReceiver);
		}
	}
	public void ReleaseParticles()
	{
		particles.parent = null;
		particles.GetComponent<ParticleSystem>().enableEmission = false;
		Destroy(particles.gameObject,3f);
	}
	
	IEnumerator targetRotina()
	{
		yield return new WaitForSeconds(0.5f);
		hasTarget = true;
		
	}

}

using UnityEngine;
using System.Collections;

public class Foguete : MonoBehaviour {
	
    public GameObject mesh;
    [SerializeField]
	private float forca;
	float originalMass;
	[HideInInspector]public bool hasMoon;
	private ParticleSystem particle;
	
	Vector3 posicaoFoguete;
	Quaternion rotationFoguete;
	public bool haslanched;
	[HideInInspector]public bool propulsionando;
	public float tempoPropulsao;
	public bool isAnimating = false;
	float targetPositionAnimation;
	
	void Start () {

	    originalMass = rigidbody.mass;
        posicaoFoguete = transform.position;
		rotationFoguete = transform.rotation;
		particle = GetComponentInChildren<ParticleSystem>();
		rigidbody.isKinematic = true;
	}

	void FixedUpdate () {

		
        if (haslanched)
        { 
            propulsao();
			if(!isAnimating)
            transform.LookAt(transform.position+rigidbody.velocity.normalized);
        }
		
	}
	public void Lauch(float forca, float tempoPropulsao)
	{
            this.forca = forca;
            this.tempoPropulsao = tempoPropulsao;
		    rigidbody.isKinematic = false;  
			gameObject.transform.parent = null;
			particle.enableEmission = true;
			StartCoroutine("Launched");
			haslanched= true;
			propulsionando = true;
			if(OnLancado!=null)
			{
				
				OnLancado(gameObject);
			}
			

			
	}
	void propulsao()
	{
		if(propulsionando)
		{
			tempoPropulsao-=Time.fixedDeltaTime;
			gameObject.rigidbody.AddForce(mesh.transform.forward * forca);
			if(tempoPropulsao<=0)
			{
				propulsionando =false;
			}
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if((other.gameObject.CompareTag("celestialBody")))
		{
			if(OnMorreu!=null)
			{
				OnMorreu(other.gameObject);
			}
		}
		
		if(other.gameObject.tag == "ColisorCenario")
		{
			if(OnColisorCenario!=null)
			{
				OnColisorCenario(other.gameObject);
			}
		}
		
		if(OnGenericCollision!=null)
		{
			OnGenericCollision(other.gameObject);
		}
	}
	public delegate void FogueteEventHandler(GameObject sender);
	public event FogueteEventHandler OnMorreu;
	public event FogueteEventHandler OnLancado;
	public event FogueteEventHandler OnColisorCenario;
	public event FogueteEventHandler OnGenericCollision;
	
	public void Reset(Transform parent)
	{
		mesh.renderer.enabled = true;
		rigidbody.drag = 0;
		if(!rigidbody.isKinematic)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		rigidbody.isKinematic = true;
		gameObject.transform.position = posicaoFoguete;
		gameObject.transform.rotation = rotationFoguete;
		propulsionando = false;
		gameObject.SetActive(true);
		gameObject.transform.parent = parent;
		hasMoon = false;
		particle.enableEmission = false;
		haslanched= false;
	}
	
	IEnumerator Launched()
	{
		
		yield return new WaitForSeconds(tempoPropulsao*20);
		particle.enableEmission = false;
	}

	
}

using UnityEngine;
using System.Collections;

public class Foguete : MonoBehaviour {
	
	[SerializeField]
	private GameObject propulsores;
	
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

	    originalMass = GetComponent<Rigidbody>().mass;
        posicaoFoguete = transform.position;
		rotationFoguete = transform.rotation;
		particle = GetComponentInChildren<ParticleSystem>();
		GetComponent<Rigidbody>().isKinematic = true;
	}

	void FixedUpdate () {

		
        if (haslanched)
        { 
            propulsao();
			if(!isAnimating)
            transform.LookAt(transform.position+GetComponent<Rigidbody>().velocity.normalized,Vector3.up);
			//  transform.rotation = Quaternion.LookRotation(transform.position+rigidbody.velocity.normalized,Vector3.left);
			
        }
		
	}
	public void Lauch(float forca, float tempoPropulsao)
	{
		
			propulsores.SetActive(true);
            this.forca = forca;
            this.tempoPropulsao = tempoPropulsao;
		    GetComponent<Rigidbody>().isKinematic = false;  
			gameObject.transform.parent = null;
			particle.enableEmission = true;
			StartCoroutine("Launched");
			haslanched= true;
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
			gameObject.GetComponent<Rigidbody>().AddForce(mesh.transform.forward * forca);
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
		mesh.GetComponent<Renderer>().enabled = true;
		GetComponent<Rigidbody>().drag = 0;
		if(!GetComponent<Rigidbody>().isKinematic)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		GetComponent<Rigidbody>().isKinematic = true;
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
		
		GetComponent<Rigidbody>().AddForce(mesh.transform.forward * 0.5f,ForceMode.Force);
		yield return new WaitForSeconds(0.4f);
		propulsionando = true;
		yield return new WaitForSeconds(tempoPropulsao);
		particle.enableEmission = false;
		propulsores.SetActive(false);
		propulsionando =false;
	}

	
}

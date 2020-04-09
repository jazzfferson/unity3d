using UnityEngine;
using System.Collections;

public class Foguete : MonoBehaviour {
	
	public float forca;
	public GameObject explosion;
	float originalMass;
	[HideInInspector]public bool hasMoon;
	ParticleSystem particle;
	
	Vector3 posicaoFoguete;
	Quaternion rotationFoguete;
	bool haslanched;
	bool propulsionando;
	bool contabiliza =true;
	public float tempoPropulsao;
	
	// Use this for initialization
	void Start () {
	    originalMass = GetComponent<Rigidbody>().mass;
		posicaoFoguete = new Vector3(-8.8f,0,60);
		rotationFoguete = transform.rotation;
		particle = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (haslanched)
        {
            propulsao();
            transform.LookAt(transform.position + transform.forward * 10);
        }
	}
	public void Lauch(float forca, float tempoPropulsao)
	{
            this.forca = forca;
            this.tempoPropulsao = tempoPropulsao;
		    GetComponent<Rigidbody>().isKinematic = false;  
			gameObject.transform.parent = null;
			particle.enableEmission = true;
			StartCoroutine("Launched");
			haslanched= true;
			propulsionando = true;
		    StartCoroutine(Colidiu());

			
	}
	void propulsao()
	{
		if(propulsionando)
		{
			tempoPropulsao-=Time.deltaTime;
			Vector3 direcao = gameObject.transform.forward;
			gameObject.GetComponent<Rigidbody>().AddForce(direcao.normalized * forca);
			if(tempoPropulsao<=0)
			{
				propulsionando =false;
			}
		}
	}
	void OnCollisionEnter(Collision other)
	{
		
		
		if(other.gameObject.name =="Planet" && contabiliza)
		{
			contabiliza = false;
			
			gameObject.transform.parent = other.gameObject.transform;
			GetComponent<Rigidbody>().isKinematic = true;  
			
			if(OnCaiu!=null)
			{
				
				OnCaiu(this);
			}
			
		
		}
		else if (other.gameObject.name =="Lua")
		{
			Instantiate(explosion,this.transform.position,Quaternion.identity);
			this.gameObject.SetActive(false);
			
			if(OnMorreu!=null)
			{
				
				OnMorreu(this);
			}
			
		}
	} 
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "MoonCollider")
		{
			print("HasMoon");
			hasMoon = true;
		}
	}
	public delegate void FogueteEventHandler(object sender);
	public event FogueteEventHandler OnCaiu;
	public event FogueteEventHandler OnMorreu;
	public void Reset()
	{
		
		
		gameObject.transform.position = posicaoFoguete;
		gameObject.transform.rotation = rotationFoguete;
		GetComponent<Rigidbody>().isKinematic = false;  
		contabiliza = true;
		propulsionando = false;
		gameObject.SetActive(true);
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		gameObject.transform.parent = null;
		hasMoon = false;
		particle.enableEmission = false;
		haslanched= false;
	}
	IEnumerator Launched()
	{
		
		yield return new WaitForSeconds(tempoPropulsao);
		particle.enableEmission = false;
	}
	IEnumerator Colidiu()
	{
		yield return new WaitForSeconds(2f);
		contabiliza = true;
	}
	
}

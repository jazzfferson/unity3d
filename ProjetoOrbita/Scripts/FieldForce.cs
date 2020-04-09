using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldForce : MonoBehaviour {

	[SerializeField]
	public GameObject dot;
	private float intervalo;
	private float tempo;
	private int iterations;
	private float intervaloEntreIterations;
	private int ringsNumber;
	private bool play;
	private Ring[] arrayRings;
	private GoEaseType _tipo;
	
	public void CreateField(Transform root,int dotsAmount,int dotsAmountMultiplier,int _ringsNumber,float innerDistance,float ringsDistance,float ringsDistanceMultiplier,float _intervalo,float _tempo,int _iterations,float _intervaloEntreIterations,int initialAngle, int finalAngle,GoEaseType tipo)
	{
		_tipo = tipo;
		ringsNumber = _ringsNumber;
		intervalo = _intervalo;
		tempo = _tempo;
		iterations = _iterations;
		intervaloEntreIterations = _intervaloEntreIterations;
		
		arrayRings = new Ring[ringsNumber];
		
		for(int i = 0; i<ringsNumber;i++)
		{
			arrayRings[i] = new Ring();
			arrayRings[i].array = GenerateCircle.Create(root.position,(ringsDistance *ringsDistanceMultiplier * i) + innerDistance , i * dotsAmount * dotsAmountMultiplier,dot,Vector3.forward,initialAngle,finalAngle);
			
			foreach(GameObject inst in arrayRings[i].array)
			{
				inst.transform.parent = transform;
			}
		}
		
		play = true;
		StartCoroutine(Play());
	}
	
	public void CancelField()
	{
		StopAllCoroutines();
		
		for(int i = 0; i<ringsNumber;i++)
		{
			Go.to(arrayRings[i],0.4f,new GoTweenConfig().floatProp("alpha",0,false).onComplete(complete=> {play = false; ClearAll();}));
		}
	}
	
	void Update () {
		
		if(!play)
			return;
		
		for(int i = 0; i<ringsNumber;i++)
		{
			for(int j = 0; j<arrayRings[i].array.Length;j++)
			{
				arrayRings[i].array[j].GetComponent<UISprite>().alpha = arrayRings[i].alpha;
			}
		}
	
	}
	
	IEnumerator Play()
	{

		for(int i = 0; i<ringsNumber;i++)
		{
			Go.to(arrayRings[i],tempo,new GoTweenConfig().floatProp("alpha",0.3f,false).setIterations(2,GoLoopType.PingPong).setDelay(i * intervalo).setEaseType(_tipo));
		}
		yield return new WaitForSeconds(intervaloEntreIterations);
		StartCoroutine(Play());
		iterations--;
		
		if(iterations<=0)
		{
			StopAllCoroutines();
			play = false;
			ClearAll();
			if(OnFinished!=null)
			{
				OnFinished();
			}
		}
		
	}
	
	class Ring
	{
		public GameObject[] array;
		public float alpha
		{
			get;
			set;
		}
		
		public void SelfDestroy()
		{
			foreach(GameObject obj in array)
			{
				MonoBehaviour.Destroy(obj);
			}
		}
	}
	
	void ClearAll()
	{
		for(int i = 0; i<ringsNumber;i++)
		{
			arrayRings[i].SelfDestroy();
		}
		arrayRings = null;
	}
	
	  public delegate void FieldForceEventHandler();
    public event FieldForceEventHandler OnFinished;
	
}

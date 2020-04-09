using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathViewer : MonoBehaviour {
	
	public GameObject Root;
 	public GameObject Sprite;
	public float minDistance;
	public int maxPoints;
	public bool limitMaxPoinsts;
	public bool colorSpeedBased;
	
	
	private Vector3 lastPosition;
	private Vector3 actualPosition;
	private List<GameObject> listaSprites;
	private float timeToDestroySprite= 0.2f;
	private Vector3 rotation;
	
	void Start () {
		
		listaSprites = new List<GameObject>();
		actualPosition = Root.transform.position;
		lastPosition = actualPosition;
	}
	
	
	void Update () {
		
		actualPosition = Root.transform.position;
		
		if(Vector3.Distance(actualPosition,lastPosition)>=minDistance)
		{
			rotation = Root.rigidbody.velocity.normalized;
		
			GameObject sprite = (GameObject)Instantiate(Sprite,actualPosition,Quaternion.identity);
			
			
		  sprite.transform.LookAt(Root.transform.position+rotation);
		
		  if(colorSpeedBased)
		  sprite.GetComponentInChildren<UISprite>().color = new Color(Root.rigidbody.velocity.magnitude/10,Root.rigidbody.velocity.magnitude/10,1,1);
			
			
			sprite.transform.parent = this.transform;
			listaSprites.Add(sprite);
			lastPosition = actualPosition;
		}
		
		if(listaSprites.Count>maxPoints && limitMaxPoinsts)
		{
		
			Destroy(listaSprites[0]);
			listaSprites.RemoveAt(0);
			listaSprites.TrimExcess();
				
		}
		 
	}

    public void Reset()
    {
		if(listaSprites==null)
			return;
        foreach (GameObject sprite in listaSprites)
        {
            Destroy(sprite);
        }

        listaSprites.Clear();

    }
}

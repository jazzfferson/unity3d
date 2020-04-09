using UnityEngine;
using System.Collections;

public class MovStars : MonoBehaviour {
	
	public Transform relativeTo;
    public float smooth;
    public float starsDistance;
    public UvOffset[] arrayUvOffset;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		Vector2 posCamera = new Vector2 (relativeTo.position.x,relativeTo.position.z);
		

        for (int i = 0; i < arrayUvOffset.GetLength(0); i++)
        {
            if (i == 0)
            {
                arrayUvOffset[i].pos = posCamera * (i + 1)/ smooth;
            }
            else
            {
                arrayUvOffset[i].pos = posCamera  * starsDistance * (i + 1)/ smooth;
            }

          
        }
	
	}
}

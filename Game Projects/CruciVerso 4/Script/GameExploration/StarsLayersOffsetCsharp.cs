using UnityEngine;
using System.Collections;

public class StarsLayersOffsetCsharp : MonoBehaviour {
	
	public float minSize;
	public float maxSize;
    public float minDistance;
    public float randomDistance;
    public GameObject star;
    public int starAmout;
    GameObject[] arrayStars;
    public Rigidbody playerRef;

	void Start () {

        arrayStars = new GameObject[starAmout];


        for (int i = 0; i < arrayStars.GetLength(0); i++)
        {
            arrayStars[i] = (GameObject)Instantiate(star,transform.position, Quaternion.identity);
            arrayStars[i].transform.parent = this.transform;
            float random = Random.Range(minSize, maxSize);
            arrayStars[i].transform.localScale = new Vector3(random, random, random);
        }

        for (int i = 0; i < arrayStars.GetLength(0); i++)
            for (int j = 0; j < arrayStars.GetLength(0); j++)
            {
               
                if(Vector3.Distance(arrayStars[i].transform.position,arrayStars[j].transform.position)<Mathf.Abs(minDistance) && i!=j)
                {
                    arrayStars[i].transform.position = arrayStars[j].transform.position + new Vector3(Random.Range(-randomDistance, randomDistance), Random.Range(-200, -800), Random.Range(-randomDistance, randomDistance));
                }
            }
		
   
	}
	
	// Update is called once per frame
	void Update () {


        Reposition();

	}

    void Reposition()
    {

		 for (int i = 0; i < arrayStars.GetLength(0); i++)
         {
			arrayStars[i].transform.position-=playerRef.velocity/60;
		 }
    }
}

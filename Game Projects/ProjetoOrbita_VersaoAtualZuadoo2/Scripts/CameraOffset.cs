using UnityEngine;
using System.Collections;

public class CameraOffset : MonoBehaviour {


    [HideInInspector]
    public bool follow = false;
    [SerializeField]
    public Transform target;
    [SerializeField]
    private float damp;
    [SerializeField]
    private Vector2 Offset;
    [HideInInspector]
    public Vector2 secondTargert; 

    private Vector2 cameraPosition;
    private Vector2 targetPosition;
	private Vector2 posicaoOriginal;


	void Start () {

		posicaoOriginal = new Vector2(transform.position.x, transform.position.y);

	}
	
	void LateUpdate () {


        cameraPosition = new Vector2(transform.position.x, transform.position.y);
        targetPosition = new Vector2(target.position.x, target.position.y);
        targetPosition = targetPosition + Offset;

        if (follow)
        {

            transform.position = Vector2.Lerp(cameraPosition, targetPosition, Time.deltaTime * damp);
        }
        else
        {
            transform.position = Vector2.Lerp(cameraPosition, posicaoOriginal, Time.deltaTime * 2);
        }
			
  
	}
}

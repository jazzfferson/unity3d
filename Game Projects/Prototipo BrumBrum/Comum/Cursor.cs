using UnityEngine;
using System.Collections;
public class Cursor : MonoBehaviour {

    public static Cursor instancia;
    public Transform target;
    public float smoothTime = 1;
    public float maxSpeed = 100;
    
    Vector3 speed;

	void Start () {

        if (instancia == null)
        {
            instancia = this;
        }
        
	}

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref speed, smoothTime, maxSpeed, Time.deltaTime);//Vector3.Lerp(transform.position, target.position, Time.deltaTime * 10);
       // transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * 20);
    }
    public void SetPosicao(Transform botao)
    {
        target = botao.transform;  
    }
}

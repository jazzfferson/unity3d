using UnityEngine;
using System.Collections;

public class OrganizeSockets : MonoBehaviour {

    public Transform[] sockets;
    public float distancia;

	void Start () {

        for (int i = 0; i < sockets.Length; i++)
        {
            sockets[i].position = new Vector3(sockets[i].position.x, sockets[i].position.y + i * distancia, sockets[i].position.z);
        }
       
	}

   
}

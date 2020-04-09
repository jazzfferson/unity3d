using UnityEngine;
using System.Collections;

public class PlaceCar : MonoBehaviour {

    public Transform pivoChao;
    public void Place(Transform trackCarPosition)
    {
         transform.rotation = trackCarPosition.rotation;
         transform.position = trackCarPosition.position 
             + new Vector3(0, Mathf.Abs(transform.position.y - pivoChao.position.y), 0);
    }
}

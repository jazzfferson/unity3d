using UnityEngine;
using System.Collections;

public class UvOffset : MonoBehaviour {

	// Use this for initialization
    public float multipler = 1;
    private Vector2 offset;
    public Vector2 Offset
    {
        set
        {
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(value.x * multipler, value.y * multipler));
            offset = value;
        }

        get
        {
            return offset;
        }
    }
   


}

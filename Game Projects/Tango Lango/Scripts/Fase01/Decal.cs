using UnityEngine;
using System.Collections;

public class Decal : MonoBehaviour {

    public Texture[] texture;
    public float tamanhoFinal;

    void Start()
    {
        renderer.material.mainTexture = texture[Random.Range(0,texture.GetLength(0))];
        transform.eulerAngles = new Vector3(90, Random.Range(0, 360), 0);
        Go.to(this.transform, 0.3f, new GoTweenConfig().scale(Random.Range(tamanhoFinal, tamanhoFinal * tamanhoFinal/2), false).setEaseType(GoEaseType.ElasticOut));
        Go.to(this.gameObject.renderer, 5f, new GoTweenConfig().materialColor(new Color(1,1,1,0)).setDelay(7));
        Destroy(this.gameObject,12f);
    }
}

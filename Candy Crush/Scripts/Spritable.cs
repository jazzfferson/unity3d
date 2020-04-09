using UnityEngine;
using System.Collections;

public class Spritable : MonoBehaviour {


  //  public SpriteRenderer highLightSpriteRender;
    [HideInInspector]
    public SpriteRenderer normalSpriteRenderer;

    public float HighLighted
    {
        get;
        set;
    }
    public float Alpha
    {
        get;
        set;
    }
    private Color cor;
    public virtual void Awake()
    {
        normalSpriteRenderer = GetComponent<SpriteRenderer>();
        cor = Color.white;

    }
    public virtual void Start()
    {


    }
    public void SetAlpha(float alpha)
    {
        Alpha = alpha;
        GetComponent<SpriteRenderer>().color = new Color(cor.r, cor.g, cor.b, alpha);
    }
    public virtual void LoadSprite(Sprite normalSprite, Sprite highLightedSprite)
    {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    //    highLightSpriteRender.sprite = highLightedSprite;
    }
    protected virtual void HighLight(float alpha)
    {        
     //   highLightSpriteRender.color = new Color(cor.r, cor.g, cor.b, alpha);
    }
    public virtual void Update()
    {
    //    HighLight(Mathf.Lerp(highLightSpriteRender.color.a, HighLighted, Time.deltaTime * 4));
     //   HighLighted = 0;

        normalSpriteRenderer.color = new Color(cor.r, cor.g, cor.b, Alpha);
    }


   

}

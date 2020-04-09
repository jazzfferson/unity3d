using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    Color corTarget;
    Hashtable hs;
    Hashtable hs2;
    public bool tweenRodando;
    public Material materialTarget;
    Material materialNormal;
    
	// Use this for initialization
	void Start () {

        materialNormal = gameObject.renderer.material;
	}

    public void TargetMode(bool active)
    {
        if (active)
        {
            gameObject.renderer.material = materialTarget;
        }
        else
        {
            gameObject.renderer.material = materialNormal;
        }
    }
	// Update is called once per frame

    public void TweenAlpha(float time, float time2, float to)
    {
        tweenRodando = true;
        corTarget = gameObject.renderer.material.GetColor("_OutlineColor");        
        hs = iTween.Hash("time", time, "to", to, "from", corTarget.a, "looptype", iTween.LoopType.none, "onupdate", "atualizar", "easetype", iTween.EaseType.easeOutCubic,"oncomplete","Terminou");
        hs2 = iTween.Hash("time", time2, "to", corTarget.a, "from", to, "looptype", iTween.LoopType.none, "onupdate", "atualizar", "easetype", iTween.EaseType.easeInOutCubic, "oncomplete", "Terminou2");
        iTween.ValueTo(gameObject, hs);
    }
    public void TweenAlpha(float time, float to)
    {
        tweenRodando = true;
        iTween.Stop(gameObject);
        corTarget = gameObject.renderer.material.GetColor("_OutlineColor");
        hs = iTween.Hash("time", time, "to", to, "from", corTarget.a, "looptype", iTween.LoopType.none, "onupdate", "atualizar", "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "Terminou2");
        iTween.ValueTo(gameObject, hs);
    }
    public void Color(Color cor,bool reset)
    {
        iTween.Stop(gameObject);
        gameObject.renderer.material.SetColor("_OutlineColor", cor);
        tweenRodando = false;
    }
   
    void atualizar(float valor)
    {
        gameObject.renderer.material.SetColor("_OutlineColor", new Color(corTarget.r, corTarget.g, corTarget.b, valor));
    }
    void Terminou()
    {
        iTween.ValueTo(gameObject, hs2);
    }
    void Terminou2()
    {
        tweenRodando = false;
    }
}

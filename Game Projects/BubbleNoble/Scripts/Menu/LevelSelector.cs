using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour
{

    public static LevelSelector instance;
    public UISprite[] stars;
    public UISprite unlocked;
    public UISprite locked;
    public UILabel numeroFase;
    bool loked;
    GoTween tweenAnimationLocked;
    

    void Start()
    {

        if (instance == null)
            instance = this;

       

    }
    public void LockedLevel(bool travado)
    {
        if (travado)
        {
            Instanciador.instancia.PlaySfx(1, 0.4f, 1);
            TweenAlpha.Begin(locked.gameObject, 0.2f, 1f);
            TweenAlpha.Begin(unlocked.gameObject, 0.2f, 0f);
            TweenColor.Begin(numeroFase.gameObject, 0.2f, new Color(0.2f, 0.2f, 0.2f, 1));

            CancelAnimation();
            tweenAnimationLocked = Go.to(transform, 0.4f, new GoTweenConfig().shake(new Vector3(0.02f, 0, 0.02f), GoShakeType.Position, 0, false).setDelay(0.2f));  
        }
        else
        {
            TweenAlpha.Begin(locked.gameObject, 0.2f, 0f);
            TweenAlpha.Begin(unlocked.gameObject, 0.2f, 1f);
            TweenColor.Begin(numeroFase.gameObject, 0.2f, Color.white);
        }

    }
    public void Stars(int quantidade)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].alpha = 0;
        }
        for (int i = 0; i < stars.Length; i++)
        {
            if (i <= quantidade-1)
            {
              //TweenAlpha.Begin(stars[i].gameObject, 0.1f, 1f).delay = i * 0.3f;
                stars[i].alpha = 1;

            }
            else
            {
                //TweenAlpha.Begin(stars[i].gameObject, 0.1f, 0f).delay = i * 0.3f;
                stars[i].alpha = 0;
            }
        }
    }
    public void CancelAnimation()
    {

        if (tweenAnimationLocked != null)
        {
            tweenAnimationLocked.destroy();
        }
        
    }


}

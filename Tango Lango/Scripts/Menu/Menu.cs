using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

    public Transform olho;
    public UISprite sombra;
    public Transform root;
    public UILabel score;
    public Transform nave;
    Vector3 posicaoRefNave;
    bool info = false;

    void Start()
    {


      
        Go.to(sombra.transform, 2f, new GoTweenConfig().rotation(new Vector3(0, 0, 2), false).setIterations(90000, GoLoopType.PingPong));
        Go.to(olho.transform, 2f, new GoTweenConfig().localPosition(new Vector3(-3, 0, 0), true).setIterations(90000, GoLoopType.PingPong));

        if (GameData.GetRecorde() <= 0)
        {
            score.text = "No Records!";
        }
        else
        {
            score.text = string.Format("{0:0.0}", GameData.GetRecorde()) + " Secs";
        }

        posicaoRefNave = nave.transform.position;
		 Animacao();
        StartCoroutine(AnimcaoNave());

    }

    IEnumerator AnimcaoNave()
    {
       
        yield return new WaitForSeconds(Random.Range(6f,10f));

        if (!info)
        {
            Animacao();
            StartCoroutine(AnimcaoNave());
        }
    }

    void Animacao()
    {
        nave.transform.position = posicaoRefNave;
        Go.to(nave.transform, 1f, new GoTweenConfig().position(new Vector3(-115.47f, 0, -53f), false).setEaseType(GoEaseType.CubicInOut));
        Go.to(nave.transform, 1f, new GoTweenConfig().shake(new Vector3(0, 0, 2), GoShakeType.Eulers).setEaseType(GoEaseType.CubicInOut).setDelay(1));
        Go.to(nave.transform, 2f, new GoTweenConfig().position(new Vector3(250, 80, -53f), false).setEaseType(GoEaseType.CubicInOut).setDelay(2f));
    }
    void Update()
    {

        if (info && Input.GetKeyDown(KeyCode.Escape))
        {
            MenuDown();
            info = false;
        }
        else if (!info && Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    void Info()
    {
        info = true;
        Go.to(root, 0.5f, new GoTweenConfig().position(new Vector3(0, -180, 0), false).setEaseType(GoEaseType.CubicInOut));

    }
    void MenuDown()
    {
        Go.to(root, 0.5f, new GoTweenConfig().position(new Vector3(0, 0, 0), false).setEaseType(GoEaseType.CubicInOut));
        info = false;
        StartCoroutine(AnimcaoNave());
        
    }
    void Quit()
    {
        Application.Quit();
    }
    void Play()
    {
        if (!GameData.firstRun)
        {
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Tutorial", 0.4f);
			//Application.LoadLevel("Tutorial");
            
        }
        else
        {
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Fase01", 0.4f);
			//Application.LoadLevel("Fase01");
        }
    }
}

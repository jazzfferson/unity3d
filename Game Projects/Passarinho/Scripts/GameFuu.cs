using UnityEngine;
using System.Collections;

public class GameFuu : MonoBehaviour {

    public Transform teto, logo;
    public static float GameSpeed,PosicaoDestroyObstaculo;
    public static bool Playing, ScoreMode;
    public static int Pontos;
    public GameObject pecasObstaculo;
    public UIPanel preMortemPanel, posMortemPanel,sairPanel;
    public UI2DSprite spriteCima;
    public Scores scoreScript;
    
    public float velocidade;
    public float posicaoFinalX,posicaoInicialX;

    float intervaloInstanciarMin, intervaloInstanciarMax;

    public Player player;
    public UILabel bestScore, score, info;
    public GameObject Logo;

    int pontos;
    bool mudarAltura;
    bool exiting;
    float ultimaAltura;

    public static int BestScore
    {
        get 
        {
            if(!PlayerPrefs.HasKey("bestScore"))
            {
                PlayerPrefs.SetInt("bestScore",0);
            }

            return PlayerPrefs.GetInt("bestScore");
        }
        set 
        {
            PlayerPrefs.SetInt("bestScore", value);
        }
    }
    float timer;

	void Start () {

        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        ScoreMode = false;
        TweenAlpha.Begin(Logo, 0.3f, 1);
        Playing = false;
        PosicaoDestroyObstaculo = posicaoFinalX;
        player.Score += new Player.Delegate(Score);
        player.Morreu += new Player.Delegate(GameOver);
        player.StartGame += new Player.Delegate(StartGame);
        BestScoreCheck();
        intervaloInstanciarMin = 2.8f;
        intervaloInstanciarMax = 3.2f;
        bestScore.alpha = 0;
        score.alpha = 0;
        info.alpha = 0;
        Go.to(info, 0.3f, new GoTweenConfig().floatProp("alpha", 1, false).setIterations(-1, GoLoopType.PingPong));

        
	}
    void Update()
    {
        if (Playing)
        {
            GameSpeed = Time.deltaTime * velocidade;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = Random.Range(intervaloInstanciarMin, intervaloInstanciarMax);
                GerarObstaculo();
            }
        }

        if (!player.vivo && !exiting)
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.red, Time.deltaTime * 3);
        }
        else
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.white, Time.deltaTime * 2);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Quit());
        }
       
        

    }
    void GerarObstaculo()
    {
        GameObject obs = (GameObject)Instantiate(pecasObstaculo, new Vector3(posicaoInicialX, 0), Quaternion.identity);
       

        if (pontos < 2)
        {
            intervaloInstanciarMin = 3f;
            intervaloInstanciarMax = 3.5f;
            obs.GetComponent<Obstaculo>().Move(Random.Range(-1.6f, 1.6f));
            obs.GetComponent<Obstaculo>().Distancia(Random.Range(3.84f, 3.89f));
        }
        else if (pontos >=2 && pontos <=7)
        {
            intervaloInstanciarMin = 2.8f;
            intervaloInstanciarMax = 3f;
            obs.GetComponent<Obstaculo>().Move(Random.Range(-1.8f, 1.8f));
            obs.GetComponent<Obstaculo>().Distancia(Random.Range(3.79f, 3.84f));
        }

        else if (pontos > 7 && pontos <=10)
        {
            intervaloInstanciarMin = 2.6f;
            intervaloInstanciarMax = 2.9f;
            obs.GetComponent<Obstaculo>().Move(Random.Range(-2.2f, 2.2f), 1f, 0.8f);
            obs.GetComponent<Obstaculo>().Distancia(Random.Range(3.74f, 3.79f));

        }

        else if (pontos > 10)
        {
            intervaloInstanciarMin = 2f;
            intervaloInstanciarMax = 2.4f;
            obs.GetComponent<Obstaculo>().Move(Random.Range(-2.4f, 2.4f), 1f, 0.92f);
            obs.GetComponent<Obstaculo>().Distancia(Random.Range(3.69f, 3.73f));

        }

    }
    void Score()
    {
        Instanciador.instancia.PlaySfx(1, 1, 1);
        Camera.main.backgroundColor = Color.green;
        pontos++;
        Pontos = pontos;
        score.text = "SCORE : " + pontos;
        BestScoreCheck();
    }
    void GameOver()
    {
        if (exiting)
            return;

        velocidade = 0;
        Playing = false;

       
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(posMortem());
            }
            else
            {
                StartCoroutine(loadCena());
            }
        
    }
    IEnumerator Quit()
    {
        Go.killAllTweensWithTarget(info);
        TweenAlpha.Begin(info.gameObject, 0.1f, 0);

        exiting = true;
        player.Morrer();
        velocidade = 0;
        Playing = false;
        yield return new WaitForSeconds(1);
        TweenAlpha.Begin(preMortemPanel.gameObject, 0.2f, 0);
        TweenAlpha.Begin(sairPanel.gameObject, 0.6f, 1);
        yield return new WaitForSeconds(1.2f);
        Wp8Unity.Interstitial();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    void StartGame()
    {
        Go.killAllTweensWithTarget(info);
        TweenAlpha.Begin(Logo, 0.3f, 0);
        TweenAlpha.Begin(bestScore.gameObject, 0.5f, 1);
        TweenAlpha.Begin(score.gameObject, 0.5f, 1);
        TweenAlpha.Begin(info.gameObject, 0.1f, 0);
        Go.to(teto, 0.5f, new GoTweenConfig().localPosition(new Vector3(0, 483, 0), false).setEaseType(GoEaseType.CubicOut));
        Go.to(logo, 0.2f, new GoTweenConfig().localPosition(new Vector3(0, 525, 0), false).setEaseType(GoEaseType.CubicOut));
        
    }
    void BestScoreCheck()
    {
       

        if (pontos > BestScore)
        {
            BestScore = pontos;
            Go.to(bestScore, 0.2f, new GoTweenConfig().colorProp("color", Color.red, false).setIterations(4, GoLoopType.PingPong));
        }

        bestScore.text = "BEST SCORE : " + BestScore;
    }

    IEnumerator loadCena()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(0);
    }
    IEnumerator posMortem()
    {
        yield return new WaitForSeconds(1.5f);
        TweenAlpha.Begin(preMortemPanel.gameObject, 0.5f, 0);
        yield return new WaitForSeconds(0.5f);
        TweenAlpha.Begin(posMortemPanel.gameObject, 0.2f, 1);
        scoreScript.StartScore();
        ScoreMode = true;
    }
    IEnumerator preMortem()
    {
        
        yield return new WaitForSeconds(0.1f);
        TweenAlpha.Begin(spriteCima.gameObject, 0.2f, 1);
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(0);
        
    }


    public void Play()
    {
        StartCoroutine(preMortem());
    }
 


    
}


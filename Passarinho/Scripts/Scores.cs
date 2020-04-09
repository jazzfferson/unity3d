using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using AssemblyCSharp;


public class Scores : MonoBehaviour {

    public Color[] rankColors;
    public UILabel[] scoresLabels;
    public UIPanel namePanel,fapPanel,errorPanel,scoreResultsPanel;
    public UIInput input;
    public Transform inputPivo;
    public string success;

    
    string MyUserName
    {
        get
        {
            if (!PlayerPrefs.HasKey("MyUserName"))
            {
                PlayerPrefs.SetString("MyUserName", "");
            }

            return PlayerPrefs.GetString("MyUserName");
        }
        set
        {
            PlayerPrefs.SetString("MyUserName", value);
        }
    }
    int myBestScore;
    bool checkinName;
    bool connectionSucces;

    //
    ScoreBoardResponse callBackScore = new ScoreBoardResponse();
    ScoreBoardResponse callBackUserName = new ScoreBoardResponse();
    ScoreBoardResponse callBackRankers = new ScoreBoardResponse();
    ServiceAPI sp = null;
    ScoreBoardService scoreBoardService = null; // Initializing ScoreBoard Service.
    Constant cons = new Constant();

#if UNITY_EDITOR
    public static bool Validator(object sender, 
        System.Security.Cryptography.X509Certificates.X509Certificate certificate, 
        System.Security.Cryptography.X509Certificates.X509Chain chain, 
        System.Net.Security.SslPolicyErrors sslPolicyErrors)
    { return true; }
#endif

    

	public void StartScore () {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Application.Quit();
        }

        myBestScore = GameFuu.BestScore;
        input.onChange.Add(new EventDelegate(this, "GetName"));
        connectionSucces = false;
        
        


#if UNITY_EDITOR
        ServicePointManager.ServerCertificateValidationCallback = Validator;
#endif
        sp = new ServiceAPI(cons.apiKey, cons.secretKey);

        callBackScore.OnSuccessEvent += new System.EventHandler(callBackScore_OnSuccessEvent);
        callBackUserName.OnSuccessEvent += new System.EventHandler(callBackUserName_OnSuccessEvent);
        callBackRankers.OnSuccessEvent+=new System.EventHandler(callBackRankers_OnSuccessEvent);

        
        

        for (int i = 0; i < scoresLabels.Length; i++)
        {
            scoresLabels[i].text = (i + 1) + "º - Username   Score: 0";
        }

        CheckUserName();
       
	}

    public void UpDownInput()
    {
        print("asidasd");
        Go.to(inputPivo, 0.2f, new GoTweenConfig().localPosition(new Vector3(0, 315, 0), false));
    }

    void CheckUserName()
    {
        if (string.IsNullOrEmpty(MyUserName))
        {
            TweenAlpha.Begin(namePanel.gameObject, 0.2f, 1);
        }
        else
        {
            Send();
        }
    }
    public void NameOk()
    {
        print("Clicked");
        
        if (!string.IsNullOrEmpty(MyUserName))
        {
            TweenAlpha.Begin(namePanel.gameObject, 0.2f, 0);
        }
        
        Send();
    }
    public void GetName()
    {
        print("GetedName");
        MyUserName = input.value;
    }
    void callBackScore_OnSuccessEvent(object sender, System.EventArgs e)
    {

        if (e != null)
        {
            connectionSucces = false;
            FailConnection();
            StopCoroutine("ConnectionTime");
            return;
        }

        scoreBoardService = sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.
        scoreBoardService.GetTopNRankers(cons.gameName, 13, callBackRankers);
    }
    void callBackRankers_OnSuccessEvent(object sender, System.EventArgs e)
    {
       
        if (e != null)
        {
            connectionSucces = false;
            FailConnection();
            StopCoroutine("ConnectionTime");
            return;
        }

        IList<Game.Score> list = sender as IList<Game.Score>;
        if (list.Count > 0)
        {
            for (int i = 0; i < scoresLabels.Length - 1; i++)
            {
                if (i > list.Count - 1)
                    break;
                scoresLabels[i].text = (i + 1) + "º - " + list[i].GetUserName() + "  Score: " + list[i].GetValue();
            }

        }

         scoreBoardService = sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.
         scoreBoardService.GetUserRanking(cons.gameName, cons.userName, callBackUserName);
    }
    void callBackUserName_OnSuccessEvent(object sender, System.EventArgs e)
    {
       

        if (e != null)
        {
            connectionSucces = false;
            FailConnection();
            StopCoroutine("ConnectionTime");
            return;
        }

        IList<Game.Score> list = sender as IList<Game.Score>;

        int rank;
        int.TryParse(list[0].rank, out rank);

        scoresLabels[12].text = list[0].rank + "º - " + list[0].userName + "  Score: " + list[0].value;

        if (rank <= 3)
        {
            scoresLabels[12].color = rankColors[0];
        }
        else if (rank > 3 && rank < 10)
        {
            scoresLabels[12].color = rankColors[1];
        }
        else if (rank > 9 && rank < 13)
        {
            scoresLabels[12].color = rankColors[2];

        }
        else if (rank > 13)
        {
            scoresLabels[12].color = rankColors[3];
        }

        SuccessConnection();
    }

    public void Send()
    {

        TweenAlpha.Begin(fapPanel.gameObject, 0.2f, 1);
        StartCoroutine(ConnectionTime());

        string name = MyUserName;
        int score = myBestScore;
        cons.userName = MyUserName;
       
        App42Log.SetDebug(true);
        scoreBoardService = sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.
        scoreBoardService.SaveUserScore(cons.gameName, cons.userName, score, callBackScore);
        

        scoresLabels[12].text = "?" + " - " + name + "  Score: " + score;
    }
    public void Curtir()
    {
        Wp8Unity.Curtir();
    }
    public void Compartilhar()
    {
        Wp8Unity.Compartilhar();
    }
    IEnumerator ConnectionTime()
    {
        yield return new WaitForSeconds(8);
        if (!connectionSucces)
        {
            FailConnection();
        }
    }
    void FailConnection()
    {
        callBackScore.OnSuccessEvent -= new System.EventHandler(callBackScore_OnSuccessEvent);
        callBackUserName.OnSuccessEvent -= new System.EventHandler(callBackUserName_OnSuccessEvent);
        callBackRankers.OnSuccessEvent -= new System.EventHandler(callBackRankers_OnSuccessEvent);

        TweenAlpha.Begin(fapPanel.gameObject, 0.2f, 0);
        TweenAlpha.Begin(errorPanel.gameObject, 0.2f, 1);
    }
    void SuccessConnection()
    {
        TweenAlpha.Begin(fapPanel.gameObject, 0.2f, 0);
        TweenAlpha.Begin(scoreResultsPanel.gameObject, 0.2f, 1);
        connectionSucces = true;
    }

   
}

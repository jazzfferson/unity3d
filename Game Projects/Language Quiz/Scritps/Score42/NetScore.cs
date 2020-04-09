using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using AssemblyCSharp;



public class NetScore : MonoBehaviour {

    public GameObject userRankingPrefb;
    List<GameObject> rankingListUsers;
    public float offset;
    public Transform position;

    public UIInput userNameInput,scoreInput;
    public UIPanel loadingPanel;
    private float alphaTarget;

    ScoreBoardResponse callBack = new ScoreBoardResponse();
    ServiceAPI sp = null;
    ScoreBoardService scoreBoardService = null; // Initializing ScoreBoard Service.
    Constant cons = new Constant();

    public string success;

#if UNITY_EDITOR
    public static bool Validator(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    { return true; }
#endif

	void Start () {


        rankingListUsers = new List<GameObject>();

#if UNITY_EDITOR
        ServicePointManager.ServerCertificateValidationCallback = Validator;
#endif
        sp = new ServiceAPI(cons.apiKey, cons.secretKey);
        callBack.OnSuccessEvent += new System.EventHandler(callBack_OnSuccessEvent);
	}

    void Update()
    {
        loadingPanel.alpha = Mathf.Lerp(loadingPanel.alpha, alphaTarget, Time.deltaTime * 8);
    }

    void callBack_OnSuccessEvent(object sender, System.EventArgs e)
    {
        alphaTarget = 0f;



        IList<Game.Score> list = sender as IList<Game.Score>;
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("UserName is  : " + list[i].GetUserName());
            Debug.Log("Value is  : " + list[i].GetValue());

            rankingListUsers.Add((GameObject) Instantiate(userRankingPrefb,position.position+new Vector3(0,i*offset,0),Quaternion.identity));
            rankingListUsers[i].GetComponent<UserRanking>().SetValues(list[i].GetUserName(), list[i].GetValue().ToString(), (i + 1).ToString());

            if (i > 7)
                return;
        }

    }
   
    public void Send()
    {

        

        if (string.IsNullOrEmpty(userNameInput.value) || string.IsNullOrEmpty(scoreInput.value))
            return;


        alphaTarget = 1f;

        string name = userNameInput.value;
        int score = int.Parse(scoreInput.value);
        print("Name: " + name + " Score: " + score);


        App42Log.SetDebug(true);
        cons.userName = name;
        scoreBoardService = sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.
        scoreBoardService.SaveUserScore(cons.gameName, cons.userName, score, callBack);

        DestruirListaRanking();
    }

    public void Refresh()
    {
        alphaTarget = 1f;

        scoreBoardService = sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.
        scoreBoardService.GetTopNRankers(cons.gameName, 10, callBack);

        DestruirListaRanking();
    }

    void DestruirListaRanking()
    {
        foreach (GameObject userRank in rankingListUsers)
        {
            Destroy(userRank);
        }

        rankingListUsers.Clear();
    }

   
}

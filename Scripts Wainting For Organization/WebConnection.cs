using UnityEngine;
using System.Collections;

public class WebConnection
{

    public delegate void WebConnectionDelegate(string data);
    public event WebConnectionDelegate OnFailConnection;
    public event WebConnectionDelegate OnSucessConnection;
   // public event WebConnectionDelegate OnTimeOut;


    /// <summary>
    /// A pré url é o começo da url que é comum entre todas as chamadas
    /// 
    /// </summary>
    private string _preUrl;

    private bool connectionSucess;

    public WebConnection(string preUrl)
    {
        _preUrl = preUrl;
    }

    public IEnumerator Connection(string postUrl)
    {
      //  connectionSucess = false;

        Debug.Log(_preUrl + postUrl);
        WWW webConnection = new WWW(_preUrl + postUrl);

        //StartCoroutine(TimeOut());

        yield return webConnection;

        if (!string.IsNullOrEmpty(webConnection.error))
        {
            if (OnFailConnection != null)
            {
                OnFailConnection(webConnection.error);
            }
        }
        else
        {
           // connectionSucess = true;

            if (OnSucessConnection != null)
            {
                OnSucessConnection(webConnection.text);
            }
        }
    }
}

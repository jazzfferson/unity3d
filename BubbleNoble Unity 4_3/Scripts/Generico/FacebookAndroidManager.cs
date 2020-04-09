/*using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class FacebookAndroidManager : MonoBehaviour {

	// Variavel que indica se o plugin do facebook foi inicializado ou nao.
	private bool isInit = false;
	public UILabel label;
	// Use this for initialization
	void Start () {
		
		FB.Init(OnInitComplete,OnHideUnity);

	
	
	}


	#region Inicializacao


	//Inicializa o plugin do facebook. O primeiro parametro
	//eh o nome do metodo que ele ira chamar quando estiver inicializado
	// e o segundo parametro e o metodo que ele ira chamar quando a unity
	// estiver minimizada/segundo plano.

	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}

	private void OnInitComplete()
	{
		label.text = "FB.Init completed: Is user logged in? " + FB.IsLoggedIn;
		isInit = true;
		Instanciador.instancia.PlaySfx(1,1,1);

		if(!FB.IsLoggedIn)
		{
			FB.Login("email,publish_actions,publish_actions", Callback);
		}


	}
	
	private void OnHideUnity(bool isGameShown)
	{

		Debug.Log("Is game showing? " + isGameShown);
	}
	
	#endregion
	
	#region Metodos de execucao

	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", Callback);
	}

	void Callback(FBResult result)
	{
		label.text = result.Error;
	}
	
	#endregion
	

	 #region FB.PublishInstall() example
	
	private void CallFBPublishInstall()
	{
		FB.PublishInstall(PublishComplete);
	}
	
	private void PublishComplete(FBResult result)
	{
		Debug.Log("publish response: " + result.Text);
	}
	

	
	public string FeedToId = "";
	public string FeedLink = "http://windowsphone.com/";
	public string FeedLinkName = "no name";
	public string FeedLinkCaption = "Soy foda";
	public string FeedLinkDescription = "Sem descricao";
	public string FeedPicture = "http://cdn.marketplaceimages.windowsphone.com/v8/images/4cd56bf6-e365-471c-bd47-8ee62ea796f8?imageType=ws_icon_large";
	public string FeedMediaSource = "";
	public string FeedActionName = "no name";
	public string FeedActionLink = "www.google.com";
	public string FeedReference = "";
	public bool IncludeFeedProperties = false;
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();
	
	private void CallFBFeed()
	{
		Dictionary<string, string[]> feedProperties = null;
		if (IncludeFeedProperties)
		{
			feedProperties = FeedProperties;
		}
		FB.Feed(
			toId: FeedToId,
			link: FeedLink,
			linkName: FeedLinkName,
			linkCaption: FeedLinkCaption,
			linkDescription: FeedLinkDescription,
			picture: FeedPicture,
			mediaSource: FeedMediaSource,
			actionName: FeedActionName,
			actionLink: FeedActionLink,
			reference: FeedReference,
			properties: feedProperties
			);
	}


	// Update is called once per frame
	void Update () {

	
	
	}
}
*/

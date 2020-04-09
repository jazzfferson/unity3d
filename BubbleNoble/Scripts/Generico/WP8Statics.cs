using System;
public static class WP8Statics 

{

     public static bool hasInternetConection = false;	
     //advertising da tela de resultados do gameplay
     public static event EventHandler OnEnableAd1Handle;    
     public static void EnableAd1()

     {

          if (OnEnableAd1Handle != null && hasInternetConection)
          {

               OnEnableAd1Handle(null, null);            

          }

     }
	
     public static event EventHandler OnDisableAd1Handle;    
     public static void DisableAd1()

     {

          if (OnDisableAd1Handle != null && hasInternetConection)

          {

               OnDisableAd1Handle(null, null);            

          }

     }
	
     //advertising da tela play do menu
     public static event EventHandler OnEnableAd2Handle;    
     public static void EnableAd2()

     {

          if (OnEnableAd2Handle != null && hasInternetConection)

          {

               OnEnableAd2Handle(null, null);            

          }

     }
	
     public static event EventHandler OnDisableAd2Handle;    
     public static void DisableAd2()

     {

          if (OnDisableAd2Handle != null && hasInternetConection)

          {

               OnDisableAd2Handle(null, null);            

          }

     }

     //Review do jogo no marketPlace

	public static event EventHandler OnReviewMarketPlace;    
	public static void ReviewMarketPlace()
		
	{
		
		if (OnReviewMarketPlace != null)
			
		{
			
			OnReviewMarketPlace(null, null);            
			
		}
		
	}
	// Ir para a pagina do facebook
	public static event EventHandler OnFacebookLike;    
	public static void FacebookLike()
		
	{
		
		if (OnFacebookLike != null)
			
		{
			
			OnFacebookLike(null, null);            
			
		}
		
	}




}

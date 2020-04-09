using UnityEngine;
using System.Collections;

public class InfoFases {

    public static int FasesHabilitadas = 1;
    public static int FaseAtual = 0;
	
    public static Fase[] FaseJogo;

    public class Fase
    {
        public string name;
        public string landTargetName;
    }

    public static void Initialize()
    { 
		
        FaseJogo = new Fase[6];

        for (int i = 0; i < FaseJogo.GetLength(0); i++)
        {
            FaseJogo[i] = new Fase();
            FaseJogo[i].name = "Fase" + i + 1;
        }
		#region Fases
          	#region Fase1
            FaseJogo[0].landTargetName = "Lua";
        	#endregion
		
			#region Fase2
            FaseJogo[1].landTargetName = "Terra";
        	#endregion
		
			#region Fase3
            FaseJogo[2].landTargetName = "Lua";
        	#endregion
		
			#region Fase4
            FaseJogo[3].landTargetName = "Terra";
        	#endregion
		
			#region Fase5
            FaseJogo[4].landTargetName = "Lua";
        	#endregion
		
			#region Fase6
            FaseJogo[5].landTargetName = "Terra";
        	#endregion
		
		#endregion

		PlayerPrefs.SetInt("FasesHabilitadas",6);
		PlayerPrefs.SetInt("FaseAtual",1);
    }
	
	public static void Load()
	{
		FasesHabilitadas = PlayerPrefs.GetInt("FasesHabilitadas");
		FaseAtual = PlayerPrefs.GetInt("FaseAtual");

	}
	
	public static void Save()
	{
		PlayerPrefs.SetInt("FasesHabilitadas",FasesHabilitadas);
		PlayerPrefs.SetInt("FaseAtual",FaseAtual);
		PlayerPrefs.Save();
	}
	

	
}

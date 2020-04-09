using UnityEngine;
using System.Collections;

public class GameData
{
    public static float numVidas;
    public static bool firstRun;
    public static float tempoFicouVivo;
    public static int faseAtual;
    public static float ultimaPontucao;

    #region Recordes

    public static bool NovoRecorde(float pontuacao)
    {
        if (PlayerPrefs.HasKey("save"))
        {
            if (PlayerPrefs.GetFloat("save") < pontuacao)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public static bool NovoRecorde(float pontuacao, bool jaSalvar)
    {
        if (PlayerPrefs.HasKey("save"))
        {
            if (PlayerPrefs.GetFloat("save") < pontuacao)
            {
                if (jaSalvar)
                {
                    SalvarRecorde(pontuacao);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (jaSalvar)
            {
                SalvarRecorde(pontuacao);
            }
            return true;
        }
    }

    public static void SalvarRecorde(float pontuacao)
    {
        PlayerPrefs.SetFloat("save", pontuacao);
    }

    public static float GetRecorde()
    {
        return PlayerPrefs.GetFloat("save");
    }

    #endregion
       

    public static bool FirstRun()
    {
        if (!PlayerPrefs.HasKey("firstRun"))
        {
            PlayerPrefs.SetInt("firstRun", 0);
        }

        if (PlayerPrefs.GetInt("firstRun") == 0)
        {
            PlayerPrefs.SetInt("firstRun", 1);
            return true;
        }
        else
        {
            return false;
        }
    }

}

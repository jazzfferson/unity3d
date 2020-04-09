
using UnityEngine;
using System.Collections;

public enum EMenu { MenuPlay, MenuSelectLanguage, MenuSelectMode, MenuInfo, 
            Ingame,EndlessStatistic,PraticeStatistc ,Pause, Quit, Quiting };

public class Menus : MonoBehaviour {

    public static Menus instance;

    public GameObject MenuPlay, MenuSelectLanguage, MenuSelectMode, MenuInfo, 
            Ingame, EndlessStatistic, PraticeStatistc, Pause, Quit, Quiting;

    public GameObject Common_BackGround, Common_Buttons, Common_HighScore, Common_Score;
    public EMenu menuAtual;
    EMenu menuAnterior;

  //  public GerenciadorMusica gerenciadorMusica;


    IEnumerator Start () {

        if (instance == null) { instance=this; }     
     //   Scores.instance.SetHighScore(Proprietes.HighScore);
     //   HighScoreAnimation.instance.animateScore = true;
        yield return new WaitForSeconds(0.5f);
        ChangeMenu(menuAtual);

	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote))
        {
            Sfx_Select1();
            BackMenu(menuAtual);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            print("Data deleted!");
            Scores.instance.SetHighScore(Proprietes.HighScore);
        }

	}

    bool playClicked = false;
    void Play()
    {
        if (playClicked)
            return;
        StartCoroutine(PlayClicked());
    }
    IEnumerator PlayClicked()
    {
        Sfx_Select1();
        playClicked = true;
        ButtonStartTextRetro.instance.Speed = 15;
        yield return new WaitForSeconds(1);
        ButtonStartTextRetro.instance.Speed = 0;
        ChangeMenu(EMenu.MenuSelectLanguage);
    }

    #region Gerenciadores de Tela

    public void ChangeMenu(EMenu menu)
    {
        float timeTransition = 0.5f;
        FadeScreen.instance.Fade(timeTransition / 2);
        StartCoroutine(CM(menu, timeTransition / 2));
    }

    void ChangeCommonsElements(EMenu menu)
    {
        switch (menu)
        {
            case EMenu.MenuPlay:
                Common_BackGround.SetActive(true);
                Common_Buttons.SetActive(true);
                Common_HighScore.SetActive(true);
                Common_Score.SetActive(false);
               // HighScoreAnimation.instance.animateScore = true;
                ButtonStartTextRetro.instance.Speed = 4.75f;
               
                break;

            case EMenu.MenuSelectLanguage:
                Common_BackGround.SetActive(true);
                Common_Buttons.SetActive(true);
                Common_HighScore.SetActive(true);
                Common_Score.SetActive(false);
            //    HighScoreAnimation.instance.animateScore = true;
               
                break;

            case EMenu.MenuSelectMode:
                Common_BackGround.SetActive(true);
                Common_Buttons.SetActive(true);
                Common_HighScore.SetActive(true);
                Common_Score.SetActive(false);
           //     HighScoreAnimation.instance.animateScore = true;
               
                break;

            case EMenu.MenuInfo:
                Common_BackGround.SetActive(true);
                Common_Buttons.SetActive(true);
                Common_HighScore.SetActive(false);
                Common_Score.SetActive(false);
           //     HighScoreAnimation.instance.animateScore = false;
               
                break;

            case EMenu.Ingame:
                Common_BackGround.SetActive(false);
                Common_Buttons.SetActive(false);
                Common_HighScore.SetActive(true);
           //     HighScoreAnimation.instance.animateScore = false;

                if(Proprietes.Mode == Mode.Endless)
                  Common_Score.SetActive(true);
                else
                  Common_Score.SetActive(false);
                break;

            case EMenu.EndlessStatistic:

                break;

            case EMenu.PraticeStatistc:
                Common_Buttons.SetActive(true);
                Common_HighScore.SetActive(true);
                break;

            case EMenu.Pause:
                Common_BackGround.SetActive(false);
                Common_Buttons.SetActive(true);
                Common_HighScore.SetActive(true);
            //    HighScoreAnimation.instance.animateScore = false;
                
                break;

            case EMenu.Quit:

                Common_BackGround.SetActive(false);
                Common_Buttons.SetActive(false);
                Common_HighScore.SetActive(false);
            //    HighScoreAnimation.instance.animateScore = false;
               

                break;
            case EMenu.Quiting:
                break;
        }
    }

    IEnumerator CM(EMenu menu, float delay)
    {
        yield return new WaitForSeconds(delay);

        menuAtual = menu;
        MenuPlay.SetActive(false);
        MenuSelectLanguage.SetActive(false);
        MenuSelectMode.SetActive(false);
        Ingame.SetActive(false);
        Pause.SetActive(false);
        Quit.SetActive(false);
        Quiting.SetActive(false);
        MenuInfo.SetActive(false);
        EndlessStatistic.SetActive(false);
        PraticeStatistc.SetActive(false);


        switch (menu)
        {
            case EMenu.MenuPlay:
                MenuPlay.SetActive(true);
                
                break;

            case EMenu.MenuSelectLanguage:
                MenuSelectLanguage.SetActive(true);
                break;

            case EMenu.MenuSelectMode:
                MenuSelectMode.SetActive(true);
                break;

            case EMenu.MenuInfo:
                MenuInfo.SetActive(true);
                break;

            case EMenu.Ingame:
                Ingame.SetActive(true);
                break;

            case EMenu.EndlessStatistic:
                EndlessStatistic.SetActive(true);
                StatisticsEndLess.instance.ShowResults();
                break;

            case EMenu.PraticeStatistc:
                PraticeStatistc.SetActive(true);
                break;

            case EMenu.Pause:
                Pause.SetActive(true);
                break;

            case EMenu.Quit:
                Quit.SetActive(true);
                break;

            case EMenu.Quiting:
                Quiting.SetActive(true);
                break;
        }

        ChangeCommonsElements(menu);
       // gerenciadorMusica.ChangeMusic(menu);
    }

    public void BackMenu(EMenu MenuAtual)
    {

        switch (MenuAtual)
        {
            case EMenu.MenuPlay:
                ChangeMenu(EMenu.Quit);
                break;

            case EMenu.MenuSelectLanguage:
                ChangeMenu(EMenu.MenuPlay);
                break;

            case EMenu.MenuSelectMode:
                ChangeMenu(EMenu.MenuSelectLanguage);
                break;

            case EMenu.MenuInfo:
                ChangeMenu(EMenu.MenuPlay);
                break;

            case EMenu.Ingame:
                ChangeMenu(EMenu.Pause);
                break;

            case EMenu.EndlessStatistic:
                ChangeMenu(EMenu.MenuPlay);
                break;

            case EMenu.PraticeStatistc:
                ChangeMenu(EMenu.MenuPlay);
                break;

            case EMenu.Pause:
                ChangeMenu(EMenu.Ingame);
                break;

            case EMenu.Quit:
                ChangeMenu(EMenu.MenuPlay);
                break;
        }
    }

    #endregion

    #region Botoes de escolha de linguagem do jogo

    void LanguageEnglish()
    {
        SelectedLanguage(Language.English);
    }
    void LanguagePortugues()
    {
        SelectedLanguage(Language.Portugues);
    }
    void LanguageEspanhol()
    {
        SelectedLanguage(Language.Espanhol);

    }
    void LanguageNihongo()
    {
        SelectedLanguage(Language.Nihongo);

    }
    void LanguageFrancais()
    {
        SelectedLanguage(Language.Francais);
    }

    void SelectedLanguage(Language selectedLanguage)
    {
        Sfx_Select1();
        print("Lingua Selecionada: " + selectedLanguage.ToString());

        Proprietes.Language = selectedLanguage;
        ChangeMenu(EMenu.MenuSelectMode);
    }

    #endregion

    #region Botoes de escolha do modo de jogo

    void ModeLearn()
    {
        SelectedMode(Mode.Pratice);
    }

    void ModeEndless()
    {
        SelectedMode(Mode.Endless);
    }

    void SelectedMode(Mode selectedMode)
    {
        Sfx_Select2();
        print("Modo Selecionado: " + selectedMode.ToString());

        Proprietes.Mode = selectedMode;
        ChangeMenu(EMenu.Ingame);
        GameQuiz.instance.StartGame();
    }

    #endregion

    #region Botoes da tela pause

    void PauseBackMenu()
    {
        MainMenu();
    }

    void PauseResumeGame()
    {
        Sfx_Select1();
        ChangeMenu(EMenu.Ingame);
    }
    #endregion

    #region Botoes da tela Endless Statistc

    void EndlessMainMenu()
    {
        MainMenu();
    }

    void EndLessPlayAgain()
    {
        PlayAgain();
    }
    
    #endregion

    #region Botoes da tela Pratice Statistc

    void PraticeMainMenu()
    {
        MainMenu();
    }
    void PraticeAgain()
    {
        PlayAgain();
    }

    #endregion

    #region Botoes Commons

    void Info()
    {
        Sfx_Select1();
        ChangeMenu(EMenu.MenuInfo);
    }
    void Facebook()
    {
 
    }
    void Review()
    {
 
    }
    void MainMenu()
    {
        Sfx_Select1();
        ChangeMenu(EMenu.MenuPlay);
    }
    void PlayAgain()
    {
        ChangeMenu(EMenu.Ingame);
        GameQuiz.instance.StartGame();
    }
    #endregion

    #region Quit

    void QuitYes()
    {
        ChangeMenu(EMenu.Quiting);
        Invoke("FinishApp", 1);
        Sfx_Select1();
      //  gerenciadorMusica.Music_FadeOut(0.8f);
    }
    void QuitNo()
    {
        BackMenu(menuAtual);
        Sfx_Select2();
    }
    void FinishApp()
    {
        Application.Quit();
    }

    #endregion

    #region Sounds

    void Sfx_Select1()
    {
        Instanciador.instancia.PlaySfx(3, 1, 1);
    }

    void Sfx_Select2()
    {
        Instanciador.instancia.PlaySfx(3, 1, 1);
    }


    #endregion



   


}

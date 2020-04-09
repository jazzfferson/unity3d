using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField]
    private float totalRaceTimeInSeconds = 60;

    [SerializeField]
    private int totalLaps = 2; 

    [SerializeField]
    private TrackSection trackSectionManager;

    [SerializeField]
    private CarHudInfo gameHud;

    [SerializeField]
    private ResultsPanel resultsPanel;

    [SerializeField]
    private CanvasGroup ingameCanvasGroup;

    [SerializeField]
    private PlayerController playerController;



    private bool gameStarted = false;
    private bool ended = false;
    private bool isValidPlayerTime = true;
    private float actualLapTime, totalTime;
    private int playerLap = 0;
    private int lastSectionId = -1;

	void Awake ()
    {
       
        gameHud.SetStaticValues(totalRaceTimeInSeconds, totalLaps, 1);
        gameHud.UpdateGameHUD(totalTime,actualLapTime);
        ingameCanvasGroup.alpha = 0;
        trackSectionManager.OnStartFinishPass += TrackSectionManager_OnStartFinishPass;

    }

    private void TrackSectionManager_OnStartFinishPass(object sender, System.EventArgs e)
    {
        if(!gameStarted) // A corrida inicia aqui
        {
            playerLap ++;
            gameStarted = true;
            Debug.Log("Game has started !");
            //Go.to(ingameCanvasGroup, 0.15f, new GoTweenConfig().floatProp("alpha", 1, false).setEaseType(GoEaseType.SineOut));
        }
        else
        {
            playerLap++;

            if (playerLap > totalLaps)
            {
                Debug.Log("End race !");

                ended = true;
                //Acabou todas as voltas
                //   playerLap = totalLaps;
                playerController.playerEnable = false;
                playerController.throttleDelegate(0);
                playerController.brakeDelegate(0.85f);
                playerController.steeringDelegate(0);
                //Go.to(ingameCanvasGroup, 0.15f, new GoTweenConfig().floatProp("alpha", 0, false).setEaseType(GoEaseType.SineOut).setDelay(1));
                resultsPanel.ShowResults(totalRaceTimeInSeconds, totalTime);
                return;
            }
            else
            {
                //Tem mais volta para dar
                actualLapTime = 0; // Zera o timer da volta atual
                Debug.Log("New lap !");
            }
        }

        gameHud.SetStaticValues(totalRaceTimeInSeconds, totalLaps, playerLap);
    }

    void Update ()
    {
        if(gameStarted && !ended)
        {
            UpdatePlayerTimer();
        }
	}

    private void UpdatePlayerTimer()
    {
        float time = Time.deltaTime;
        totalTime += time;
        actualLapTime += time;
        gameHud.UpdateGameHUD(totalTime,actualLapTime);

        if(totalTime > totalRaceTimeInSeconds && isValidPlayerTime)
        {
            isValidPlayerTime = false;
            gameHud.SetPlayerTimeColor(Color.red);
        }
    }
}

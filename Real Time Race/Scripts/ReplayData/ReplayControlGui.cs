using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReplayControlGui : MonoBehaviour
{
    public static ReplayControlGui instance;
    public static bool Replaying;
    public Text messageLabel;
    public Transform replayPanel, gamePanel;
    public Camera replayCamera;
    public CarCamera carCamera;
    CarCamera scriptCarCamera;
    CarReplay carReplayManager;

    float speed = 1.0f;
    bool paused = false;
    int actualReplayCameraView=0;

    void Awake()
    {
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
        }

        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);

       
    }
    void Start()
    {
        scriptCarCamera = carCamera.GetComponent<CarCamera>();

        if (Replaying)
        {
            replayPanel.gameObject.SetActive(true);
            gamePanel.gameObject.SetActive(false);
            SetCameraActive(false);
        }
        else
        {
            replayPanel.gameObject.SetActive(false);
            gamePanel.gameObject.SetActive(true);
            SetCameraActive(true);
        }
    }
    void Instance_OnReferencesReady()
    {
        carReplayManager = StaticReferences.Instance.car.GetComponent<CarReplay>();
    }
    public void PlayReplay()
    {
        carReplayManager.PlayReplay();
    }
    public void Restart()
    {
        carReplayManager.RestartRace();
    }
    public void ChangeView()
    {
        switch (actualReplayCameraView)
        {
            case 0:
                ShowControlUtilized("Car View");
                break;
            case 1:
                ShowControlUtilized("Free View");
                break;
            case 2:
                ShowControlUtilized("Roof View");
                break;
            case 3:
                ShowControlUtilized("Replay View");
                break;
        }


        if (actualReplayCameraView == 0)
        {
            SetCameraActive(true);
        }
        else
        {
            scriptCarCamera.ChangeCamera();
        }

        if (actualReplayCameraView + 1 > scriptCarCamera.Views)
        {
            actualReplayCameraView = 0;
            SetCameraActive(false);
        }
        else
        {
            actualReplayCameraView++;
        } 
       
    }
    public void PlayPause()
    {
        if (!paused)
        {
            speed = Time.timeScale;
            Time.timeScale = 0;
            paused = true;
            ShowControlUtilized("Paused");
            SoundController.instanceSoundController.MuteSounds(true);
        }
        else
        {
            Time.timeScale = speed;
            paused = false;
            ShowControlUtilized("Resumed");
            SoundController.instanceSoundController.MuteSounds(false);
        }
    }
    public void IncreaseSpeed()
    {
        Speed(0.1f);
    }
    public void DecreaseSpeed()
    {
        Speed(-0.1f);
    }
    void Speed(float valor)
    {
        if (paused)
            return;

        speed = Mathf.Clamp(speed + valor, 0.4f, 2f);

        if (speed == 1)
        {
            ShowControlUtilized("Replay Speed : Normal");
        }
        else
        {
            ShowControlUtilized("Replay Speed : " + speed);
        }

        Time.timeScale = speed;
    }
    public void Exit()
    {
        print("Nada implementado");
    }
    public void Save()
    {
        print("Nada implementado");
    }
    void ShowControlUtilized(string message)
    {
        messageLabel.canvasRenderer.SetAlpha(1);
        messageLabel.text = message;
       // TweenAlpha.Begin(messageLabel.gameObject, 1f, 0f).delay = 1f;
    }
    void SetCameraActive(bool carCameraActive)
    {
        if (carCameraActive)
        {
            replayCamera.gameObject.SetActive(false);
            carCamera.enabled = true;
            carCamera.gameObject.GetComponent<AudioListener>().enabled = true;
            scriptCarCamera.IsBeingUsing = true;
            
        }
        else
        {
            replayCamera.gameObject.SetActive(true);
            carCamera.enabled = false;
            carCamera.gameObject.GetComponent<AudioListener>().enabled = false;
            scriptCarCamera.IsBeingUsing = false;
        }
    }

}


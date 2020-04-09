using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(RacePrepare))]
public class StaticReferences : MonoBehaviour {

    public bool ready = false;

    public static StaticReferences Instance;
   
    public GameObject car,pista;

    public Camera MainCamera,ReplayCamera;

    [HideInInspector]
    public RacePrepare racePrepare;
    [HideInInspector]
    public DayTimeManager dayTimeManager;
    [HideInInspector]
    public GameTimers gameTimers;

    public delegate void VisualLevelDelegate(GraphicsLevel raceTrack, GraphicsLevel carShadow);
    public delegate void SimpleDelegate();

    public event SimpleDelegate OnReferencesReady,OnNightAndDayTransition,OnReplayCameraChange;
    public event VisualLevelDelegate OnVisualLevelSelected;

    private GraphicsLevel carShadowGraficsLevel = GraphicsLevel.VeryLow;
    public GraphicsLevel ECarShadowGraficsLevel
    {
        get { return carShadowGraficsLevel; }
        set { carShadowGraficsLevel = value; }
    }

    private GraphicsLevel raceTrackGraficsLevel = GraphicsLevel.VeryLow;
    public GraphicsLevel ERaceTrackGraficsLevel
    {
        get { return raceTrackGraficsLevel; }
        set { raceTrackGraficsLevel = value; }
    }

    public ParticleSystem trackSmoke, offroadSmoke;

    public Light mainLight;

	void Awake () {


        racePrepare = GetComponent<RacePrepare>();
        dayTimeManager = GetComponent<DayTimeManager>();
        gameTimers = GetComponent<GameTimers>();

        if (Instance == null)
        {
            Instance = this;
        }

        ready = false;

       racePrepare.OnLoaded += new System.EventHandler(racePrepare_OnLoaded);

	}
    void racePrepare_OnLoaded(object sender, System.EventArgs e)
    {
        print("RacePrepare_OnLoaded() StaticReferences");

        ready = true;

        car = racePrepare.carroObj;
       
        car.GetComponent<PlaceCar>().Place(pista.GetComponent<TrackReferences>().carStartPosition);


        if (OnReferencesReady != null)
        {
            OnReferencesReady();
        }

        ECarShadowGraficsLevel = GraphicsLevel.Low;
        SetVisualLevel(GraphicsLevel.High, ECarShadowGraficsLevel);
    }

    public void SetVisualLevel(GraphicsLevel raceTrack, GraphicsLevel carShadow)
    {  
        if (OnVisualLevelSelected != null)
            OnVisualLevelSelected(raceTrack, carShadow);       
    }
    public void NightAndDayTransitionEvent()
    {
        if (OnNightAndDayTransition != null)
        {
            OnNightAndDayTransition();
        }
    }
    public void ReplayCameraChangeEvent()
    {
        if (OnReplayCameraChange != null)
        {
            OnReplayCameraChange();
        }
    }
    
	

}

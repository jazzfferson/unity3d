using UnityEngine;
using System.Collections;

public class DayTimeManager : MonoBehaviour {

    public Gradient SkyTop,SkyMid,SkyEquator,SunTint;
    public Material skyMapMaterial;
    public static DayTimeManager Instance;
    public Light sunlight;
    public Gradient sunLightColor, ambientLightColor;
    public AnimationCurve colorsAlongDay;
    public AnimationCurve dinamicCarShadowIntensityAlongDay;
    public AnimationCurve staticCarShadowIntensityAlongDay;
    public AnimationCurve sunLightIntensity;
    public float dayTime;
    public float time
    {
        get;
        set;
    }
    public float sunAngle;
    VisualCarManager visualCarManager;
    float finalTime;
    [HideInInspector]
    public bool isNight;
    bool lastValue=true;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);

      //  Go.to(this,60, new GoTweenConfig().floatProp("time", 9, false).setIterations(-1, GoLoopType.PingPong));
    }

    void Instance_OnReferencesReady()
    {
        visualCarManager = StaticReferences.Instance.car.GetComponent<VisualCarManager>();
        time = dayTime;
        UpdateParams();
    }

   
    void LateUpdate()
    {       
       // UpdateParams();     
    }
    void UpdateParams()
    {
        if (!StaticReferences.Instance.ready)
            return;
      //  time = dayTime;
        finalTime = time * 15;

        RenderSettings.ambientLight = ambientLightColor.Evaluate(colorsAlongDay.Evaluate(time));
        sunlight.color = sunLightColor.Evaluate(colorsAlongDay.Evaluate(time));
        sunlight.intensity = sunLightIntensity.Evaluate(time);
       
        
        sunlight.transform.rotation = Quaternion.Euler((finalTime) + 90, sunAngle, 0);
        visualCarManager.SetStaticShadowIntensity(staticCarShadowIntensityAlongDay.Evaluate(time));
        visualCarManager.SetDinamicShadowIntensity(dinamicCarShadowIntensityAlongDay.Evaluate(time));
            

        if (skyMapMaterial)
        {
            float amounth = colorsAlongDay.Evaluate(time);
            skyMapMaterial.SetColor("_SkyTopColor", SkyTop.Evaluate(amounth));
            skyMapMaterial.SetColor("_SkyMidColor", SkyMid.Evaluate(amounth));
            skyMapMaterial.SetColor("_SkyEquatorColor", SkyEquator.Evaluate(amounth));
            skyMapMaterial.SetColor("_SunTint", SunTint.Evaluate(amounth));
        }



        if (time > 6f && time < 16.5f)
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }

        if (isNight != lastValue)
        {
            StaticReferences.Instance.NightAndDayTransitionEvent();
            lastValue = isNight;

        }
    }
	
}

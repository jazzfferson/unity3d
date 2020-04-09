using UnityEngine;
using System.Collections;

public class VisualCarManager : VisualManager
{

    public MaterialLevelSetup[] arrayMaterialsSetup;
    public Projector sombraEstatica;
    public Material sombraMaterialEstatico;
    //public CustomShadow sombraRealTime;
    public Light farol;
    public int farolTraseiroMaterialId, farolDianteiroMaterialId;
    public MeshRenderer carRenderer;

    private CarController carController;
    private float brakeLightIntensityActual, brakeLightIntensityTarget, brakeLightIntensityMin, brakeNightIntensity;
    private Material[] meshRendererMaterials;


    private float farolIntensity;
    private GoTween farolTween;


    void Start()
    {
        StaticReferences.Instance.OnVisualLevelSelected += new StaticReferences.VisualLevelDelegate(Instance_OnVisualLevelSelected);
        StaticReferences.Instance.OnNightAndDayTransition += new StaticReferences.SimpleDelegate(Instance_OnNightAndDayTransition);
        carController = transform.GetComponent<MobileCarController>();
        if (farol != null)
        farolIntensity = farol.intensity;
        farolTween = null;
        EnableDisableFarolDianteiro(false);
        if (carRenderer != null)
        meshRendererMaterials = carRenderer.materials;
        brakeNightIntensity = 0.35f;
        brakeLightIntensityMin = 0;
        EnableDisableFarolDianteiro(false);
    }

    void Instance_OnNightAndDayTransition()
    {
        print("Transition");
        if (farol != null)
        EnableDisableFarolDianteiro(StaticReferences.Instance.dayTimeManager.isNight);
    }

    GraphicsLevel lastshadowLevelTrackSelected = GraphicsLevel.None;

    void ChangeShadowsLevel(GraphicsLevel shadowLevel)
    {

        if (shadowLevel != lastshadowLevelTrackSelected)
        {
            switch (shadowLevel)
            {
                case GraphicsLevel.High:
                    EnableStaticShadow(true);
                    EnableDinamicShadow(true);
                 //   sombraRealTime.UpdateShadowConfig(1, 1);
                    break;

                case GraphicsLevel.Medium:
                    EnableStaticShadow(true);
                    EnableDinamicShadow(true);
                 //   sombraRealTime.UpdateShadowConfig(2, 2);
                    break;

                case GraphicsLevel.Low:
                    EnableStaticShadow(true);
                    EnableDinamicShadow(false);
                    break;

                case GraphicsLevel.VeryLow:
                    EnableStaticShadow(false);
                    EnableDinamicShadow(false);
                    break;
            }

            lastshadowLevelTrackSelected = shadowLevel;
        }
    }
    void Instance_OnVisualLevelSelected(GraphicsLevel raceTrack, GraphicsLevel shadowLevel)
    {
        if(carRenderer!=null)
      //  ChangeAllMaterialsLevel(arrayMaterialsSetup, IsHighLevelMaterial(level));
        ChangeShadowsLevel(shadowLevel);

    }
    public void EnableStaticShadow(bool enable)
    {
        sombraEstatica.enabled = enable;
    }
    public void EnableDinamicShadow(bool enable)
    {
       // sombraRealTime.EnableShadow(enable);
    }
    public void EnableDisableFarolDianteiro(bool enable)
    {

        if (farol == null)
            return;

            float valorFinal;
            if (enable)
            {
                valorFinal = farolIntensity;
                farol.enabled = true;
                ChangeShadowsLevel(GraphicsLevel.VeryLow);

            }
            else
            {
                valorFinal = 0;
                ChangeShadowsLevel(StaticReferences.Instance.ECarShadowGraficsLevel);
            }

            farolTween = Go.to(farol, 0.5f, new GoTweenConfig().floatProp("intensity", valorFinal, false).setEaseType(GoEaseType.BackInOut)
                .onComplete(Completed =>
                {
                    farolTween = null;

                    if (!enabled)
                    {
                        farol.enabled = false;
                    }
                })
                .onUpdate(Update =>
                {
                    float multiplier = farol.intensity / farolIntensity;
                    brakeLightIntensityMin = brakeNightIntensity * multiplier;
                    carRenderer.materials[farolDianteiroMaterialId].SetFloat("_Intensity", 2.2f * multiplier);
                }));
        
    }
    void FixedUpdate()
    {
        BrakeLight();
    }
    void BrakeLight()
    {
        if (carRenderer == null)
            return;

        if (carController.brakeKey)
        {
            brakeLightIntensityTarget = 0.75f;
        }
        else
        {
            brakeLightIntensityTarget = brakeLightIntensityMin;
        }

            carRenderer.materials[farolTraseiroMaterialId].SetFloat("_Intensity", brakeLightIntensityTarget);
    }
    public void SetStaticShadowIntensity(float intensity)
    {
        sombraMaterialEstatico.SetFloat("_Bright", intensity);
    }
    public void SetDinamicShadowIntensity(float intensity)
    {
      //  sombraRealTime.Lightness = intensity;
    }
}

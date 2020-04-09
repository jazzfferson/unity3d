using UnityEngine;
using System.Collections;

public class VisualTrackManager : VisualManager
{
    public AnimationCurve speedAnimation;
    public int intervaloUpdate = 15;
   /* public Renderer pistaR, zebrasR, terrenoR;*/
    public MaterialLevelSetup[] arrayMaterialsSetup;
    public StaticPropsLevelSetup staticPropsLevelSetup;

    private Drivetrain driveTrainCar;

    void Start()
    {
        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);
        StaticReferences.Instance.OnVisualLevelSelected += new StaticReferences.VisualLevelDelegate(instance_OnVisualLevelSelected);
    }

    //Evento disparado quando o usuário muda o nível de visual do jogo
    void instance_OnVisualLevelSelected(GraphicsLevel raceTrack,GraphicsLevel shadowLevel)
    {
        ChangeAllMaterialsLevel(arrayMaterialsSetup, IsHighLevelMaterial(raceTrack));
        staticPropsLevelSetup.SetLevel(raceTrack);
    }
    //Evento disparado quando todos as referências estão prontas para serem utilizadas
    void Instance_OnReferencesReady()
    {
        driveTrainCar = StaticReferences.Instance.car.GetComponent<Drivetrain>(); 
    }
  /*  void LateUpdate()
    {

        if (Time.frameCount % intervaloUpdate == 0 && driveTrainCar != null)
        {
            float speed = speedAnimation.Evaluate(driveTrainCar.velo * 3.6f);
            pistaR.sharedMaterials[0].SetFloat("_Speed", speed);
            zebrasR.sharedMaterials[0].SetFloat("_Speed", speed);
        }

    }*/



   
}


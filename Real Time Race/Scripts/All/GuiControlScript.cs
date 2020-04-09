using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GuiControlScript : MonoBehaviour
{
    
 //   public Text turboUI;

    public int RPMTESTE;

    public Unit unidadeDevelocidade = Unit.Kilometer;
    public float angOffsetRpm, angOffsetTurbo;
    public Text gear, kmh, kmhReplay, unit;
    public Slider throttle, brake, steering;
    //  public Image accelBrakeSprite;
    public Transform steeringPivo, rpmPivo, turboPointerPivo;
    public float multiplicadorVisualSteering, multiplicadorVisualGiroRpm, multiplicadorVisualGiroTurbo;
    public AnimaCurvePreset steeringCurve, throttleCurve, brakeCurve;
    public float roughnessSteeringTouch,
                 roughnessSteeringAccelerometer,
                 camereraCompensationAmounth,
                 steeringAmounth;

    public bool isSteeringAccelerometer;
    public bool steeringCameraCompensation;

    private CarCamera carCamera;
    private Drivetrain driveTrain;
    private TurboCharger turbine;
    private MobileCarController controler;
    private float unidadeMedida;

    public void Menu()
    {

        LoadScreen.instance.LoadLevel("Menu");
    }

    void Start()
    {

        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);

        ChangeUnit();


    }

    void Instance_OnReferencesReady()
    {
        driveTrain = StaticReferences.Instance.car.GetComponent<Drivetrain>();
        turbine = StaticReferences.Instance.car.GetComponent<TurboCharger>();
        controler = StaticReferences.Instance.car.GetComponent<MobileCarController>();
        carCamera = StaticReferences.Instance.MainCamera.GetComponent<CarCamera>();


    }


    void TurboUiInfo(params string[] message)
    {
        string finalMessage = "";

        for (int i = 0; i < message.Length; i++)
        {
            finalMessage += (message[i] + "\n");
        }

    //    turboUI.text = finalMessage;

    }

    string formated(object value)
    {
        return string.Format("{0:0.0}", value);
    }

    void FixedUpdate()
    {
        if (!StaticReferences.Instance.ready)
            return;

                if (!Application.isEditor)
                {
                    #region Steering

                    if (isSteeringAccelerometer)
                    {
                        SteeringControl(Input.acceleration.normalized.x, roughnessSteeringAccelerometer);
                        if (steeringCameraCompensation)
                            CameraCompensation(controler.stee);
                    }
                    else
                    {
                        SteeringControl((steering.value - 0.5f) * 2, roughnessSteeringTouch);
                        if (!steeringB)
                        {
                            //  SliderDefaulPositionSmooth(steering, 0.1f, 0.5f);
                        }
                    }

                    #endregion

                    #region Brake and Throttle
                    Throttle(throttle);
                    Brake(brake);
                    #endregion
                }
                else
                {
                    controler.acce = Input.GetAxisRaw("Throttle");
                    controler.brac = Input.GetAxisRaw("Brake");
                    SteeringControl(Input.GetAxisRaw("Horizontal"), 0.1f);

                }
        /*
        string unit = " lb/min";
        
        string turbineRpm = "Turbo Rpm = " + formated(turbine.rpmTurbine)+" rpm";
        string turbinePsi = "Turbo Psi = " + formated(turbine.TurbinePressurePsi()) + unit;
        string engineAirFlow = "Engine Air Flow = " + formated(turbine.CmfToLbMin(turbine.EngineAirFlow())) + unit;
        string turbineAirFlow = "Turbo Air Flow = " + formated(turbine.CmfToLbMin(turbine.TurbineAirFlow())) + unit;
        string engineExhaustAirFlow = "Engine Exhaust Air FLow = " + formated(turbine.CmfToLbMin(turbine.EngineExhaustFlow())) + unit;
        string totalPower = "Engine Total Power  = " + formated(driveTrain.currentPower) + " hp";

       /* string 
          turbine.CmfToPsi(), 
            turbine.CmfToPsi(turbine.EngineExhaustFlow()),
            turbine.CmfToPsi(turbine.engineAirFlow), 
            turbine.TurbinePressureRatio()*/

        //    TurboUiInfo(turbineRpm, turbineAirFlow, engineAirFlow, turbinePsi, engineExhaustAirFlow, totalPower);



                string carSpeed = Mathf.Round(StaticReferences.Instance.car.GetComponent<Rigidbody>().velocity.magnitude * MainDefinitions.CurrentUnit).ToString();
        kmh.text = carSpeed;
        kmhReplay.text = string.Concat(MainDefinitions.CurrentSpeedUnit + " : ", carSpeed);

        float delta = 0;
        //Rpm 

        Vector3 rotacaoRpmPointer = ValueToRotationSmooth(rpmPivo, driveTrain.rpm, multiplicadorVisualGiroRpm, angOffsetRpm, delta);
        if (MathHelper.IsValid(rotacaoRpmPointer))
            rpmPivo.eulerAngles = rotacaoRpmPointer;

        //Turbo
        if (turbine != null)
        {
            Vector3 rotacaoTurboPointer = ValueToRotationSmooth(turboPointerPivo, turbine.TurbinePressureRatio(), multiplicadorVisualGiroTurbo, angOffsetTurbo, delta);
            if (MathHelper.IsValid(rotacaoTurboPointer))
                turboPointerPivo.eulerAngles = rotacaoTurboPointer;
        }





        string gearString = (driveTrain.gear - 1).ToString();
        if (gearString == "-1")
        {
            gearString = "R";
        }
        else if (gearString == "0")
        {
            gearString = "N";
        }
        gear.text = gearString;


    }

    void SteeringControl(float steeringValue, float smooth)
    {
        int dir = 1;

        if (steeringValue < 0)
        {
            dir = -1;
        }
        else if (steeringValue > 0)
        {
            dir = 1;
        }

        steeringValue = (steeringCurve.AnimationCurvePreset.Evaluate(Mathf.Abs(steeringValue)) * dir) * steeringAmounth;

        float refValue = 0;

        controler.stee = Mathf.SmoothDamp(controler.stee, steeringValue, ref refValue, smooth);

        steeringPivo.localRotation = Quaternion.Euler(new Vector3(0, 0, -controler.stee * multiplicadorVisualSteering));
    }

    void CameraCompensation(float steeringValue)
    {
        float value = -steeringValue * camereraCompensationAmounth;
        carCamera.Roll(value);
    }

    void Throttle(Slider slider)
    {
        controler.acce = throttleCurve.AnimationCurvePreset.Evaluate(slider.value);
        if (!acelerating)
        {
            SliderDefaulPositionLinear(slider, 5);
        }
    }

    void Brake(Slider slider)
    {
        controler.brac = brakeCurve.AnimationCurvePreset.Evaluate(slider.value);
        if (!braking)
        {
            SliderDefaulPositionLinear(slider, 5);
        }
    }

    void SliderDefaulPositionLinear(Slider slider, float smoothTime)
    {
        //  slider.value = Mathf.SmoothDamp(slider.value, target, ref velocity, smoothTime);
        slider.value -= Time.deltaTime * smoothTime;
    }

    void SliderDefaulPositionSmooth(Slider slider, float smoothTime, float target)
    {
        float velocity = 0;
        slider.value = Mathf.SmoothDamp(slider.value, target, ref velocity, smoothTime);
    }

    bool acelerating;
    public void ThrottleFinishedDraged()
    {
        acelerating = false;
    }

    public void ThrottleBeginDraged()
    {
        acelerating = true;
    }


    bool braking;
    public void BrakeFinishedDraged()
    {
        braking = false;
    }

    public void BrakeBeginDraged()
    {
        braking = true;
    }

    bool steeringB;
    public void SteeringFinishedDraged()
    {
        steeringB = false;
    }

    public void SteeringBeginDraged()
    {
        steeringB = true;
    }


    public void ShitUp()
    {
        StaticReferences.Instance.car.GetComponent<MobileCarController>().ShiftUp();
    }

    public void ShiftDown()
    {
        StaticReferences.Instance.car.GetComponent<MobileCarController>().ShiftDown();
    }

    void ChangeUnit()
    {
        if (unidadeDevelocidade == Unit.Kilometer)
        {
            MainDefinitions.SetUnit(Unit.Kilometer);

        }
        else
        {
            MainDefinitions.SetUnit(Unit.Mile);
        }

      //  unit.text = MainDefinitions.CurrentSpeedUnit;
    }

    Vector3 ValueToRotationSmooth(Transform pivo, float value, float multiplyier, float offset, float delta)
    {
        if (delta > 0)
        {
            return new Vector3(0, 0, Mathf.LerpAngle(pivo.eulerAngles.z, -(value / multiplyier) + offset, delta));
        }
        else
        {
            return new Vector3(0, 0, -(value / multiplyier) + offset);
        }
    }


    public void SteeringEvent(float value)
    {
        print(value);
        SteeringControl(value, 1);
    }

}

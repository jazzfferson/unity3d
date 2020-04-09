using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum VE : int { Stock2Valve=85, Stock4Valve=90, StreetModified=93, Competition=105};
public enum EngineCicles : int {TwoCicleEngine =1,FourCicleEngine=2};
public class TurboCharger : MonoBehaviour {

   

    public AudioSource turbineAudioSource, blowOutAudioSource;
    public VE volumetricEngineEfficiency = VE.Stock4Valve;
    public EngineCicles engineCicles = EngineCicles.FourCicleEngine;

    public float engineLiters;
    public float blowDiference;
    public float horsePowerPerPsi = 10;
    public float airFlowTurbineMultiplier=1;
    public AnimationCurve volumeCurve;
    public AnimationCurve airFlowEfficiencyCurve;
    public AnimationCurve turineDragOverRpm;
    public float minRotationTurbine=15000, maxRotationTurbine=150000;
    public float minPitch=0.5f, maxPitch=3.5f;

    //Turbina
    public float turbineMaxPressureRatio=3.5f;
    public float turbineEfficiency = 1;
    public float turbineVolume=1;
    public float blowOutVolume = 1;
    public int turbinePitch;
    public float loadDamp;
    public float maxPowerCvTurbine;
    public float airFlowInTakeMultiplier;
    public float engineAirFlow;

    [HideInInspector]
    public float rpmTurbine;
    private float pitch;
    private float powerOriginal;
    private float torqueOriginal;
    private float _blowOutVolume;
    private float pressureTurbine;
    private Drivetrain driveTrain;
    public TurboShaft shaft;
    

    

	IEnumerator Start () {

       

        do
        {
            driveTrain = GetComponent<Drivetrain>();
            if (driveTrain)
            {
                powerOriginal = driveTrain.maxPower;
                torqueOriginal = driveTrain.maxTorque;
            }
            yield return null;
        } while (driveTrain==null);

        turbineAudioSource.Play();

        

        
       
	}

   
	
	// Update is called once per frame
	void Update () {

        if (driveTrain)
        {

            float throttle = driveTrain.throttle;
            float forceToRotateTurbine = (EngineExhaustFlow() * turbineEfficiency) *
                (1 + (-turineDragOverRpm.Evaluate(rpmTurbine / maxRotationTurbine)));

            shaft.AddForce(forceToRotateTurbine,Time.deltaTime);

            rpmTurbine = shaft.Rpm;
           
            rpmTurbine = Mathf.Clamp(rpmTurbine, 0, float.PositiveInfinity);  
         
            pitch = rpmTurbine / turbinePitch;
            turbineAudioSource.pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            turbineAudioSource.volume = volumeCurve.Evaluate(rpmTurbine / maxRotationTurbine) * turbineVolume;

            float cv = TurbinePressurePsi() * horsePowerPerPsi;
            float percentage = (cv * 100) / powerOriginal;
            float multiplier = 1 + percentage/100;
            driveTrain.powerMultiplier = multiplier;

            if (!blowOutAudioSource.isPlaying && isBlowvalve)
            {
                BlowOut();
            }

            blowOutAudioSource.volume = Mathf.Clamp(pressureTurbine*blowOutVolume,0.03f,1);
            engineAirFlow = EngineAirFlow();
           
        }
	
	}

    public float TurbinePressureRatio()
    {

        float returnValue;

        pressureTurbine = (1+TurbineAirFlow()) / (1+engineAirFlow);

       

        if (BlowValve(pressureTurbine, blowDiference))
        {
            returnValue = blowDiference;
            
        }
        else
        {
            returnValue = pressureTurbine;
        }

        returnValue = Mathf.Clamp(returnValue, 0, float.PositiveInfinity);

        return returnValue;
    }

    public float TurbinePressurePsi()
    {
        return TurbinePressureRatio() * 14.7f;
    }

    public float TurbineAirFlow()
    {
        return airFlowEfficiencyCurve.Evaluate(rpmTurbine/maxRotationTurbine) * airFlowTurbineMultiplier;
    }

    void BlowOut()
    {
        blowOutAudioSource.Play();
    }
 

    /// <summary>
    /// Converte
    /// "Cubic feets per minute" para "pounds per minute"
    /// </summary>
    /// <param name="cfm"></param>
    /// <returns></returns>
    public float CmfToLbMin(float cfm)
    {
        return cfm * 0.07f;
    }
    /// <summary>
    /// Converte a quantidade de ar por minuto em cavalos de potencia.
    /// </summary>
    /// <param name="psi"></param>
    /// <returns></returns>
    public float PsiToHp(float psi)
    {
        return psi * 10;
    }
    /// <summary>
    /// Converte Psi para pressure ratio levando em consideração a pressão atmosférica.
    /// Por isso é somado 14.7 = 1 atmosfera de pressão.
    /// </summary>
    /// <param name="psi"></param>
    /// <returns></returns>
    public float PsiToPressureRatio(float psi)
    {
        return (14.7f + psi) / 14.7f;
    }

//Engine Temperatures-----------------------------
//Diesel 2-Cycle Naturally Aspirated = 900ºF 4" Hg
//Diesel 2-Cycle Turbo = 750ºF 3" Hg
//Diesel 4-Cycle Naturally Aspirated = 1000ºF 3" Hg
//Diesel 4-Cycle Turbo = 900ºF 3" Hg
//Gasoline (all types) = 1200ºF 4" Hg
//------------------------------------------------

    float temperatura = 1200;
    public float EngineAirFlow()
    {
        return (((driveTrain.maxPower / driveTrain.maxRPM) * driveTrain.rpm) * 2.5f) * (1+TurbinePressureRatio()) * (0.04f + driveTrain.throttle);
    }
    public float EngineExhaustFlow()
    {
        return (((temperatura * (0.04f+driveTrain.throttle)) + 460) / 540) * EngineAirFlow();
    }
    float LitersToCID(float liters)
    {
        return liters * 61.02f;
    }

    bool isBlowvalve;
    bool BlowValve(float pressure,float maxPressure)
    {
        if (pressure > maxPressure)
        {
            isBlowvalve = true;
            return isBlowvalve;
        }
        else
        {
            isBlowvalve = false;
            return isBlowvalve;
        }
    }

}

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Drivetrain))]
public class TorqueCurve : MonoBehaviour
{

    public float torqueMultiplier=1;
    public int precision;
    public AnimaCurvePreset curvePreset;
    private int iterationLength;
  
    public float[,] GetTorqueRPMValues()
    {

        int length = (int)curvePreset.AnimationCurvePreset.keys[curvePreset.AnimationCurvePreset.length-1].time;

       

        iterationLength = (length/precision);

      

        float[,] data = new float[precision+1,2];

        data[0, 0] = 0;
        data[0, 1] = 0;

        for (int i = 1; i < precision+1; i++)
        {
            float rpm = i * iterationLength;
            float torque = curvePreset.AnimationCurvePreset.Evaluate(rpm) * torqueMultiplier;

            data[i, 0] = rpm;
            data[i, 1] = torque;
        }

       
        return data;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Grafico_Potencia : MonoBehaviour {

    public float torqueScale=1;
    public float powerScale=1;

    public int step;
    public float smoothAnimation;
    public LineRenderer lineTorque, lineCavalos;
    public float gridHeight = 300;
    public float gridWidth = 300;
    public Vector3 offset;
    public float scaleWidth=1.0f, scaleHeight=1.0f;
    public Text[] rpmTextUI, torqueTextUI, powerTextUI;
    float floor;
    float top;

    float oldactualMaxPower, actualMaxPower;
    int maxRPM;
    float factor;
    float m_maxTorque;

    public Text maxPowerUI, maxTorqueUI, maxRpmUI;
    public GameObject car;
  
    Drivetrain drivetrain;
    Setup carSetup;

    public Color torqueColor, horsePowerColor;

    float rpmNormalized, torqueNormalized, powerNormalized;
    float torque, power;
    float scale=1;

	void Awake () {

        carSetup = car.GetComponent<Setup>();
        carSetup.OnLoadedSetup += new System.EventHandler(carSetup_OnLoadedSetup);
        top = gridHeight + Mathf.RoundToInt(gridHeight * 0.17f) + floor;
      
        
	}
    void carSetup_OnLoadedSetup(object sender, System.EventArgs e)
    {
        drivetrain = car.GetComponent<Drivetrain>();
        m_maxTorque = drivetrain.maxTorque;
        maxRPM = (Mathf.CeilToInt(drivetrain.maxRPM / 1000) + 1) * 1000;

        print("HY");

        GraficoInfo info = LineUpdate();
        int lengtTotal = info.cavalos.Length;
        lineTorque.SetVertexCount(lengtTotal);
        lineCavalos.SetVertexCount(lengtTotal);
        List<Vector3> tempBasePosition = new List<Vector3>();

        for (int i = 0; i < lengtTotal; i++)
        {
            Vector3 value = new Vector3(info.torque[i].x, 0, 0);
            tempBasePosition.Add(value);
        }

        StartCoroutine(delayMotion(info,tempBasePosition.ToArray()));
        CalcTextValues();
       
        
    }

    IEnumerator delayMotion(GraficoInfo info,Vector3[] basePosition)
    {
        int length = info.torque.Length;
        Vector3[] torqueTempPos = new Vector3[length];
        Vector3[] cavaloTempPos = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            torqueTempPos[i] = new Vector3(basePosition[i].x, 0, 0);
            cavaloTempPos[i] = new Vector3(basePosition[i].x, 0, 0); 
        }


        while (true)
        {
            float deltaTime = Time.deltaTime * smoothAnimation;

            for (int i = 0; i < length; i++)
            {   
               

                torqueTempPos[i] = Vector3.Lerp(torqueTempPos[i], info.torque[i], deltaTime);
                cavaloTempPos[i] = Vector3.Lerp(cavaloTempPos[i], info.cavalos[i], deltaTime);

                lineTorque.SetPosition(i, new Vector3(torqueTempPos[i].x, torqueTempPos[i].y * torqueScale,0));
                lineCavalos.SetPosition(i, new Vector3(cavaloTempPos[i].x, cavaloTempPos[i].y * powerScale, 0));
            }

            yield return null;
        }
        

    }
    GraficoInfo LineUpdate()
    {

        List<Vector3> listPointsTorque = new List<Vector3>();
        List<Vector3> listPointsCavalos = new List<Vector3>();
        GraficoInfo graficoInfo;
     //  grid.SetPixels(buffer);
      
        factor = 1;
        drivetrain.CalcValues(factor, drivetrain.engineTorqueFromFile);
        for (float rpm = 0; rpm <= drivetrain.maxRPM; rpm += 1)
        {
            torque = drivetrain.CalcEngineTorque(1, rpm);
            oldactualMaxPower = actualMaxPower;
            actualMaxPower = torque * rpm * drivetrain.RPM2angularVelo * 0.001f * drivetrain.KW2CV;
            if (actualMaxPower > drivetrain.maxPower && actualMaxPower > oldactualMaxPower && torque < drivetrain.maxTorque) factor = drivetrain.maxPower / actualMaxPower;
        }
        m_maxTorque = Mathf.Round(drivetrain.maxTorque * factor);
        //m_maxNetTorque=Mathf.Round(drivetrain.maxNetTorque*factor);
        //float minRPM=Mathf.Min(drivetrain.minRPM,500);
        for (float rpm = 0; rpm <= drivetrain.maxRPM; rpm += step)
        {
            torque = drivetrain.CalcEngineTorque(drivetrain.powerMultiplier * factor, rpm);
            if (torque > m_maxTorque * drivetrain.powerMultiplier) torque = m_maxTorque * drivetrain.powerMultiplier;

            power = torque * rpm * drivetrain.RPM2angularVelo * 0.001f * drivetrain.KW2CV;

            torqueNormalized = Mathf.RoundToInt((torque / (m_maxTorque * drivetrain.powerMultiplier)) * gridHeight * 0.5f + floor) * scale;

            powerNormalized = Mathf.RoundToInt((power / (drivetrain.maxPower * drivetrain.powerMultiplier)) * gridHeight + floor) * scale;
           
            rpmNormalized = Mathf.RoundToInt((rpm / (maxRPM)) * gridWidth);

            torqueNormalized = MathHelper.ConvertNmToLbft(torqueNormalized);

            listPointsTorque.Add(new Vector3(rpm * scaleWidth, Mathf.Clamp(torqueNormalized, floor, top) * scaleHeight, offset.z));
            listPointsCavalos.Add(new Vector3(rpm * scaleWidth, Mathf.Clamp(powerNormalized, floor, top) * scaleHeight, offset.z));
        }

        graficoInfo.cavalos = listPointsCavalos.ToArray();
        graficoInfo.torque = listPointsTorque.ToArray();

        return graficoInfo;
        
    }
    public struct GraficoInfo
    {
        public Vector3[] torque;
        public Vector3[] cavalos;
    }

    void CalcTextValues()
    {
        float rpmStep = drivetrain.maxRPM/rpmTextUI.Length;
        for (int i = 0; i < rpmTextUI.Length; i++)
        {
            rpmTextUI[i].text = ((i+1) * rpmStep).ToString();
        }
    }

  
}

 



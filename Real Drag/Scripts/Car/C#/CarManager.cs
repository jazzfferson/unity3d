using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;

public class CarManager : MonoBehaviour
{

    public AnimationCurve curvaSteering;
    public float speedSteeringSensitive;
    public Motor motor;
    public Transmsition transmition;
    public Steering steer;
    public Brake brake;
    public UISlider accelbrakeSlider;
    public UISlider steeringSlider;
    public ContaGiro contaGiro;
    public Kmh kmh;
    public Body body;
    public SoundEngine soundEngine;
    public Gear gear;
    public int minRpmChangeGear, maxRpmChangeGear;
    public GameObject cameraJogo, cameraReplay;
    public UILabel replayLabel;
    public CameraManager camManager;
    public WheelCollider[] wheels;
    public SteeringHud steerHud;






    private int actualGear = 1;
    private float accel;
    private int brak;


    Vector3 infoCar;
    Quaternion infoCarR;
    bool automaticGearDelayboolUp = false;
    bool automaticGearDelayboolDown = false;

    static bool recording = true;
    int indexReplay;


    void Awake()
    {
        if (ReplayData.accel == null)
        {
            Initi();
        }
    }
    void Initi()
    {
        ReplayData.accel = new List<float>();
        ReplayData.brake = new List<float>();
        ReplayData.gear = new List<int>();
        ReplayData.steering = new List<float>();
        ReplayData.position = new List<Vector3>();
        ReplayData.rotation = new List<Quaternion>();
    }
    IEnumerator Start()
    {

        Cursor.visible = false;

        infoCar = transform.position;
        infoCarR = transform.rotation;

        if (!recording)
        {

            ReplayCameraManager.instance.ReplayManager = true;

            cameraJogo.SetActive(false);
            cameraReplay.SetActive(true);
            camManager.CarRender(true);
        }
        else
        {
            ReplayCameraManager.instance.ReplayManager = false;
            cameraJogo.SetActive(true);
            cameraReplay.SetActive(false);
        }

        yield return new WaitForSeconds(0.2f);
        actualGear = 1;
        ChangeGear(actualGear);




    }


    void FixedUpdate()
    {


        Player();

        motor.MotorUpdate(transmition.ActualGearRatio, transmition.FinalDrive, accel);
        contaGiro.Rpm = soundEngine.finalRpm;
        kmh.KmhValue = body.Kmh;


    }

    void Update()
    {

        if (Input.GetKey(KeyCode.L))
        {
            Application.LoadLevel(0);
            recording = true;
            Initi();

        }

        /* if (Input.GetKeyDown(KeyCode.RightArrow))
         {
             NextGear();
         }
         if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
             PreviousGear();
         }*/



    }

    void Player()
    {
        if (recording)
        {
            replayLabel.gameObject.SetActive(false);


            float valor = (accelbrakeSlider.value - 0.5f) * 2;
            if (valor > 0)
            {
                accel = valor;
                brake.ApplyBrake(0);
            }
            else
            {
                accel = 0;
                brake.ApplyBrake(valor);
            }


            SaveData(accel, brake.Amount, steeringSlider.value, actualGear, GetComponent<Rigidbody>().transform.position, GetComponent<Rigidbody>().transform.rotation);
        }

        else
        {
            replayLabel.gameObject.SetActive(true);
            accel = ReplayData.accel[indexReplay];
            brake.ApplyBrake(ReplayData.brake[indexReplay]);
            steeringSlider.value = ReplayData.steering[indexReplay];
            GetComponent<Rigidbody>().transform.position = ReplayData.position[indexReplay];
            GetComponent<Rigidbody>().transform.rotation = ReplayData.rotation[indexReplay];

            if (indexReplay >= ReplayData.rotation.Count - 1)
            {
                Application.LoadLevel(0);
                recording = false;
            }
            else
                indexReplay++;

        }

        Steering();

        AutomaticGear();
    }

    public void AccelBrake()
    {
        float valor = (accelbrakeSlider.value - 0.5f) * 2;

        if (valor > 0)
        {
            accel = valor;
        }
        if (valor < 0)
        {
            brake.ApplyBrake(valor);
        }
        else
        {
            brake.ApplyBrake(0);
        }
    }

    void AccelBrakeReleased()
    {
        Go.to(accelbrakeSlider, 0.3f, new GoTweenConfig().floatProp("value", 0.5f, false));
    }

    void BrakeUp()
    {
        brake.ApplyBrake(1);
    }

    void BrakeDown()
    {
        brake.ApplyBrake(0);
    }

    void Brake()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            brake.ApplyBrake(Input.GetAxis("Vertical"));
        }
        else
        {
            brake.ApplyBrake(0);
        }
    }

    void Throttle()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            accel = Input.GetAxis("Vertical");
        }
        else
        {
            accel = 0;
        }
    }

    public void Steering()
    {


        float amount = Mathf.Abs(steeringSlider.value - 0.5f) * 2;
        float valor = -curvaSteering.Evaluate(amount);

       
        
        int dir = 1;

        if ((steeringSlider.value - 0.5f) > 0)
            dir = -1;
        else
            dir = 1;

        steerHud.UpdateSteering(-valor *dir);
        steer.SteeringAmount((valor * 3) * dir);

    }

    void NextGear()
    {
        actualGear++;
        ChangeGear(actualGear);

    }

    void PreviousGear()
    {
        actualGear--;
        ChangeGear(actualGear);
    }

    void ChangeGear(int index)
    {
        index = (int)Mathf.Clamp(index, 0, transmition.GearsCount);
        transmition.ChangeGear(index);
        gear.value.text = index.ToString();
    }

    public void Engatar()
    {
        Go.to(this, 1f, new GoTweenConfig().floatProp("Gluth", 1, false));
    }

    void AutomaticGear()
    {
        if (soundEngine.finalRpm >= maxRpmChangeGear && !automaticGearDelayboolUp && actualGear < 6)
        {
            automaticGearDelayboolUp = true;
            Invoke("AutomaticGearDelayUp", transmition.speedChange + 0.1f);
            actualGear++;
            ChangeGear(actualGear);
        }
        if (soundEngine.finalRpm <= minRpmChangeGear && !automaticGearDelayboolUp && actualGear > 1)
        {
            automaticGearDelayboolDown = true;
            Invoke("AutomaticGearDelayUp", transmition.speedChange + 0.1f);
            actualGear--;
            ChangeGear(actualGear);

        }
    }

    void AutomaticGearDelayUp()
    {
        automaticGearDelayboolUp = false;
    }

    void AutomaticGearDelayDown()
    {
        automaticGearDelayboolDown = false;
    }

    void SaveData(float accel, float brake, float steering, int gear, Vector3 posicao, Quaternion rotacao)
    {
        ReplayData.accel.Add(accel);
        ReplayData.brake.Add(brake);
        ReplayData.steering.Add(steering);
        ReplayData.gear.Add(gear);
        ReplayData.position.Add(posicao);
        ReplayData.rotation.Add(rotacao);
    }

    public void Replay()
    {

        Application.LoadLevel(0);
        recording = false;
    }
    public void StartGame()
    {
        Initi();
        Application.LoadLevel(0);
        recording = true;
    }

}


public static class ReplayData
{
    public static List<float> accel;

    public static List<float> brake;

    public static List<float> steering;

    public static List<int> gear;

    public static List<Vector3> position;

    public static List<Quaternion> rotation;

}

using UnityEngine;
using System.Collections;

public class PlayerController : PossesController {

    [HideInInspector]
    public bool playerEnable = true;

    private GameObject carplayerGameObject;
    public GameObject CarPlayer
    {
        get { return carplayerGameObject; }
    }

    void Awake()
    {
        carplayerGameObject = GameObject.FindGameObjectWithTag("Player");
        carplayerGameObject.GetComponent<CarController>().possesController = this;
    }

    void Update () {

        if (!playerEnable)
            return;

        isRightSteering = false;
        isLeftSteering = false;

        if (Input.GetKeyDown(KeyCode.A))
        {
            gearUpDelegate();
        }
        else if(Input.GetKeyUp(KeyCode.Z))
        {
            gearDownDelegate();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            throttleDelegate(1);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            throttleDelegate(0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            brakeDelegate(1);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            brakeDelegate(0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            isRightSteering = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeftSteering = true;
        }

        if(isLeftSteering&&isRightSteering || !isLeftSteering && !isRightSteering)
        {
            steeringDelegate(0);
        }
        else if(isLeftSteering)
        {
            steeringDelegate(-1);
        }
        else if (isRightSteering)
        {
            steeringDelegate(1);
        }
    }
}

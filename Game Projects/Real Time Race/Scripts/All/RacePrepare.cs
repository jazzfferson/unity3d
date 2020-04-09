using UnityEngine;
using System.Collections;
using System;
public enum Ecar : int { GTOne = 0, Porche = 1, OldCar = 2, Mustang = 3, NoneCar = 4 };
public enum Epista : int { TestTrack = 0, SpenMotorsport = 1, Montain = 2, Mexico_Evening = 3, Mexico_Night = 4, TestTrack2 = 5, Chuckwalla = 6, SimpleTeste=7,Fiction=8,NonePista };


public class RacePrepare : MonoBehaviour
{

    public Ecar carroEditor;
    public Epista pistaEditor;
    public bool editorChoise;

    public static Ecar carro = Ecar.OldCar;
    public static Epista pista = Epista.SpenMotorsport;
    public GameObject carroObj;

    public event EventHandler OnLoaded;
    bool loaded;
    Transform carSpawTransform;

    void Awake()
    {
        if (editorChoise)
        {
            carro = carroEditor;
            pista = pistaEditor;
        }

        carSpawTransform = GetComponent<StaticReferences>().pista.GetComponent<TrackReferences>().carStartPosition;

       
        StartCoroutine(LoadObjs());
 
    }


    IEnumerator LoadObjs()
    {
        yield return new WaitForSeconds(0.1f);

        if (carroObj == null)
        {
            GameObject cObj = Resources.Load<GameObject>("Carros/" + carro.ToString());
            carroObj = (GameObject)Instantiate(cObj);
            carroObj.transform.position = carSpawTransform.position + new Vector3(0, 1, 0);
            carroObj.transform.rotation = carSpawTransform.rotation;
            loaded = true;
        }
        else
        {
            carroObj.transform.position = carSpawTransform.position + new Vector3(0, 1, 0);
            carroObj.transform.rotation = carSpawTransform.rotation;

            if (OnLoaded != null)
            {
                OnLoaded(this, null);
            }
            loaded = true;
        }

       


    }

    // Update is called once per frame
    void Update() {
        
        if (loaded && carroObj != null)
        {
            if (OnLoaded != null)
            {
                OnLoaded(this, null);
            }

            loaded = false;
        }	
	}
}

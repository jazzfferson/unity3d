using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public CopyPosRot scriptCapo;
    public CameraThirdView scriptTraseira;
    public Transform capo, livre;
    public GameObject [] visualsCarro;

    int cameraIndex;

    void Start()
    {
        CarroView();
    }
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LivreView();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CapoView();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            CarroView();
        }
	
	}

    void CapoView()
    {
        scriptCapo.target = capo;
        scriptCapo.enabled = true;
        scriptTraseira.enabled = false;

        CarRender(true);
    }
    void LivreView()
    {
        scriptCapo.target = livre;
        scriptCapo.enabled = true;
        scriptTraseira.enabled = false;
        CarRender(false);
    }
    void CarroView()
    {
        scriptCapo.enabled = false;
        scriptTraseira.enabled = true;
        CarRender(true);
    }
    public void CarRender(bool enable)
    {
        foreach (GameObject part in visualsCarro)
        {
            part.SetActive(enable);
        }
    }
    public void ChangeView()
    {
        cameraIndex++;

        switch ((int)Mathf.Repeat(cameraIndex, 3))
        {
            case 0:
            CarroView();
            break;

            case 1:
            CapoView();
            break;

            case 2:
            LivreView();
            break;
        }
    }

   
}

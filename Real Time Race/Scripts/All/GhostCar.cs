using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameTimers))]
public class GhostCar : MonoBehaviour {
    
    public GameObject prefab;
    public bool mustHaveGhostCar = false;
    public Material ghostMaterial;
    public AnimationCurve alphaCurve;

    private GameObject ghostCarVisual;
    private GameTimers gameTimers;
    private CarReplay carReplayManager;
    private GhostReplayData ghostData;
    private Transform carTransform;

    //Em qual frame o ghost vai começar
    int startFrame;
    // Em qual frame o ghost vai acabar
    int finishFrame;
    //guarda o start frame da ultima volta
    int lastStartFrame;
   

    bool runGhost=false;
    bool firstLap=true;
    bool newFastLap = false;

  
    const float interp = 0.008f;
   
  

    void Start()
    {
        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);
    }

    void Instance_OnReferencesReady()
    {
        if (!ReplayControlGui.Replaying && mustHaveGhostCar)
        {
            carReplayManager = StaticReferences.Instance.car.GetComponent<CarReplay>();
            carTransform = StaticReferences.Instance.car.transform;
            gameTimers = GetComponent<GameTimers>();
            gameTimers.OnNewBestTime += new System.EventHandler(gameTimers_OnNewBestTime);
            gameTimers.OnRaceStarted += new System.EventHandler(gameTimers_OnRaceStarted);
            gameTimers.OnFinishedLap += new System.EventHandler(gameTimers_OnFinishedLap);
            gameTimers.OnRaceEnded += new System.EventHandler(gameTimers_OnRaceEnded);
 
        }
    }

    void gameTimers_OnRaceStarted(object sender, System.EventArgs e)
    {
        //Salva o frame quando a corrida inicia
        if(firstLap)
        startFrame = carReplayManager.WriteReplayData.Frames;
    }

    void gameTimers_OnFinishedLap(object sender, System.EventArgs e)
    {
        runGhost = true;
        firstLap = false;    
    }

    void gameTimers_OnNewBestTime(object sender, System.EventArgs e)
    {
        lastStartFrame = startFrame;
        finishFrame = carReplayManager.WriteReplayData.Frames;
        startFrame = finishFrame;
        //Convert o replay do carro em replay para o ghostCar
        ghostData = null;
        ghostData = new GhostReplayData(carReplayManager.WriteReplayData.CreateScriptableData(), lastStartFrame, finishFrame);

        if (ghostCarVisual == null)
        {
            ghostCarVisual = (GameObject)Instantiate(prefab, ghostData.body[lastStartFrame].GetPosition(),
              ghostData.body[lastStartFrame].GetRotation());
        }

        StartCoroutine(CustomUpdate()); 
    }

    void gameTimers_OnRaceEnded(object sender, System.EventArgs e)
    {
        Destroy(ghostCarVisual);
    }

    float alpha = 0;


	void FixedUpdate () {

        if (runGhost)
        {
            
                ghostCarVisual.transform.position = ghostData.GetPosicao();
                ghostCarVisual.transform.rotation = ghostData.GetRotacao();
                
                if (!ghostData.NextFrame())
                {
                    runGhost = false;
                    ghostCarVisual.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ghostCarVisual.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            
        }
	}
  
    IEnumerator CustomUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);
            alpha = alphaCurve.Evaluate(Vector3.Distance(ghostCarVisual.transform.position, carTransform.position)/100);
            ghostMaterial.SetColor("_Color", new Color(ghostMaterial.color.r, ghostMaterial.color.g, ghostMaterial.color.b, alpha));
        }
       
        
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayCameraManager : MonoBehaviour
{

    public ReplayCam cameraReplay;
    private CameraSessionTriggerEvent[] cameraEventTriggers;

    void Start()
    {

        if (ReplayControlGui.Replaying)
        {
            StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);
        }
    }

    void Instance_OnReferencesReady()
    {
        cameraEventTriggers = StaticReferences.Instance.pista.GetComponent<TrackReferences>().sessoes;

        //Passa para a camera as configurações da primeira sessão
        if (cameraEventTriggers != null && cameraEventTriggers.Length > 0)
            cameraReplay.SetConfig(cameraEventTriggers[0]);

        //Associa o metodo dessa classe aos eventos do triggers.
        foreach (EventTrigger tr in cameraEventTriggers)
        {
            tr.Enter += new EventTrigger.EventTriggerDelegate(tr_Enter);
        }  
    }

    void tr_Enter(string Name, int[] IDs)
    {
        //Printa a sessão.
        print("Passou na sessão : " + Name + " ID : " + IDs[0]);
        //Seta as  propriedades da camera conforme a sessão.
        cameraReplay.SetConfig(cameraEventTriggers[IDs[0]]);
        StaticReferences.Instance.ReplayCameraChangeEvent();
    }
}

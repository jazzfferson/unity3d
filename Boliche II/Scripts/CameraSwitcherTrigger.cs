using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcherTrigger : BoxTrigger
{

    [SerializeField]
    private CameraSwitcher cameraSwitcher;

    [SerializeField]
    private int cameraIndex;

    [SerializeField]
    private float delay = 2;

    private void Awake()
    {
        OnEnterTrigger += CameraSwitcherTrigger_OnEnterTrigger;
    }

    private void CameraSwitcherTrigger_OnEnterTrigger(GameObject other)
    {
        StartCoroutine(SetCamera());
    }

    private IEnumerator SetCamera()
    {
        yield return new WaitForSeconds(delay);
        cameraSwitcher.Switch(cameraIndex);
    }
}

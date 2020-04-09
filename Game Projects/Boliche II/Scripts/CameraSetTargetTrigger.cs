using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetTargetTrigger : BoxTrigger
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera camera;

    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float delay = 0;

    [SerializeField]
    private bool setTargetPositionToCollisionPosition = false;

    private void Awake()
    {
      //  OnEnterTrigger += CameraSwitcherTrigger_OnEnterTrigger;
        OnCollisionEnterEvent += CameraSetTargetTrigger_OnEnterCollision;
    }

    private void CameraSetTargetTrigger_OnEnterCollision(Collision other)
    {

        if (setTargetPositionToCollisionPosition)
        {
            m_Target.position = other.contacts[0].point;
        }
        StartCoroutine(SetCamera(m_Target));
    }


    private IEnumerator SetCamera(Transform target)
    {
        yield return new WaitForSeconds(delay);
        camera.LookAt = target; 
    }
}

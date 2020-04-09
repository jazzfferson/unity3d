using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaBoliche : MonoBehaviour
{
    public Rigidbody m_rigidbody;

    private float defaultDrag, defaultAngularDrag;

    private void Awake()
    {
        defaultDrag = m_rigidbody.drag;
        defaultAngularDrag = m_rigidbody.angularDrag;
    }
    public void SetarEstado(bool esperandoParaLancar)
    {
        if (esperandoParaLancar)
        {
            m_rigidbody.isKinematic = true;
            ResetRigidbody();
        }
        else
        {
            m_rigidbody.isKinematic = false;
        }
    }
    public void AddImpulso(float intensidade,float lateralForce,float upForce,float rotationAmount)
    {
        m_rigidbody.AddTorque(new Vector3(0, 0, rotationAmount), ForceMode.VelocityChange);
       // m_rigidbody.angularVelocity = new Vector3(0,0, rotationAmount);
        m_rigidbody.AddForce(new Vector3(lateralForce, upForce, intensidade), ForceMode.Impulse);
    }

    private void ResetRigidbody()
    {
        m_rigidbody.drag = defaultDrag;
        m_rigidbody.angularDrag = defaultAngularDrag;
    }
}

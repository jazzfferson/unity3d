using UnityEngine;
using System.Collections;


public class Pino : MonoBehaviour
{
	public int ID;
    public bool derrubado = false;
    public bool deveSerContabilizado = true;
    public Transform defaultOriginTransform;
    [SerializeField]
    private Rigidbody m_rigidbody;

    private float defaultDrag, defaultAngularDrag;

    public bool SetKinematic
    {
        set { m_rigidbody.isKinematic = value; }
    }

    private void Awake()
    {
        //m_rigidbody.centerOfMass = new Vector3(0,0.3f,0);
        defaultDrag = m_rigidbody.drag;
        defaultAngularDrag = m_rigidbody.angularDrag;
    }

    /// <summary>
    /// - Zera a velocidade de movimento e angular
    /// - Retorna o drag e o angularDrag para o padrão
    /// </summary>
    public void ResetRigidybodyMovement()
    {
        m_rigidbody.angularVelocity = Vector3.zero;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularDrag = defaultAngularDrag;
        m_rigidbody.drag = defaultDrag;
    }
    public void SetPseudoStatic(bool ativo)
	{
        if(!ativo)
        {
            m_rigidbody.angularDrag = defaultAngularDrag;
            m_rigidbody.drag = defaultDrag;
        }
        else
        {
            m_rigidbody.angularDrag = 2;
            m_rigidbody.drag = 2;
        }
        //m_rigidbody.isKinematic = !ativo;
	}
	public void ResetarPino(Transform parent)
	{
        ResetRigidybodyMovement();
        transform.SetParent(parent);
        transform.rotation = defaultOriginTransform.rotation;
        transform.position = defaultOriginTransform.position; 
        derrubado = false;
        deveSerContabilizado = true;
    }

    public void ProgressivePhysicsSleep(float amount)
    {
        m_rigidbody.angularDrag += amount;
        m_rigidbody.drag += amount;
    }
    public bool IsMoving()
    {
        return m_rigidbody.angularVelocity.magnitude > 0.005f || m_rigidbody.velocity.magnitude > 0.005f;
    }

    public delegate void PinoEventHandler(Pino pino);
    public event PinoEventHandler OnPinoTouchTrackEvent;
    public PinoEventHandler OnPinoTouchTrackDelegate;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pista"))
        {

            //Debug.Log(collision.gameObject.name + " Tocou no pino");

            if (OnPinoTouchTrackDelegate != null)
                OnPinoTouchTrackDelegate(this);

            if (OnPinoTouchTrackEvent != null)
                OnPinoTouchTrackEvent(this);
        }
    }
   /* private void OnTriggerEnter(Collider collider)
    {

        Debug.Log(collider.gameObject.name + " Tocou na pista");

        if (collider.gameObject.CompareTag("Pista"))
        {



            if (OnPinoTouchTrackDelegate != null)
                OnPinoTouchTrackDelegate(this);

            if (OnPinoTouchTrackEvent != null)
                OnPinoTouchTrackEvent(this);
        }
    }*/
}
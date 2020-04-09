using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class LancadorBoliche : StateMachine
{
    [SerializeField]
    private CinemachineVirtualCamera posLancamentoCamera;
    [SerializeField]
    private GameObject seta;
    [SerializeField]
    private MeshRenderer setaMeshRenderer;
    [SerializeField]
    private float maxXMovement = 9,
                  addRotation = 0, 
                  lateralMultiplier = 10,
                  upForce = 50 ,
                  movementSpeed = 2;

    [SerializeField]
    private AnimationCurve m_movementEaseCurve;

   [SerializeField]
    private BolaBoliche bolaPrefab;
    [SerializeField]
    private int intensidadeLancamento = 600;

    private BolaBoliche m_instantiatedBola;
    private bool bolaChegouAoFinalDaPistaEventFired = false;
    private Vector3 m_originalPosition;
    private float xPositionEvaluateValue;
    private float lateralMovement;
    private float lastXPosition;
    private Color setaColor;

    private void Awake()
    {
        m_originalPosition = transform.localPosition;
        setaColor = setaMeshRenderer.material.color;
        m_movementEaseCurve.preWrapMode = WrapMode.PingPong;
        m_movementEaseCurve.postWrapMode = WrapMode.PingPong;
    }

    private void Update()
    {
        UpdateCurrentState();
    }

    public void NovaBola()
    {
        if (m_instantiatedBola == null)
        {
            m_instantiatedBola = GameObject.Instantiate(bolaPrefab) as BolaBoliche;
            posLancamentoCamera.LookAt = m_instantiatedBola.transform;
        }
        m_instantiatedBola.transform.SetParent(transform);
        m_instantiatedBola.SetarEstado(true);


        Transform trans = m_instantiatedBola.transform;
        trans.position = transform.position;

        bolaChegouAoFinalDaPistaEventFired = false;
    }

    public void SetPosition(float positionEvaluate)
    {
        Vector3 newPosition = new Vector3(Mathf.LerpUnclamped(-maxXMovement, maxXMovement, positionEvaluate), m_originalPosition.y, m_originalPosition.z);
        transform.localPosition = newPosition;
    }
    public void AnimatedPosition(float time)
    {
        float curvedEvaluated = m_movementEaseCurve.Evaluate(time);
        SetPosition(curvedEvaluated);
    }

    public void LaunchBall()
    {
        m_instantiatedBola.transform.SetParent(null);
        m_instantiatedBola.SetarEstado(false);
        m_instantiatedBola.AddImpulso(intensidadeLancamento,
            lateralMovement,
            upForce,
            addRotation);
    }

    public delegate void LancadorDelegateHandler();
    public event LancadorDelegateHandler OnBolaChegouAoFinalDaPistaEvent;
    public LancadorDelegateHandler OnBolaChegouAoFinalDaPistaDelegate;

    public float MovementSpeed { get => movementSpeed; }

    private void OnBolaChegouFinalDaPista()
    {
        if (OnBolaChegouAoFinalDaPistaDelegate != null)
        {
            OnBolaChegouAoFinalDaPistaDelegate();
        }

        if (OnBolaChegouAoFinalDaPistaEvent != null)
        {
            OnBolaChegouAoFinalDaPistaEvent();
        }
    }

    public void CheckEvents()
    {
        if (bolaChegouAoFinalDaPistaEventFired)
            return;

        if (m_instantiatedBola != null)
        {
            if (m_instantiatedBola.transform.position.z > 19 && m_instantiatedBola.transform.position.y < -0.5f)
            {
                bolaChegouAoFinalDaPistaEventFired = true;
                OnBolaChegouFinalDaPista();
            }
        }
    }

    public void SetVisibilty(float alpha,float time)
    {
        setaMeshRenderer.material.DOFade(alpha, time);
    }


    public sealed class LancadorWaitingState : IState
    {
        private LancadorBoliche m_LancadorBoliche;

        public string Name { get; private set; }

        public LancadorWaitingState(LancadorBoliche lancadorBoliche, string stateName)
        {
            Name = stateName;
            m_LancadorBoliche = lancadorBoliche;
        }

        public void Enter()
        {
            m_LancadorBoliche.SetPosition(0.5f);
            m_LancadorBoliche.NovaBola();
            m_LancadorBoliche.SetVisibilty(1, 0.5f);
        }
        public void Update()
        {
            m_LancadorBoliche.AnimatedPosition(Time.unscaledTime * m_LancadorBoliche.MovementSpeed);
        }
        public void Exit()
        {

        }

    }

    public sealed class LancadorLauchedState : IState
    {
        private LancadorBoliche m_LancadorBoliche;

        public string Name { get; private set; }

        public LancadorLauchedState(LancadorBoliche lancadorBoliche,string stateName)
        {
            Name = stateName;
            m_LancadorBoliche = lancadorBoliche;
        }

        public void Update()
        {
            m_LancadorBoliche.CheckEvents();
        }

        public void Enter()
        {
            m_LancadorBoliche.SetVisibilty(0, 0.25f);
            m_LancadorBoliche.LaunchBall();
        }

        public void Exit()
        {
           // throw new System.NotImplementedException();
        }
    }

    public sealed class LancadorStartState : IState
    {
        private LancadorBoliche m_LancadorBoliche;

        public string Name { get; private set; }

        public LancadorStartState(LancadorBoliche lancadorBoliche, string stateName)
        {
            Name = stateName;
            m_LancadorBoliche = lancadorBoliche;
        }

        public void Update()
        {

        }

        public void Enter()
        {
            m_LancadorBoliche.SetVisibilty(0, 0f);
            m_LancadorBoliche.SetPosition(0.5f);
        }

        public void Exit()
        {

        }
    }
}



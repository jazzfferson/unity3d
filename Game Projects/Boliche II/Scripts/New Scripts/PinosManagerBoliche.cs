using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PinosManagerBoliche : MonoBehaviour
{
    public delegate void PinosManagerEvent();
    public event PinosManagerEvent OnBallHitPins, OnReadyToCheckPinsHited, OnClearedTrackWithNewPins, OnClearedTrack, OnPinosColocados,OnWaitingForPinsToFall;
    public Animator protetorAnimator, colocadorAnimator;
    public AnimationEventController protetorEventController, colocadorEventController;

    public BoxTrigger pinoBoxTrigger;
    public Transform[] pinoPosition;
    public Pino pinoPrefab;
    private List<Pino> instantiatedPinosList;
    public List<Pino> AllPinos
    {
        get => instantiatedPinosList;
    }

    public ContadorPinos contadorPinos;

    
    private bool waitingForPinsToFall = false;
    private bool atLastOnePinHited = false;

    void Awake()
    {
        instantiatedPinosList = new List<Pino>();   
    }

    private void Start()
    {      
        pinoBoxTrigger.OnEnterTrigger += PinoBoxTrigger_BolaPassou;
        contadorPinos.OnPinoCaiu += ContadorPinos_PinoCaiu;
    }
 
    public List<Pino> ListaDePinosCaidos()
    {
        return instantiatedPinosList.FindAll(currentPino => currentPino.derrubado);
    }
    public List<Pino> ListaDePinosEmPe()
    {
        return instantiatedPinosList.FindAll(currentPino => !currentPino.derrubado);
    }

    public void LimparPistaEColocarPinosQueFicaram()
    {
        ColocadorPegarPinosEmPe((AnimatorStateInfo animClipInfo2, string animaClipName2) =>
        {
            LimpadorLimparPista((AnimatorStateInfo animClipInfo3, string animaClipName3) =>
            {
                ColocadorColocarPinosNaPista(ListaDePinosEmPe(), (AnimatorStateInfo animClipInfo4, string animaClipName4) =>
                {
                    PlayLevantarProtetor((AnimatorStateInfo animClipInfo5, string animaClipName5) =>
                    {
                        FireEvent(OnClearedTrackWithNewPins);
                    });
                });
            });
        });
    }
    public void LimparPistaEColocarTodosOsPinos()
    {
        instantiatedPinosList.ForEach(pino =>
        {
            pino.ResetRigidybodyMovement();
            pino.SetKinematic = false;
        });

        LimpadorLimparPista((AnimatorStateInfo animClipInfo3, string animaClipName3) =>
        {
            ColocadorColocarPinosNaPista(instantiatedPinosList, (AnimatorStateInfo animClipInfo4, string animaClipName4) =>
            {
                PlayLevantarProtetor((AnimatorStateInfo animClipInfo5, string animaClipName5) =>
                {
                    FireEvent(OnClearedTrackWithNewPins);
                });
            });
        });
    }

    public void ColocarPinos(List<Pino> pinosParaColocar)
    {
        PlayAbaixarProtetor((AnimatorStateInfo ani, string aniNam) =>
        {
            ColocadorColocarPinosNaPista(pinosParaColocar, (AnimatorStateInfo ani2, string aniNam2)=> 
            {
                PlayLevantarProtetor();
            });
        });
    }

    private void ColocadorPegarPinosEmPe(AnimationEventController.AnimationControllerEventHandler onFinishDelegate = null)
    {
        contadorPinos.EnableContador = false;

        AnimationEventController.AnimationControllerEventHandler colocadorAbaixarFinishDelegate = (AnimatorStateInfo animClipInfo, string animaClipName) =>
        {
            ListaDePinosEmPe().ForEach(pinoIterator =>
            {
                pinoIterator.transform.SetParent(colocadorAnimator.transform);
                pinoIterator.SetKinematic = true;
            });
        
            PlayLevantarColocador(onFinishDelegate);
        };

        PlayAbaixarColocador(colocadorAbaixarFinishDelegate);
    }
    private void ColocadorColocarPinosNaPista(List<Pino> pinosParaColocar, AnimationEventController.AnimationControllerEventHandler onFinishDelegate = null)
    {
        if(pinosParaColocar.Count==0) return;

        contadorPinos.EnableContador = false;

        pinosParaColocar.ForEach(pino =>
        {
            pino.SetKinematic = true;
            pino.ResetarPino(colocadorAnimator.transform);
        });

        //Animacao do colocador de pino Descendo
        //Registrando o delegate do callback 
        PlayAbaixarColocador((AnimatorStateInfo animatorClipInfo, string animationClipName) =>
        {
            PlayLevantarColocador((AnimatorStateInfo ani, string aniNam) =>
            {
                contadorPinos.EnableContador = true;
                onFinishDelegate(ani, aniNam);
                FireEvent(OnPinosColocados);
            });
        });

        //Evento de callback que sera chamado quando o pino tocar a pista
        //Registrando o delegate do callback
        pinosParaColocar[0].OnPinoTouchTrackDelegate = (Pino pino) =>
        {
            //Tira a referencia dessa funcão do delegate
            pino.OnPinoTouchTrackDelegate = null;

            pinosParaColocar.ForEach(pinoIterator =>
            {
                pinoIterator.transform.SetParent(null);
                pinoIterator.SetKinematic = false;
                pinoIterator.SetPseudoStatic(true);
            });
        };
    }
    private void LimpadorLimparPista(AnimationEventController.AnimationControllerEventHandler onFinishDelegate = null)
    {
        PlayProtetorLimparIn((AnimatorStateInfo animClipInfo, string animaClipName) =>
        {
            PlayProtetorLimparOut((AnimatorStateInfo animClipInfo2, string animaClipName2) =>
            {
                onFinishDelegate(animClipInfo2, animaClipName2);
                FireEvent(OnClearedTrack);
            });
        });
    }
   

    #region Animações
        public void PlayLevantarProtetor(AnimationEventController.AnimationControllerEventHandler delegateMethod = null)
        {
            protetorAnimator.Play("Proteger Pinos Out");
            protetorEventController.OnEndAnimationDelegate = delegateMethod;
        }
        public void PlayAbaixarProtetor(AnimationEventController.AnimationControllerEventHandler delegateMethod = null)
        {
            protetorAnimator.Play("Proteger Pinos In");
            protetorEventController.OnEndAnimationDelegate = delegateMethod;
        }
        public void PlayProtetorLimparIn(AnimationEventController.AnimationControllerEventHandler delegateMethod = null)
        {
            protetorAnimator.Play("Limpar Pinos Motion In");
            protetorEventController.OnEndAnimationDelegate = delegateMethod;
        }
        public void PlayProtetorLimparOut(AnimationEventController.AnimationControllerEventHandler delegateMethod = null)
        {
            protetorAnimator.Play("Limpar Pinos Motion Out");
            protetorEventController.OnEndAnimationDelegate = delegateMethod;
        }
        public void PlayAbaixarColocador(AnimationEventController.AnimationControllerEventHandler delegateMethod = null)
        {
            colocadorAnimator.Play("Colocador Down Motion");
            colocadorEventController.OnEndAnimationDelegate = delegateMethod;
        }
        public void PlayLevantarColocador(AnimationEventController.AnimationControllerEventHandler delegateMethod = null)
        {
            colocadorAnimator.Play("Colocador Up Motion");
            colocadorEventController.OnEndAnimationDelegate = delegateMethod;
        }
    #endregion

    #region Events

    private void FireEvent(PinosManagerEvent eventToFire)
    {
        if(eventToFire != null)
        {
            eventToFire();
        }
    }
    #endregion

    public void CriarTodosPinos()
    {
        int pinoIndex = 0;
        foreach (Transform trans in pinoPosition)
        {
            Pino newPino = Instantiate(pinoPrefab) as Pino;
            instantiatedPinosList.Add(newPino);
            newPino.ID = pinoIndex;
            pinoIndex++;

            //Gera a posicao default do pino na pista
            // GerarPosicaoOriginalPinos(trans.position, newPino);
            newPino.defaultOriginTransform = trans;

            //Coloca o pino na posicao gerada e reseta os movimentos dele
            newPino.ResetarPino(colocadorAnimator.transform);

            //Impede que o pino se mova
            newPino.SetKinematic = true;
        }

    }

    private IEnumerator PodeContarPinos()
    {
        yield return new WaitForSeconds(2);
        if (OnReadyToCheckPinsHited != null)
            OnReadyToCheckPinsHited();
    }
    private IEnumerator EsperarPinosCairem()
    {
        //Espera dois segundos para comecar a checar se os pinos já pararam de se mover
        yield return new WaitForSeconds(0.65f);

        PlayAbaixarProtetor((AnimatorStateInfo stateInfo, string animationName)=> 
        {
            waitingForPinsToFall = true;

            if(OnWaitingForPinsToFall!=null)
            {
                OnWaitingForPinsToFall();
            }
        });
        
    }
    private void ContadorPinos_PinoCaiu(Pino pino)
    {
        if (!atLastOnePinHited)
        {
            atLastOnePinHited = true;
            if (OnBallHitPins != null) { OnBallHitPins(); }
        }
    }

    private void Update()
    {
        ChecaSeOsPinosAindaSeMovem();
    }

    private void ChecaSeOsPinosAindaSeMovem()
    {
        if (waitingForPinsToFall)
        {
            float deltaTime = Time.deltaTime;

            //Adiciona em todos os pinos uma quantidade para que eles parem vagarosamente de se mover
            ListaDePinosEmPe().ForEach(pinoQueFicou => pinoQueFicou.ProgressivePhysicsSleep(deltaTime * 0.005f));

            //Checa se todos os pinos já pararam de mover
            if (ListaDePinosEmPe().All(currentPino => !currentPino.IsMoving()) || ListaDePinosEmPe().Count == 0)
            {
                waitingForPinsToFall = false;
                StartCoroutine(PodeContarPinos());
            }
        }
    }
    private void PinoBoxTrigger_BolaPassou(GameObject bola)
    {
        // Debug.Log("Pinos Manager Bola Passou No Coliider");
        atLastOnePinHited = false;

        foreach (Pino pino in instantiatedPinosList)
        {
            pino.SetPseudoStatic(false);
        }

        StartCoroutine(EsperarPinosCairem());
    }
    /*private void GerarPosicaoOriginalPinos(Vector3 rayCastOriginTransform, Pino pino)
    {
        RaycastHit hit;
        Ray newRay = new Ray(rayCastOriginTransform, Vector3.down);
        Physics.Raycast(newRay, out hit);
        pino.defaultPosition = hit.point;
    }*/
}

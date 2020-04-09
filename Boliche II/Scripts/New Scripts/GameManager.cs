using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class StateMachine:MonoBehaviour
{
    private IState currentState;
    public IState CurrentState => currentState;
    public Dictionary<string, IState> stateDicionary;

    public IState[] GetAllStates()
    {
        IState[] statesArray = stateDicionary.Values.ToArray();
        if (statesArray == null) { Debug.Log($"Nenhum State Adicionado No dicionario"); }
        return statesArray;
    }
    public IState GetState(string stateName)
    {
        IState state = stateDicionary[stateName];
        if (state == null) { Debug.Log($"Nenhum State Encontrado com o nome {stateName}"); }
        return state;
    }
    public IState GetState(int index)
    {
        IState state = stateDicionary.Values.ToArray()[index];
        if (state == null) { Debug.Log($"Nenhum State Encontrado com o index {index}"); }
        return state;
    }

    public void AddStateToList(IState state)
    {
        if (stateDicionary == null)
            stateDicionary = new Dictionary<string, IState>();

        if (string.IsNullOrEmpty(state.Name)) { Debug.LogError("O State Adicionado Não Possui Nome Definido");  return; }

        stateDicionary.Add(state.Name, state);
    }

    public void SetCurrentState(IState newState)
    {
        if (currentState != null) { currentState.Exit(); }
        currentState = newState;
        currentState.Enter();
    }
    public void SetCurrentState(string newStateName)
    {
        SetCurrentState(GetState(newStateName));
    }
    public void SetCurrentState(int index)
    {
        SetCurrentState(GetState(index));
    }

    protected void UpdateCurrentState()
    {
        if(currentState != null)
            currentState.Update();
    }
}
public interface IState
{
     string Name { get;}
     void Enter();
     void Update();
     void Exit();
}


public class GameManager : MonoBehaviour
{
    public LancadorBoliche lancador;
    public ColisoresManager colisoresManager;
    public PinosManagerBoliche pinosManager;
    public PlacarUI placarUIPlayer1,placarUIPlayer2;
    public ResultsUI resultsUI;
    public CameraSwitcher cameraSwitcher;


    private bool podeArremacar = false;
    private bool jogadaInvalida = false;
    private bool endGame = false;

    private List<Jogador> jogadoresList;
    private Jogador jogadorAtual;
    private PlacarUI placarAtual;
    private void Start()
    {
        if(StaticMembers.jogadores == null)
        {
            jogadoresList = new List<Jogador>();
            jogadoresList.Add(new Jogador("JogadorTest1"));
            jogadoresList.Add(new Jogador("JogadorTest2"));
            //jogadoresList[0].Pontuacao.SetFrame(8);
            //jogadoresList[1].Pontuacao.SetFrame(8);
        }
        else
        {
            jogadoresList = StaticMembers.jogadores;
        }
        
        jogadorAtual = jogadoresList[0];
        placarUIPlayer1.SetPlayerName(jogadoresList[0]);
        placarUIPlayer2.SetPlayerName(jogadoresList[1]);
        placarAtual = placarUIPlayer1;
        placarAtual.Visibilty(true,0);


        jogadoresList.ForEach(jogador=>jogador.Pontuacao.OnRecebeuBonusEvent += Pontuacao_OnRecebeuBonusEvent);

        colisoresManager.OnBallFallOut += ColisoresManager_OnBallFallOut;

        lancador.OnBolaChegouAoFinalDaPistaEvent += Lancador_OnBolaChegouAoFinalDaPistaEvent;

        //Definindo os States
        lancador.AddStateToList(new LancadorBoliche.LancadorStartState(lancador,"start"));
        lancador.AddStateToList(new LancadorBoliche.LancadorWaitingState(lancador, "waiting"));
        lancador.AddStateToList(new LancadorBoliche.LancadorLauchedState(lancador,"launched"));
        lancador.SetCurrentState("start");

        pinosManager.OnBallHitPins += PinosManager_OnBallHitPins;
        pinosManager.OnReadyToCheckPinsHited += PinosManager_OnReadyToCheckPinsHited;
        pinosManager.OnClearedTrackWithNewPins += PinosManager_OnClearedTrackWithNewPins;
        pinosManager.OnClearedTrack += PinosManager_OnClearedTrack;
        pinosManager.OnPinosColocados += PinosManager_OnPinsPlaced;
        pinosManager.OnWaitingForPinsToFall += PinosManager_OnWaitingForPinsToFall;
        pinosManager.CriarTodosPinos();
        pinosManager.ColocarPinos(pinosManager.AllPinos);
        placarUIPlayer1.PlacarSize(0.5f);
        placarUIPlayer2.PlacarSize(0.5f);

     /*   yield return new WaitForSeconds(1.5f);
        resultsUI.ShowResultsPlaceholder();*/

    }
    private void SwitchPlayer()
    {
        placarAtual.Visibilty(false);

        if (jogadorAtual.Nome == jogadoresList[0].Nome)
        {
            jogadorAtual = jogadoresList[1];
            placarAtual = placarUIPlayer2;
           
        }           
        else
        {
            jogadorAtual = jogadoresList[0];
            placarAtual = placarUIPlayer1;
        }

        placarAtual.Visibilty(true);
    }
    private void PinosManager_OnWaitingForPinsToFall()
    {
        placarAtual.PlacarSize(1f);
    }

    private void Pontuacao_OnRecebeuBonusEvent(FrameBoliche frameQueRecebeuBonus)
    {
        placarAtual.UpdateResult(frameQueRecebeuBonus,jogadorAtual);
      //  placarAtual.UpdateTotalPoints(jogadorAtual.Pontuacao);
    }

    private void Lancador_OnBolaChegouAoFinalDaPistaEvent()
    {
        if(jogadaInvalida && !endGame)
        {
            RotinaLancamento();
        }
    }

    private void RotinaLancamento()
    {
        int numeroDePinosDerrubados = pinosManager.contadorPinos.NumeroDePinosDerrubados;

        FrameBoliche currentFrame = jogadorAtual.Pontuacao.FrameAtual;

        StaticMembers.TiposDeJogadas tipoJogadaAtual = currentFrame.ReceberPinosDerrubados(numeroDePinosDerrubados);

        bool rodadaAcabou = false;

        switch (tipoJogadaAtual)
        {
            case StaticMembers.TiposDeJogadas.Normal:
                GameManager_OnNormal();
                break;
            case StaticMembers.TiposDeJogadas.Strike:
                GameManager_OnStrike();
                break;
            case StaticMembers.TiposDeJogadas.Spare:
                GameManager_OnSpare();
                break;
            default:
                break;
        }

        if (currentFrame.GetType() == typeof(FrameBolicheUltimo))
        {
            Debug.Log("Ultimo Frame");

            rodadaAcabou = currentFrame.VerificarSeRodadaAcabou();
            jogadorAtual.Pontuacao.CalcularBonus(numeroDePinosDerrubados);  
            if (rodadaAcabou)
            {
                if (IsEndedGame())
                {
                    pinosManager.PlayAbaixarProtetor((AnimatorStateInfo stateInfo, string animaName) =>
                    {
                        pinosManager.PlayProtetorLimparIn((AnimatorStateInfo stateInfo2, string animaName2) =>
                        {
                            pinosManager.PlayProtetorLimparOut();
                        });
                    });

                    placarAtual.UpdateResult(currentFrame, jogadorAtual);
                    endGame = true;
                    StartCoroutine(EndGame());
                }
                else
                {
                    pinosManager.LimparPistaEColocarTodosOsPinos();
                }        
            }
            else
            {
                if (tipoJogadaAtual != StaticMembers.TiposDeJogadas.Normal)
                {
                    pinosManager.LimparPistaEColocarTodosOsPinos();
                }
                else
                {
                    pinosManager.LimparPistaEColocarPinosQueFicaram();
                }
            }
        }
        else
        {
            rodadaAcabou = currentFrame.VerificarSeRodadaAcabou();
            jogadorAtual.Pontuacao.CalcularBonus(numeroDePinosDerrubados);
            if (rodadaAcabou)
            {
                //Adicona o frame atual a lista de frames que foram finalizados
                jogadorAtual.Pontuacao.AdicionarFrameFinalizado(currentFrame);

                //Define o proximo frame a ser utilizado
                jogadorAtual.Pontuacao.SetFrame(jogadorAtual.Pontuacao.CurrentFrameIndex + 1);    

                //Limpa a pista e adiciona todos os pinos novamente
                pinosManager.LimparPistaEColocarTodosOsPinos();

                Debug.Log("Acabou Rodada, Passando para o próximo frame...");
            }
            else
            {
                //Limpa a pista e recoloca os pinos que não foram derrubados
                pinosManager.LimparPistaEColocarPinosQueFicaram();
            }                
        }

        //Atualiza o placar do jogo
        if (endGame)
            return;
        placarAtual.UpdateResult(currentFrame,jogadorAtual);
       // placarAtual.UpdateTotalPoints(jogadorAtual.Pontuacao);

        //Passa para a proxima jogada
        currentFrame.jogadaAtual++;

        if (rodadaAcabou)
            SwitchPlayer();
    }

    private IEnumerator EndGame()
    {
        Debug.Log("End Game");
        yield return new WaitForSeconds(2);
        placarUIPlayer1.Visibilty(false, 0.1f);
        placarUIPlayer2.Visibilty(false, 0.1f);
        resultsUI.SetResults(jogadoresList[0],jogadoresList[1]);
    }

    private void PinosManager_OnPinsPlaced()
    {
        Debug.Log("Pinos Colocados!");
        podeArremacar = true;
        jogadaInvalida = false;
        lancador.SetCurrentState("waiting");
        pinosManager.contadorPinos.ResetContador();
        placarAtual.ShowCurrentFrame(jogadorAtual.Pontuacao.CurrentFrameIndex);
    }

    private void PinosManager_OnClearedTrack()
    {
        cameraSwitcher.Switch(0);
    }

    private void PinosManager_OnClearedTrackWithNewPins()
    {
        // Debug.Log("PRONTO PARA NOVO ARREMESSO");
        placarAtual.PlacarSize(0.5f);

    }

    private void PinosManager_OnReadyToCheckPinsHited()
    {
        if (!endGame)
        {
            RotinaLancamento();
        }
    }

    private void PinosManager_OnBallHitPins()
    {
        // Debug.Log("ACERTOU OS PINOS");
    }

    private void ColisoresManager_OnBallFallOut()
    {
        Debug.Log("JOGADA INVALIDA");
        
        if(!jogadaInvalida)
        {
            jogadaInvalida = true;
            cameraSwitcher.Switch(1);
        }   
    }

    private void GameManager_OnStrike()
    {
        cameraSwitcher.Switch(2);

    }
    private void GameManager_OnSpare()
    {
        cameraSwitcher.Switch(2);

    }
    private void GameManager_OnNormal()
    {
        cameraSwitcher.Switch(2);
    }

    private bool IsEndedGame()
    {
        return jogadoresList.All(jogador => jogador.IsEndedGame());
    }

    private void Update()
    {
        if (podeArremacar && Input.GetKeyDown(KeyCode.Space))
        {
            podeArremacar = false;
            cameraSwitcher.Switch(1);
            lancador.SetCurrentState("launched");
        }
    }
}

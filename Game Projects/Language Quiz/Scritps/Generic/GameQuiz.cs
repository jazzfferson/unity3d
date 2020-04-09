using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameQuiz : MonoBehaviour
{

    #region Variables
    public Color timeOutCameraColor;

	public Transform comboPosition;
    public Color rightColorButton,wrongColorButton;
    public static GameQuiz instance;
    public UISlider lifeBar;

    public UI2DSprite[] socketsAnswered;
    public Sprite socketEmpty, socketCorrect, socketIncorrect;

    public UILabel[] questionLabels;
    public UISprite[] questionSprites;
    public UI2DSprite picture;

     List<int> _indexsEscolhidos;
     int _indexRightQuestion;

    float _timeBarSpeed;
    bool _endGame;

    #endregion

    #region EndLessVariables

    public float comboValidateTime;

    public int scoreRightQuestion;
    public int scoreWrongQuestion;

	int _rightComboQuestions;
    int _rightQuestionsAnswered;
    int _combos;

    float _timeEarned;
    float _timeLosted;
    const float _second = 0.016666f;
    float _comboValidateTime;

    bool hasStartedTimeOut;

   


    public GameObject pivoLifeBar;
    public GameObject pivoScore;



    #endregion

    #region LearnVariables

    int visualQuestionsAnsweredIndex;
    int rodadas;

    public GameObject pivoSockets;

    #endregion

    #region EndLess Statistics

    [HideInInspector]
    public int statistics_actualScore,
    statistics_maxCombo,
    statistics_words, statistics_corrects, 
    statistics_incorrects;

    [HideInInspector]
    public float statistics_time;



    #endregion

    void Start()
    {

        if (instance == null) { instance = this; }
        _indexsEscolhidos = new List<int>();

    }

    void Update()
    {

        if (Menus.instance.menuAtual == EMenu.Ingame)
        {
            TimeBar();
            Timer();
            ValidateCombo();
        }
    }

    #region Criar Questaos

    void SetNewQuestion(Language language)
    {

        //Reseta a lista de indexs já escolhidos quando ela chegar no limite de questoes;
        if (_indexsEscolhidos.Count >= 100/*AllWords.GetAllWords().GetLength(1)*/)
        {
            _indexsEscolhidos = new List<int>();
        }



        int index;


    Initi:
        {
            //Escolhe um index de 0 ao numero maximo de questoes
            index = Random.Range(0, 100 /*AllWords.GetAllWords().GetLength(1)*/);
            

            //Se o numero de index escolhidos for maior que zero
            //É feita uma checagem para nao repedir os indexs até
            //O numero de questoes terminar.

            if (_indexsEscolhidos.Count > 0)
            {
                for (int i = 0; i < _indexsEscolhidos.Count; i++)
                {
                    if (index == _indexsEscolhidos[i])
                    {
                        goto Initi;
                    }

                }

                _indexsEscolhidos.Add(index);
            }
            else
            {
                _indexsEscolhidos.Add(index);
            }

        }

        //Baseado na linguagem escolhida pelo jogador, escolhe-se uma questao.
        string respostaCerta = AllWords.GetSpecifiedWord(language, index);

        //Baseado na questao escolhida, seta-se a imagem equivalente.
        picture.sprite2D = Resources.Load<Sprite>("Textures/Objetos/"+index.ToString());

        //Gera-se um numero aleatorio para pegar a questao certa e 
        //Coloca-la em uma posicao aleatoria das 4 opçoes.
        int indexQuestaoCerta = Random.Range(0, 4);
        _indexRightQuestion = indexQuestaoCerta;

        //index de iteracao das respostas erradas.
        int indexQuestaoErrada = 0;

        //Gera-se 3 questoes erradas que não se repetem entre elas
        //E nao se repetem com a resposta certa.
        string[] respostasErradas = new string[3];
        respostasErradas = WrongQuestions(respostaCerta, language);

        //Aplica o texto da resposta certa no indice aleatorio gerado.
        questionLabels[indexQuestaoCerta].text = respostaCerta;

        //Coloca as questoes erradas nos espacos de opçao restantes.
        for (int i = 0; i < 4; i++)
        {
            if (i != indexQuestaoCerta)
            {
                questionLabels[i].text = respostasErradas[indexQuestaoErrada];
                indexQuestaoErrada++;
            }
        }
    }

    string[] WrongQuestions(string RightQuestion, Language language)
    {
        string[] questoesErradas = new string[3];

    GerarQuestao:
        {

            int length = 101/*AllWords.GetAllWords().GetLength(1)*/;
            //Gerando respostas erradas Aleatoriamente.
            questoesErradas[0] = AllWords.GetSpecifiedWord(language, Random.Range(0, length-1));
            questoesErradas[1] = AllWords.GetSpecifiedWord(language, Random.Range(0, length-1));
            questoesErradas[2] = AllWords.GetSpecifiedWord(language, Random.Range(0, length-1));


            //Checa se as questoes erradas geradas acima sao iguais entre elas
            //E a questao correta. Se forem ele gera aleatoriamente de novo até
            //Serem todas diferentes entre elas.
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (j != i)
                    {
                        if (questoesErradas[i] == questoesErradas[j] || questoesErradas[i] == RightQuestion)
                        {
                            goto GerarQuestao;
                        }
                    }
                }
        }
        return questoesErradas;
    }

    #endregion

    #region Botoes das questoes

    void Question1()
    {
        
        CheckRightQuestion(_indexRightQuestion, 0);
    }
    void Question2()
    {

        CheckRightQuestion(_indexRightQuestion, 1);
    }
    void Question3()
    {

        CheckRightQuestion(_indexRightQuestion, 2);
    }
    void Question4()
    {

        CheckRightQuestion(_indexRightQuestion, 3);
    }

    #endregion

    public void StartGame()
    {
        #region Switch Case Mode Game

        switch (Proprietes.Mode)
        {
            case Mode.Pratice:

                _timeBarSpeed = 0.4f;
                _timeEarned = 1;
                rodadas = socketsAnswered.Length;
                _endGame = false;
                lifeBar.value = 1;
                _rightQuestionsAnswered = 0;
                pivoLifeBar.SetActive(false);
                pivoSockets.SetActive(true);
                pivoScore.SetActive(false);
            

                break;

            case Mode.Endless:

                rodadas = 0;
                _timeBarSpeed = _second;
                _timeEarned = _second;
                _timeLosted = _second * 3;
                _endGame = false;
                lifeBar.value = 1;

                pivoLifeBar.SetActive(true);
                pivoSockets.SetActive(false);
                pivoScore.SetActive(true);
                break;
        }

        #endregion

        
        statistics_corrects = 0;
        statistics_incorrects = 0;
        statistics_maxCombo = 0;
        statistics_words = 0;
        statistics_actualScore = 0;
        statistics_time = 0;

        SetNewQuestion(Proprietes.Language);
        ResetVisualQuestionsAnswered();
        Scores.instance.SetScore(0);
    }

    void RequestNewQuestion()
    {
        SetNewQuestion(Proprietes.Language);
    }

    void CheckRightQuestion(int rightQuestionIndex, int selectIndex)
    {
        if (_endGame)
            return;

        if (rightQuestionIndex == selectIndex)
        {
            
			SetButtonColor(selectIndex, rightColorButton);
			Right();
        }
        else
        {
            
			SetButtonColor(selectIndex, wrongColorButton);
			Wrong();
        }
    }

    void CheckEndGame(int rodadaAtual)
    {
        if (rodadaAtual < 1)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        

        switch (Proprietes.Mode)
        {
            case Mode.Pratice:
                Menus.instance.ChangeMenu(EMenu.PraticeStatistc);
                StatisticsPratice.instance.ShowResults(_rightQuestionsAnswered);
                break;

            case Mode.Endless:

                Menus.instance.ChangeMenu(EMenu.EndlessStatistic);

                if (Proprietes.HighScore < statistics_actualScore)
                {
                    Proprietes.HighScore = statistics_actualScore;
                    Scores.instance.SetHighScore(statistics_actualScore);
                }

                StatisticsEndLess.instance.timeValue = (int)statistics_time;
                StatisticsEndLess.instance.scoreValue = statistics_actualScore;
                StatisticsEndLess.instance.highScoreValue = Proprietes.HighScore;
                StatisticsEndLess.instance.correctsValue = statistics_corrects;
                StatisticsEndLess.instance.incorrectsValue = statistics_incorrects;
                StatisticsEndLess.instance.maxComboValue = statistics_maxCombo;
                StatisticsEndLess.instance.wordsValue = statistics_words;

                CameraColor.instance.SetDefault();
              
                break;
        }

        _endGame = true;
        hasStartedTimeOut = false;
    }

    void Right()
    {

        Sfx_CorrectQuestion1();

        _rightQuestionsAnswered++;

		statistics_corrects++;
        
		_rightComboQuestions++;

        switch (Proprietes.Mode)
        {
            case Mode.Endless:

                lifeBar.value += _timeEarned;

			int multiplier;
            int finalValue;

			if(_rightComboQuestions>0)
				multiplier = _rightComboQuestions;
			else
				multiplier = 1;

            finalValue = scoreRightQuestion * multiplier;
            statistics_actualScore += finalValue;

            if (_rightComboQuestions > 1 && _comboValidateTime > 0)
            {

                Sfx_Combo();

                Instanciador.instancia.Instanciar(0, comboPosition.position, Quaternion.identity)
                    .GetComponent<Combo>().Initialize(_rightComboQuestions);
                //Points.instance.ShowPoints(finalValue);

                _combos++;

                if (_combos>statistics_maxCombo)
                {
                    statistics_maxCombo = _combos;
                }
            }
            else if (_comboValidateTime < 0)
            {
                _rightComboQuestions = 0;
                _combos = 0;
            }

            _comboValidateTime = 3;

                break;




            case Mode.Pratice:
                lifeBar.value = 1;
                SetVisualQuestionsAnswered(true);
                rodadas--;
                CheckEndGame(rodadas);
                break;
        }

		PostAnswer ();

    }

    void Wrong()
    {



		_rightComboQuestions = 0;

        Sfx_IncorrectQuestion1();

		statistics_incorrects++;

        switch (Proprietes.Mode)
        {
            case Mode.Endless:
                lifeBar.value -= _timeLosted;
                statistics_actualScore += scoreWrongQuestion;
               // Points.instance.ShowPoints(scoreWrongQuestion);
                break;

            case Mode.Pratice:

                SetVisualQuestionsAnswered(false);
                rodadas--;
                CheckEndGame(rodadas);
                break;
        }

		PostAnswer ();
    }

	void PostAnswer()
	{
        if (statistics_actualScore < 0)
        {
            statistics_actualScore = 0;
        }
        statistics_words++;
		RequestNewQuestion();
		Scores.instance.SetScore(statistics_actualScore);
	}

    void TimeBar()
    {
        if (_endGame || Proprietes.Mode == Mode.Pratice)
            return;

        lifeBar.value -= _timeBarSpeed * Time.deltaTime;

        if (lifeBar.value <= _second * 10 && !hasStartedTimeOut)
        {
            hasStartedTimeOut = true;
            CameraColor.instance.SetColor(0.4f, GoEaseType.CubicInOut, timeOutCameraColor);
        }
        else if (lifeBar.value <= 0 && !_endGame ) 
        {
            EndGame();
        }
    }

    void NextQuestion()
    {
        switch (Proprietes.Mode)
        {
            case Mode.Endless:

                EndGame();

                break;

               

            case Mode.Pratice:

                rodadas--;
                CheckEndGame(rodadas);
                SetVisualQuestionsAnswered(false);
                RequestNewQuestion();
                lifeBar.value = 1;
             
                break;
        }
    }

    void SetVisualQuestionsAnswered(bool isRight)
    {
        Sprite socketSprite;

        if (isRight)
        {
            socketSprite = socketCorrect;
        }
        else
        {
            socketSprite = socketIncorrect;
        }


        socketsAnswered[visualQuestionsAnsweredIndex].sprite2D = socketSprite;

        visualQuestionsAnsweredIndex = Mathf.Clamp(visualQuestionsAnsweredIndex + 1, 0, socketsAnswered.Length - 1);
    }

    void ResetVisualQuestionsAnswered()
    {
        foreach (UI2DSprite sprite in socketsAnswered)
        {
            sprite.sprite2D = socketEmpty;
        }

        visualQuestionsAnsweredIndex = 0;
    }

    void SetButtonColor(int index , Color cor)
    {
        questionSprites[index].color = cor;
        TweenColor.Begin(questionSprites[index].gameObject, 0.1f, Color.white).delay = 0.2f;
    }

    void ValidateCombo()
    {
        _comboValidateTime -= Time.deltaTime;
    }

    #region Sons

    void Sfx_CorrectQuestion1()
    {
        Instanciador.instancia.PlaySfx(0, 1, 1);
    }

    void Sfx_IncorrectQuestion1()
    {
        Instanciador.instancia.PlaySfx(1, 1, 1);
    }

    void Sfx_Combo()
    {
        Instanciador.instancia.PlaySfx(2, 1, 1);
    }

    #endregion

    #region Statistics

    void Timer()
    {
        statistics_time += Time.deltaTime;
    }

    #endregion


}

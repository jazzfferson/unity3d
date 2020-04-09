using UnityEngine;
using System.Collections;

public class ResultsPanel : MonoBehaviour {

    [SerializeField]
    private UILabel result;
    [SerializeField]
    private UILabel distance;
    [SerializeField]
    private UILabel time;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private float timeTransition;
    [SerializeField]
    private float delay;

    [SerializeField]
    private UISprite nextLevelBackground;
    [SerializeField]
    private UILabel nextLevelLabel;
    [SerializeField]
    private Collider nextLevelCollider;

    private Vector3 originalPosition;

	bool canClick = false;

    public void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void ShowResults(float _distance,float _time,bool _failed)
    {
		if(MainCode.instance.estadoJogo== EstadoJogo.Jogo)
		{
       		StartCoroutine(ShowResultsRotina(_distance, _time, _failed));
			MainCode.instance.estadoJogo = EstadoJogo.Resultado;
		}
		if(InfoFases.FaseAtual==5)
		{
			nextLevelBackground.alpha = 0;
			nextLevelLabel.alpha = 0;
			nextLevelCollider.enabled =false;
		}
		else
		{
			nextLevelBackground.alpha = 1;
			nextLevelLabel.alpha = 1;
		}
    }
    public void HideResults()
    {
		MainCode.instance.estadoJogo = EstadoJogo.Jogo;
        Go.to(transform, timeTransition, new GoTweenConfig().localPosition(originalPosition, false).setEaseType(GoEaseType.CubicOut).setDelay(0.1f));
    }

    void Close()
    {
		if(!canClick)
			return;
		
		MainCode.instance.estadoJogo = EstadoJogo.Jogo;
        if (OnClose != null)
        {
            OnClose();
        }
		
		canClick = false;
    }
    void NextLevel()
    {
		if(!canClick)
			return;
		
		MainCode.instance.estadoJogo = EstadoJogo.Jogo;
        if (OnNextLevel != null)
        {
            OnNextLevel();
        }
		canClick = false;
    }
    void Restart()
    {
		if(!canClick)
			return;
		
		MainCode.instance.estadoJogo = EstadoJogo.Jogo;
        if (OnRestart != null)
        {
            OnRestart();
        }
		canClick = false;
    }

    IEnumerator ShowResultsRotina(float _distance, float _time, bool _failed)
    {

        yield return new WaitForSeconds(delay);

        Go.to(transform, timeTransition, new GoTweenConfig().localPosition(position, false).setEaseType(GoEaseType.CubicOut));

        #region failedResult
        if (_failed)
        {

            nextLevelCollider.enabled = false;
			
			if(InfoFases.FaseAtual!=5)
			{
            	Go.to(transform, 0.2f, new GoTweenConfig().shake(new Vector3(1, 1, 0), GoShakeType.Position,
				1, false).setDelay(timeTransition + 0.1f).onComplete(complete => { 
					nextLevelBackground.alpha = 0.1f; nextLevelLabel.alpha = 0.1f;}));
			}
			else
			{
				Go.to(transform, 0.2f, new GoTweenConfig().shake(new Vector3(1, 1, 0), GoShakeType.Position,
				1, false).setDelay(timeTransition + 0.1f));
			}
            result.color = Color.red;
            distance.color = Color.red;
            time.color = Color.red;
            result.text = "FAILED";
        }
        else
        {
			if(InfoFases.FaseAtual!=5)
			{
				nextLevelBackground.alpha = 1f;
            	nextLevelLabel.alpha = 1f;
            	nextLevelCollider.enabled = true;
			}
			
           
            result.color = Color.green;
            distance.color = Color.green;
            time.color = Color.green;
            result.text = "LANDED";
        }
        #endregion

        distance.text = ((int)_distance).ToString();
        time.text = string.Format("{0:0.0}", _time);
		
		yield return new WaitForSeconds(timeTransition);
		canClick = true;
    }

    public delegate void ResultsEventHandler();
    public event ResultsEventHandler OnClose;
    public event ResultsEventHandler OnNextLevel;
    public event ResultsEventHandler OnRestart;
	
}

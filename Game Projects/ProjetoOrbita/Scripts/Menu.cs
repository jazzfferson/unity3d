using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    [SerializeField]
    private Transform menu;
    [SerializeField]
    private float tempoTransicao;
    [SerializeField]
    private Vector3 posicaoFinal;
    private Vector3 posicaoInicial;

    [SerializeField]
    private Collider[] BotoesCollider;
    [SerializeField]
    private UISprite[] BotoesBackground;
    [SerializeField]
    private UILabel[] BotoesLabel;
    [SerializeField]
    private Color habilitado;
    [SerializeField]
    private Color desabilitado;
	
	bool menuprincipal = true;

	void Start () {

        posicaoInicial = menu.position;
		
        InfoFases.Initialize();
		InfoFases.Load();
        
	}
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote))
		{	
			if(menuprincipal)
			{
				Application.Quit();
			}
			else
			{
				Return();
			}
		}
			
	
	}
    public void Play()
    {
		menuprincipal = false;
        Go.to(menu.transform, tempoTransicao, new GoTweenConfig().localPosition(posicaoFinal, false).setEaseType(GoEaseType.CubicInOut).onComplete(Complete=>CheckFasesHabilitadas()));
    }
    void Return()
    {
		menuprincipal =true;
        Go.to(menu.transform, tempoTransicao, new GoTweenConfig().localPosition(posicaoInicial, false).setEaseType(GoEaseType.CubicInOut).onComplete(Complete=>ApagarVisualCasas()));
    }
    void Fase(GameObject button)
    {
        string name = button.name;
        InfoFases.FaseAtual = int.Parse(name);
        Application.LoadLevel("Jogo");
    }
    void CheckFasesHabilitadas()
    {
		
        ApagarVisualCasas();
        
        for (int i = 0; i < InfoFases.FasesHabilitadas; i++)
        {
            BotoesCollider[i].enabled = true;
            BotoesBackground[i].color = habilitado;
            BotoesLabel[i].color = new Color(1, 1, 1, 1f);
        }
    }
	void ApagarVisualCasas()
	{
		for (int i = 0; i < BotoesCollider.Length; i++)
        {
            BotoesCollider[i].enabled = false;
            BotoesBackground[i].color = desabilitado;
            BotoesLabel[i].color = new Color(1, 1, 1, 0.1f);
        }
	}
}

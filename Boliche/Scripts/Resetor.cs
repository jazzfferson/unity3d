using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Resetor : MonoBehaviour {

	// Use this for initialization
	public GameObject colocadorPinos;
	public GameObject formacaoPinos;
    public GameObject contadorPinos;
	GameObject pinosRef;
	
	bool terminouDescerBarreira = true;
	bool terminouDepor = true;
	
	List<GameObject> pinosQueVoltarao;

	
	void Start () {

        pinosQueVoltarao = new List<GameObject>(10);
        pinosQueVoltarao = formacaoPinos.GetComponent<FormacaoPinos>().listaPinos;
        contadorPinos.GetComponent<ContadorPinos>().PinoCaiu += new ContadorPinos.EventHandler(Resetor_PinoCaiu);
   
	}
	
	void Update () {
	
	}
	
	public void Barreira()
	{

		if(terminouDescerBarreira)
		{
			terminouDescerBarreira=false;

            iTween.MoveTo(gameObject, iTween.Hash("y", gameObject.transform.position.y - 8f, "time", 1f, "looptype", iTween.LoopType.none, "oncomplete", "BarreiraTween1Complete", "easetype", iTween.EaseType.easeInOutQuad));
		
			iTween.MoveTo(gameObject,iTween.Hash("z",gameObject.transform.position.z+20 , "delay",4f,"time", 2f, "looptype", iTween.LoopType.none, "easetype", iTween.EaseType.easeInOutCubic));

            iTween.MoveTo(gameObject, iTween.Hash("z", gameObject.transform.position.z, "delay", 6f, "time", 2f, "looptype", iTween.LoopType.none, "easetype", iTween.EaseType.easeInOutCubic));
		
			iTween.MoveTo(gameObject,iTween.Hash("y",gameObject.transform.position.y , "delay",10f,"time", 2.2f, "looptype",iTween.LoopType.none, "easetype", iTween.EaseType.easeInOutQuad));
			
		}
		
	}
    void BarreiraTween1Complete()
	{
		terminouDescerBarreira=true;
        PinosEmPe();
        Colocador();
	}
	
	void Colocador()
	{
		
		if(terminouDepor)
		{
			iTween.MoveTo(colocadorPinos.gameObject,iTween.Hash("y",colocadorPinos.gameObject.transform.position.y - 7 ,"delay",0.5f, "time", 1.5f, "looptype", iTween.LoopType.none,"oncomplete","ColocadorTween1Complete","onCompleteTarget",this.gameObject,"easetype", iTween.EaseType.easeInOutQuad));
			iTween.MoveTo(colocadorPinos.gameObject,iTween.Hash("y",colocadorPinos.gameObject.transform.position.y  ,"delay",2.5f, "time", 2.3f, "looptype", iTween.LoopType.none,"easetype", iTween.EaseType.easeInOutQuad));
            iTween.MoveTo(colocadorPinos.gameObject, iTween.Hash("y", colocadorPinos.gameObject.transform.position.y - 7f, "delay", 6f, "time", 2f, "looptype", iTween.LoopType.none, "oncomplete", "ColocadorTween3Complete", "onCompleteTarget", this.gameObject, "easetype", iTween.EaseType.easeInOutQuad));
            iTween.MoveTo(colocadorPinos.gameObject, iTween.Hash("y", colocadorPinos.gameObject.transform.position.y, "delay", 8.5f, "time", 2f, "looptype", iTween.LoopType.none, "easetype", iTween.EaseType.easeInOutQuad));

		}
	}
    void ColocadorTween1Complete()
    {
        terminouDepor = true;
        //Checa os pinos que estão em pé e coloca eles em hierarquia com o "colocador de pinos" para que 
        //Eles sigam a animação
        foreach (GameObject Pino in pinosQueVoltarao)
        {
            if (!Pino.gameObject.GetComponent<Pino>().derrubado)
            {
                Pino.transform.parent = colocadorPinos.transform;
            }
        }
    }
    void ColocadorTween3Complete()
	{
        foreach (GameObject Pino in pinosQueVoltarao)
        {
            Pino.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            Pino.transform.parent = null;
            Pino.GetComponent<Pino>().DesligarFisica(false);
            Pino.gameObject.GetComponent<Pino>().derrubado = false;
        }
	}

    //Evento chamado toda vez que um pino cai
    void Resetor_PinoCaiu(GameObject pino)
    {
        pino.gameObject.GetComponent<Pino>().derrubado = true;
    }
    void PinosEmPe()
    {
        //Checa os pinos que estão em pé e tira a física deles
        foreach (GameObject Pino in pinosQueVoltarao)
        {
            if (!Pino.gameObject.GetComponent<Pino>().derrubado)
            {
                Pino.GetComponent<Pino>().DesligarFisica(true);
            }
        }
    }
}

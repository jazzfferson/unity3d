using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

    public delegate void EventoHit(Hit hitClass);
    public event  EventoHit OnKillEt;
	bool isMouse = true;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	    if(isMouse && Input.GetMouseButtonDown(0))
		{
			  Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
			
                if (Physics.Raycast(cursorRay, out hit))
                {
                    if (hit.collider.name == "Lango(Clone)")
                    {
                        hit.collider.gameObject.GetComponent<AlienPequeno>().Acertou();
                    }
                }
		}
		
        if (Input.touchCount > 0 && !isMouse)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Ray cursorRay = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                RaycastHit hit;
                if (Physics.Raycast(cursorRay, out hit) && Input.touches[0].phase != TouchPhase.Moved)
                {
                    if (hit.collider.name == "Alien_Small(Clone)")
                    {
                        hit.collider.gameObject.GetComponent<AlienPequeno>().Acertou();
                    }
                }
            }
        }
	
	}
}

using UnityEngine;
using System.Collections;

public class HighScoreAnimation : MonoBehaviour {

    public static HighScoreAnimation instance;

    public Transform pivoScore;

    [SerializeField]
    private float speed,positionTargetMax,positionTargetMin;
    [HideInInspector]
    public bool animateScore = true;

	void Start () {

        if (instance == null) { instance = this; }
	
	}
	
	// Update is called once per frame
	void Update () {

        AnimacaoScore(animateScore);
	}

    void AnimacaoScore(bool animate)
    {

        if (!animate)
        {
            pivoScore.transform.position = Vector3.zero;
        }

        else
        {
            if (pivoScore.transform.position.x >= positionTargetMax)
            {
                pivoScore.transform.position = new Vector3(positionTargetMin, 0, 0);
            }

            pivoScore.transform.Translate(speed * Time.smoothDeltaTime, 0, 0);

            
        }
    }

}

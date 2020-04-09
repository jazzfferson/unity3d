using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float forcaAsa;
    public bool vivo = true;
    public Sprite player,lol,megusta,Fuu,notBad;
    public AnimationCurve animCurve;
    public ParticleSystem particle;
    SpriteRenderer rendererSprite;
    int toques;

    	void Start () {

            rendererSprite = GetComponent<SpriteRenderer>();
            rendererSprite.sprite = player;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            
        
	}
	
	// Update is called once per frame
	void Update () {

        if (GameFuu.Playing && vivo)
        {
            if (Input.GetMouseButtonDown(0) && vivo)
            {
                BaterAsa();
            }

            if (vivo)
            {
                Vector3 direcao = GetComponent<Rigidbody2D>().velocity.normalized + new Vector2(8, 0);
                Vector3 posicao = GetComponent<Rigidbody2D>().transform.position;
                Maths2D.LookAtSmooth(GetComponent<Rigidbody2D>().transform, posicao + direcao, 8);
            }
        }
        else if (!GameFuu.Playing && vivo)
        {
           
            float evaluate = Mathf.PingPong(Time.time, 1);
            float altura = animCurve.Evaluate(evaluate)/2;
            GetComponent<Rigidbody2D>().transform.position = new Vector3(GetComponent<Rigidbody2D>().transform.position.x, altura);

            if (Input.GetMouseButtonDown(0))
            {
                GameFuu.Playing = true;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                BaterAsa();
                if (StartGame != null)
                {
                    StartGame();
                }
            }
        }
       
	}

    void BaterAsa()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaAsa));
        GetComponent<AudioSource>().Play();
       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Cenario") && vivo)
        {
            Morrer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Score") && vivo)
        {
            if(GameFuu.Pontos>=GameFuu.BestScore)
            particle.Play();
            Destroy(other.gameObject);
            StartCoroutine(LoLSprite());
            if (Score != null)
            {
                Score();
            }
            
        }
    }

    IEnumerator LoLSprite()
    {

        Go.to(transform, 0.9f, new GoTweenConfig().scale(-1.3f, false).setEaseType(GoEaseType.ElasticOut));
        

        if (GameFuu.Pontos >= 5 && GameFuu.Pontos <= 9) 
        {
            if(Random.Range(0, 2) == 1)
            rendererSprite.sprite = lol;
            else
            rendererSprite.sprite = megusta;
        }
        else if (GameFuu.Pontos >= 9)
        {
            if(Random.Range(0, 4) == 1)
            rendererSprite.sprite = lol;
            else if(Random.Range(0, 4) == 2)
            rendererSprite.sprite = megusta;
            else
            rendererSprite.sprite = notBad;
        }
        else
        {
            rendererSprite.sprite = megusta;
        }
        
        yield return new WaitForSeconds(0.9f);

        if (vivo)
        {
            rendererSprite.sprite = player;
        }

        Go.to(transform, 0.5f, new GoTweenConfig().scale(-1, false).setEaseType(GoEaseType.ElasticOut));
    }

    public delegate void Delegate();
    public event Delegate Score,Morreu,StartGame;

    public void Morrer()
    {
        transform.localScale = new Vector3(-1.3f, -1.3f);
        Go.to(GetComponent<Rigidbody2D>().transform, 0.5f, new GoTweenConfig().shake(new Vector3(0.5f, 0.5f), GoShakeType.Scale));
        GetComponent<Rigidbody2D>().isKinematic = false;
        Instanciador.instancia.PlaySfx(2, 1, 1);
        vivo = false;
        rendererSprite.sprite = Fuu;


        float multi = 80;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(1, 3) * multi, Random.Range(2, 5) * multi));
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().drag = 3.1f;

        Instanciador.instancia.PlaySfx(3, 1, 1, 0.5f);

        if (Morreu != null)
        {
            Morreu();
        }
    }
}

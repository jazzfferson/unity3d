using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

    public AnimationCurve dragCoefficientCurve;

    public AnimationCurve frontDownForceCoefficientCurve;
    public AnimationCurve rearDownForceCoefficientCurve;
    
    public float frontDownForceC,rearDownForceC;

    public Transform[] frontAerofolioPosition;
    public Transform[] rearAerofolioPosition;

	public Transform cg;
	public float mass;
	public float atenuation;
    public bool dinamicCG;
	Vector3 lastVelocity;
	Vector3 acceleration;
	Vector3 cgPosition;

    float frontDownForce;
    float rearDownForce;
    public float drag=1;

    public float Kmh
    {
        get;
        set;
    }
	
	void Start () {
		
		GetComponent<Rigidbody>().centerOfMass = cg.transform.localPosition;
		cgPosition = GetComponent<Rigidbody>().centerOfMass;
		GetComponent<Rigidbody>().mass = mass;
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
        if (dinamicCG)
        {
            CenterOfMass();
        }

        Kmh = Mathf.Clamp((int)Mathf.Abs(GetComponent<Rigidbody>().velocity.magnitude * 3.6f), 0, 10000);

    

        DragCalculation();
	}

    void CenterOfMass()
    {

        Vector3 localVelocityTemp = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity.x , GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        Vector3 localVelocity = new Vector3(-localVelocityTemp.x, localVelocityTemp.y, localVelocityTemp.z);
        acceleration = (localVelocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = localVelocity;

        GetComponent<Rigidbody>().centerOfMass = cgPosition - (acceleration/ atenuation);
        cg.localPosition = GetComponent<Rigidbody>().centerOfMass;
    }

    void DragCalculation()
    {
        GetComponent<Rigidbody>().drag = Mathf.Clamp(dragCoefficientCurve.Evaluate(Kmh) / drag,0.0001f,1);

         frontDownForce = frontDownForceCoefficientCurve.Evaluate(Kmh);
         rearDownForce = rearDownForceCoefficientCurve.Evaluate(Kmh);


        for (int i = 0; i < frontAerofolioPosition.Length; i++)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, -(frontDownForce * frontDownForceC)/ frontAerofolioPosition.Length, 0), 
                frontAerofolioPosition[i].position); 
        }

        for (int i = 0; i < rearAerofolioPosition.Length; i++)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, -(rearDownForce * rearDownForceC) / rearAerofolioPosition.Length, 0),
              rearAerofolioPosition[i].position);
        }

    }

    
}

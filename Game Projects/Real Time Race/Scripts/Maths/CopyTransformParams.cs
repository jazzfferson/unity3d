using UnityEngine;
using System.Collections;

public class CopyTransformParams : MonoBehaviour {

    [SerializeField]
    private Transform target;
    private Transform m_transform;

    public bool copyRotation;
    public bool copyPosition;

    void Awake()
    {
        m_transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        if (copyRotation)
            m_transform.rotation = target.rotation;

        if (copyPosition)
            m_transform.position = target.position;
	
	}
}

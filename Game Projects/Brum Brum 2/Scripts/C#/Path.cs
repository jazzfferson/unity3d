using UnityEngine;
using System.Collections;
using System.Linq;
using System;


public class myMonsterSorter : IComparer
{

    // Calls CaseInsensitiveComparer.Compare on the monster name string.
    int IComparer.Compare(System.Object x, System.Object y)
    {
        return ((new CaseInsensitiveComparer()).Compare(((Transform)x).name, ((Transform)y).name));
    }

}

public class Path : MonoBehaviour {

    
    public float nodeSize=10;
    public Color pathColor = Color.white;

    [SerializeField]
    private Transform[] nodes;
    public Transform[] Nodes
    {
        get { return nodes;}
    }

	void Start () {

      

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDrawGizmos()
    {

        GetNodes();

        Gizmos.color = pathColor;

        for(int i = 0 ; i <nodes.Length; i++)
        {
            Gizmos.DrawLine(nodes[i].position, nodes[Mathf.Clamp(i + 1,0,nodes.Length-1)].position);
            Gizmos.DrawSphere(nodes[i].position, nodeSize);
        }
        
    }

    void GetNodes()
    {
        Transform[] preNode = GetComponentsInChildren<Transform>();
        nodes = new Transform[preNode.Length - 1];

        for (int i = 1; i < preNode.Length; i++)
        {
            nodes[i - 1] = preNode[i].GetComponent<Transform>();
        }

        IComparer myComparer = new myMonsterSorter();
        Array.Sort(nodes, myComparer);
        
       
    }

   
}

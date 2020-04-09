using UnityEngine;
using System.Collections;
using System.Linq;
using System;


public class nodeCompare : IComparer
{

    // Calls CaseInsensitiveComparer.Compare on the monster name string.
    int IComparer.Compare(System.Object x, System.Object y)
    {
        return ((new CaseInsensitiveComparer()).Compare(((PathNode)x).name, ((PathNode)y).name));
    }

}
public static class PathColor
{
    public static Gradient nodeSpeedColor;
}
public class Path : MonoBehaviour {

   

    public Gradient nodeSpeedColor;

    public Color pathColor = Color.white;
    [SerializeField]
    private PathNode[] nodes;
    public PathNode[] Nodes
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
        PathColor.nodeSpeedColor = nodeSpeedColor;

        GetNodes();

        Gizmos.color = pathColor;

        for(int i = 0 ; i <nodes.Length; i++)
        {
            Gizmos.DrawLine(nodes[i].transform.position, nodes[Mathf.Clamp(i + 1, 0, nodes.Length - 1)].transform.position);

            Gizmos.DrawLine(nodes[0].transform.position, nodes[nodes.Length - 1].transform.position);
            
        }
        
    }

    void GetNodes()
    {
        PathNode[] preNode = GetComponentsInChildren<PathNode>();
        nodes = preNode;
     /*   nodes = new PathNode[preNode.Length - 1];

        for (int i = 1; i < preNode.Length; i++)
        {
            nodes[i - 1] = preNode[i].GetComponent<PathNode>();
        }
        */
        IComparer myComparer = new nodeCompare();
        Array.Sort(nodes, myComparer);
        
       
    }

   
}

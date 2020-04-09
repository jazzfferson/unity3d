using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplinePath : MonoBehaviour
{

    public int subdivision;
    public string nodesTagName;
    IEnumerable<Vector3> curvePositions;
    [HideInInspector]
    public List<Vector3> listaNodes;
    // [HideInInspector]
    // public IEnumerable<Vector3> curvePositions;
    public Transform[] nodes;
    public Interpolate.EaseType ease;


    public void BuildNodes()
    {
        listaNodes = new List<Vector3>();
        curvePositions = Interpolate.NewCatmullRom(nodes, subdivision,false);
        foreach (var algo in curvePositions)
        {
            listaNodes.Add((Vector3)algo);
        }

    }

    void OnDrawGizmos()
    {
        if (listaNodes.Count >= 2)
        {
            Vector3 firstNode = listaNodes[0];
            Vector3 start = firstNode;

               for (int i = 1; i < listaNodes.Count; i++)
               {
                   Gizmos.DrawLine(start, listaNodes[i]);
                   start = listaNodes[i];
                   if (start == firstNode) { break; }
               }

           /* foreach (var segmentEnd in listaNodes)
            {
                Gizmos.DrawLine(start, segmentEnd);
                start = segmentEnd;
                // prevent infinite loop, when attribute loop == true
                if (start == firstNode) { break; }
            }*/

        }


    }
}

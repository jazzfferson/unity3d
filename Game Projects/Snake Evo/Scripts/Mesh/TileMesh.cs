using UnityEngine;
using System.Collections;

public class TileMesh : ProceduralMesh
{

    public float size;

    public Vector3[] verticesHexa;
    public Vector3[] verticesQuad;
    public Vector2[] uv;
    public int[] triangles;
    // Use this for initialization
    protected override void Awake()	
    {

         base.Awake();

     verticesHexa = new Vector3[]
     {
         new Vector3( -1, 0, 0) * size,
         new Vector3( -0.5f, 0.86441f, 0) * size,
         new Vector3( -0.5f, -0.86441f, 0)* size,

         new Vector3( 1, 0, 0)* size,
         new Vector3( 0.5f, -0.86441f, 0)* size,
         new Vector3( 0.5f, 0.86441f, 0)* size,


     };

     verticesQuad = new Vector3[]
     {
         new Vector3( -1, 0, 0)* size,
         new Vector3( -1, 1, 0)* size,
         new Vector3( -1, -1, 0)* size,

         new Vector3( 1, 0, 0)* size,
         new Vector3( 1, -1, 0)* size,
         new Vector3( 1, 1, 0)* size,


     };

      uv = new Vector2[]
     {
         new Vector3( -1, 0, 0),
         new Vector3( -5, 8.6441f, 0),
         new Vector3( -5, -8.6441f, 0),

         new Vector3( 1, 0, 0),
         new Vector3( 5, -8.6441f, 0),
         new Vector3( 5, 8.6441f, 0),
     };

     triangles = new int[]
     {
         0, 1, 2,
         1, 5, 2,
         5, 4, 2,
         3, 4 ,5
     };


    }

    public void SetShape(bool quad)
    {
        if(quad)
            StartCoroutine(LerpVertices(verticesQuad, 1));
        else
            StartCoroutine(LerpVertices(verticesHexa, 1));
    }

    IEnumerator LerpVertices(Vector3[] newVerticesPosition , float timeDuration)
    {
        float time = 1/timeDuration;
        float timer=0;

        Vector3[] tempArray = new Vector3[newVerticesPosition.Length];

        while(true)
        {
            for (int i = 0; i < verticesQuad.Length; i++)
            {
                tempArray[i] = Vector3.Lerp(myMeshFilter.mesh.vertices[i], newVerticesPosition[i], timer);
            }

            UpdateVertsPosition(tempArray);

            timer += Time.deltaTime * time;

            if (timer >= 1)
                break;

            yield return null;
        }

    }
  
}

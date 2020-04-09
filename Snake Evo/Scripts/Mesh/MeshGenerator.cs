using UnityEngine;
using System.Collections;

public class MeshGenerator{


    public static Mesh GenerateMesh(Vector3[] verts, int[] triangles, Vector2[] uvs)
    {
  
        Mesh ret = new Mesh();
        ret.vertices = verts;
        ret.triangles = triangles;
        ret.uv = uvs;
        ret.RecalculateBounds();
        ret.RecalculateNormals();
        return ret;
    }
}

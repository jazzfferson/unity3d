using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ProceduralMesh : MonoBehaviour {

    protected MeshFilter myMeshFilter;
    protected MeshRenderer myMeshRenderer;


    protected virtual void Awake()
    {
        myMeshFilter = GetComponent<MeshFilter>();
        myMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void GenerateMesh(Vector3[] verts, int[] triangles , Vector2[] uvs)
    {
        SetMesh(MeshGenerator.GenerateMesh(verts, triangles,uvs));
    }

    public void SetMesh(Mesh mesh)
    {
        myMeshFilter.mesh = mesh;
    }

    public void UpdateVertsPosition(Vector3[] verts)
    {
        myMeshFilter.mesh.vertices = verts;
    }

    public void SetVertsColor(Color color)
    {

        Color32[] colors = new Color32[myMeshFilter.mesh.vertexCount];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        myMeshFilter.mesh.colors32 = colors;

    }
	
}

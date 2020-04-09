using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSix : MonoBehaviour {

    public float size;

    public Vector2 dimension;
    public GameObject prefab;
    private List<TileMesh> tileMesh;

	void Start () {


 
        tileMesh = new List<TileMesh>();

        for (int j = 0; j < dimension.y; j++)
        {
            for (int i = 0; i < dimension.x; i++)
            {
                tileMesh.Add(((GameObject)Instantiate(prefab, new Vector3(i * size, j * size, 0), Quaternion.identity)).GetComponent<TileMesh>());
            }
        }

        Mesh myMesh = MeshGenerator.GenerateMesh(tileMesh[0].verticesHexa,tileMesh[0].triangles,tileMesh[0].uv);

        for (int i = 0; i < tileMesh.Count; i++)
        {
            tileMesh[i].SetMesh(myMesh);
        }
	
	}

    bool shape = true;
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.C))
        {
            shape =!shape;
            for (int i = 0; i < tileMesh.Count; i++)
            {
                tileMesh[i].SetShape(shape);
            }
        }
	
	}


}

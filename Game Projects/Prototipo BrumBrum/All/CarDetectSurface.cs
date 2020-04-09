using UnityEngine;
using System.Collections;


public class CarDetectSurface : MonoBehaviour {



	void Update () {

   RaycastHit hit;
   if (!Physics.Raycast(transform.position, Vector3.down, out hit, 10))

      return;
           
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (renderer == null || renderer.sharedMaterial == null 
                || renderer.sharedMaterial.mainTexture == null 
                || meshCollider == null)

                return;

            int triangleIdx = hit.triangleIndex;

            Mesh mesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;

            int subMeshesNr = mesh.subMeshCount;

            int materialIdx = -1;


            for (int i = 0; i < subMeshesNr; i++)
            {

                var tr = mesh.GetTriangles(i);

                for (int j = 0; j < tr.Length; j++)
                {

                    if (tr[j] == triangleIdx)
                    {

                        materialIdx = i;

                        break;

                    }

                }

                if (materialIdx != -1) break;

            }

            if (materialIdx != -1)
                Debug.Log("-------------------- I'm using " + renderer.materials[materialIdx].name + " material(s)" + " ID : " + materialIdx);
   
	}
    void DetectMaterial()
    {
         RaycastHit hit;
   if (!Physics.Raycast(transform.position, Vector3.down, out hit, 10))

      return;
           
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (renderer == null || renderer.sharedMaterial == null 
                || renderer.sharedMaterial.mainTexture == null 
                || meshCollider == null)

                return;

            int triangleIdx = hit.triangleIndex;

            Mesh mesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;

            int subMeshesNr = mesh.subMeshCount;

            int materialIdx = -1;


            for (int i = 0; i < subMeshesNr; i++)
            {

                var tr = mesh.GetTriangles(i);

                for (int j = 0; j < tr.Length; j++)
                {

                    if (tr[j] == triangleIdx)
                    {

                        materialIdx = i;

                        break;

                    }

                }

                if (materialIdx != -1) break;

            }

            if (materialIdx != -1)
                Debug.Log("----------------");
    }
}

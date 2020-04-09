using UnityEditor;
using UnityEngine;
using System.Collections;
[CustomEditor(typeof(SplinePath))]
public class SplinePathEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SplinePath script = (SplinePath)target;

        if (GUILayout.Button("Reconstruir Node"))
        {
            script.BuildNodes();
        }
    }

}

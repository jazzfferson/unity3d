
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

[CustomEditor(typeof(PlaceObject))]
public class PlaceObjectEditor : Editor
{

    private static bool m_editMode = false;
    private List<GameObject> objetos;
    
  

     void OnSceneGUI()
    {
        
        if (m_editMode)
        {
            if (Event.current.type == EventType.MouseUp)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;
                PlaceObject myTarget = (PlaceObject)target;

                if (Physics.Raycast(worldRay, out hitInfo,1000.0f,myTarget.applyOnLayerMask))
                {
                   
                    GameObject obj;
                    Quaternion rot;

                  
                    

                    if (objetos == null)
                    {
                        objetos = new List<GameObject>();
                    }

                  

                    if (myTarget.aleatorio)
                    {
                        obj = myTarget.GetRandom();
                    }
                    else
                    {
                        obj = myTarget.Get(myTarget.instanciarPrefab);
                    }
                    if (myTarget.rotacaoYAleatoria)
                    {
                        rot = myTarget.RandomRotation();
                    }
                    else
                    {
                        rot = Quaternion.identity;
                    }
                    GameObject prefabInstance = Instantiate(obj, hitInfo.point, rot) as GameObject;
                    prefabInstance.transform.parent = myTarget.parent;
                    EditorUtility.SetDirty(prefabInstance);
                    objetos.Add(prefabInstance);
                  
                    
                }

            }

            Event.current.Use();

        }

    }
     public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        
        if (m_editMode)
        {
            if (GUILayout.Button("Disable Editing"))
            {
                m_editMode = false;
            }
        }
        else
        {
            if (GUILayout.Button("Enable Editing"))
            {
                m_editMode = true;
            }
        }

      /*  if (GUILayout.Button("Reset"))
        {
           
        }*/

    }


}

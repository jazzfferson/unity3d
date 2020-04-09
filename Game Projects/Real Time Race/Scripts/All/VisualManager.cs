using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GraphicsLevel : int { High = 0, Medium = 1, Low = 2, VeryLow = 3 ,None = 4};
public abstract class VisualManager : MonoBehaviour
{
    /// <summary>
    /// Muda todos os materiais do array para Highs ou Lows
    /// </summary>
    /// <param name="arrayMaterialsLevelSetup"></param>
    /// <param name="highShaderLevel"></param>
    protected void ChangeAllMaterialsLevel(MaterialLevelSetup[] arrayMaterialsLevelSetup, bool highShaderLevel)
    {
        if (arrayMaterialsLevelSetup != null && arrayMaterialsLevelSetup.Length > 0)
        {
            for (int i = 0; i < arrayMaterialsLevelSetup.Length; i++)
                arrayMaterialsLevelSetup[i].ChangeMaterialLevel(highShaderLevel);
        }
        else
        {
            Debug.LogWarning("O array de 'Materials Level Setup' não foi configurado ou é nulo!!! ");
        }
    }

    /// <summary>
    /// Retorna verdadeiro se o level gráfico passado corresponde à um material High. E falso para materiais lows. 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    protected bool IsHighLevelMaterial(GraphicsLevel level)
    {
        bool highLevelMaterial = false;

        switch (level)
        {
            case GraphicsLevel.High:
                highLevelMaterial = true;
                break;
            default:
                highLevelMaterial = false;
                break;
        }

        return highLevelMaterial;
    }

    [System.Serializable]
    public class MaterialLevelSetup
    {
        public Material highMaterial;
        public Material lowMaterial;
        public MeshRenderer meshRenderer;
        public int materialIndex;
        public void ChangeMaterialLevel(bool high)
        {
           
            if (highMaterial!= null&&lowMaterial!=null)
            {
                Material[] arrayMaterials;
                arrayMaterials = meshRenderer.materials;

                if (high)
                {
                    arrayMaterials[materialIndex] = highMaterial;
                    meshRenderer.materials = arrayMaterials;

                    print(meshRenderer.gameObject.name + " Materials :" + "Set to HighLevel !");
                }
                else
                {
                    arrayMaterials[materialIndex] = lowMaterial;
                    meshRenderer.materials = arrayMaterials;

                    print(meshRenderer.gameObject.name + " Materials :" + "Set to LowLevel !");
                }
            }
            else
            {
                Debug.LogWarning("Material não setado no campo: HIGH e/ou LOW  do 'MaterialLevelSetup'!!!");
            }
        }
    }

    [System.Serializable]
    public class StaticPropsLevelSetup
    {
        public GameObject[] VeryLow, Low, Medium, High, VeryHigh;
        private List<GameObject[]> listaArray;

        /// <summary>
        /// Valor 0 é o level mais alto de qualidade.
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(GraphicsLevel level)
        {
            listaArray = new List<GameObject[]>();
            listaArray.Add(VeryHigh);
            listaArray.Add(High);
            listaArray.Add(Medium);
            listaArray.Add(Low);
            listaArray.Add(VeryLow);

            for (int i = 0; i < listaArray.Count; i++)
            {
                if (i >= (int)level)
                {
                    ActiveGameObjects(listaArray[i], true);
                }
                else
                {
                    ActiveGameObjects(listaArray[i], false);
                }
            }
      
        }

        private void ActiveGameObjects(GameObject[] array, bool active)
        {
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                    array[i].SetActive(active);
            }
            else
            {
                Debug.Log("Um dos arrays de objetos do 'StaticPropsLevelSetup' é nulo ou está vazio !!!");
            }
        }
    }
}




using UnityEngine;
using System.Collections;
using System;

public class MipMap : MonoBehaviour {

    public MaterialID[] materialsIDs;

	void Start () {

        SetMip();
	}


    void SetMip()
    {
         if (materialsIDs.Length < 1)
        {
            Debug.LogError(gameObject.name + " -> " + "Nenhum mipMap foi setado para este renderer!!!");
            return;
        }

        for (int i = 0; i < materialsIDs.Length; i++)
        {
            for (int j = 0; j < materialsIDs[i].MaterialInfo.Length; j++)
            {
                string name = materialsIDs[i].MaterialInfo[j].MapName;
                float mipLevel = materialsIDs[i].MaterialInfo[j].MipMapValue;
                if(string.IsNullOrEmpty(name))
                {
                    Debug.LogError(gameObject.name + " -> " + "O material info : " + j + " tem um nome de textura não válido !");
                    return;
                }

                try
                {
                  GetComponent<Renderer>().sharedMaterials[i].GetTexture(name).mipMapBias = mipLevel;
                }
                catch
                {
                    Debug.LogWarning("Não foi possível setar o mipmap!");
                }
            }  
        }       
    }

     [Serializable]
    public struct MaterialID
    {
        public MaterialInfo[] MaterialInfo;
    }
    [Serializable]
    public struct MaterialInfo
    {
        public string MapName;
        public float MipMapValue;
    }
	
}

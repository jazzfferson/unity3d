using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityPluginForWindowsPhone;





public class LoadSaveData
{
    
    //UncompressedGameData
    const string extensaoPadrao = ".dat";
    //CompressedGameData
    const string extensaoCompactada = ".cdat";

    public static void SaveFile(string fileName, object objeto/*,bool compress*/)
    {   
        string pathFile = pathToFile(fileName, extensaoPadrao);
       //UnityPluginForWindowsPhone.SaveAndLoad.SaveSerializedObject(  FileSerializer.Serialize(pathFile, objeto);

      
        //CompressFile.Compress(pathFile, extensaoCompactada);
        
      
    }
    public static object LoadFile(string fileName, Type type)
    {

        string pathFile = pathToFile(fileName, extensaoPadrao);

        if (File.Exists(pathToFile(fileName, extensaoPadrao)))
        {
            pathFile = pathToFile(fileName, extensaoPadrao);
        }
        else if (File.Exists(pathToFile(fileName, extensaoCompactada)))
        {
            pathFile = pathToFile(fileName, extensaoCompactada);
        }
        else
        {
           
            return null;
        }
        

        // return FileSerializer.Deserialize<CarReplayData>(pathFile);

        return null;

    }
    /// <summary>
    /// Retorna um array com o endereço dos arquivo que possuem
    /// a extensão especificada
    /// </summary>
    /// <param name="name">O nome da Extensão do arquivo</param>
    /// <returns></returns>
    public static string[] SearchByExtension(string name)
    {
        return null;
    }

    /// <summary>
    /// Retorna um array com o endereço dos arquivo que possuem
    /// o nome especificado
    /// </summary>
    /// <param name="name">O nome do arquivo</param>
    /// <returns></returns>
    public static string[] SearchByName(string name)
    {
        return null;
    }

    private static string pathToFile(string fileName, string extension)
    {
        return Application.persistentDataPath + "/" + fileName + extension;
    }


}









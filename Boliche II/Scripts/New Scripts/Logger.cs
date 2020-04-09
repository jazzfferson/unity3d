using UnityEngine;
using System.Collections;

public class Logger
{

   private static bool enable = true;
   public static void Log(object objectToLog)
    {
        if(enable)
        Debug.Log(objectToLog);
    }
}

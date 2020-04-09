using UnityEngine;


public class SaveAndLoadReplayData
{

    private static CarReplayData tempScriptableReplayData;

    public static void SaveStaticReplay(CarReplayData replayDataScript)
    {
        tempScriptableReplayData = replayDataScript;
    }

    public static CarReplayData LoadStaticReplay()
    {
        return tempScriptableReplayData;
    }

    public static void SaveReplayAsset(CarReplayData replayDataScript,string name)
    {
        LoadSaveData.SaveFile(name, replayDataScript);
    }

    public static CarReplayData LoadReplayAsset(string name)
    {
        return (CarReplayData)LoadSaveData.LoadFile(name, typeof(CarReplayData));
    }
	 
}

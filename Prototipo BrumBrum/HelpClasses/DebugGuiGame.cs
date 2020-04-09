using UnityEngine;
using System.Collections;

public class DebugGuiGame : MonoBehaviour {

    // Use this for initialization
    int level = 0;
    public void Down() 
    {
        if (level < 4)
            level++;
        CallLevel();
    }
    public void Up()
    {
        if (level > 0)
            level--;

        CallLevel();
    }
    void CallLevel()
    {
        //StaticReferences.Instance.SetVisualLevel((GraphicsLevel)level);
    }
}

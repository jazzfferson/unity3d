using UnityEngine;
using System.Collections;

public class RenderTargetCamera : MonoBehaviour
{
    [SerializeField]
    private Camera Race, Replay;
    [SerializeField]
    private RenderTexture textureRender;
	void Start () {

        textureRender.Release();
        textureRender.generateMips = false;
        textureRender.format = RenderTextureFormat.Default;

        if (ReplayControlGui.Replaying)
        {
            Replay.targetTexture = textureRender;
            Race.targetTexture = null;
        }
        else
        {
            Replay.targetTexture = null;
            Race.targetTexture = textureRender;
        }

	}

}

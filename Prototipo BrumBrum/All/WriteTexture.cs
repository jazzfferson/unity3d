using UnityEngine;
using System.Collections;

public class WriteTexture : MonoBehaviour {

	

    IEnumerator Start()
    {
        Texture2D texture = new Texture2D(32, 32);
        texture.mipMapBias = -3f;
        GetComponent<Renderer>().material.mainTexture = texture;
        int y = 0;
        while (y < texture.height)
        {
            int x = 0;
            while (x < texture.width)
            {
                Color color = ((x == y) ? Color.white : Color.gray);
                texture.SetPixel(x, y, color);
                ++x;
                yield return new WaitForSeconds(0.0000001f);
                texture.Apply();
            }
            y++;
        }
       

    }
}

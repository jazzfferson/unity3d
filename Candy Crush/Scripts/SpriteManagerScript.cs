using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManagerScript : MonoBehaviour {

    [SerializeField]
    private Sprite[] spriteArray;

    private Dictionary<string, Sprite> dicionarioSprite;

    void Awake()
    {
        dicionarioSprite = new Dictionary<string, Sprite>(spriteArray.Length);

        foreach (Sprite sprite in spriteArray)
        {
            
            dicionarioSprite.Add(sprite.name, sprite);
        }
    }

    public Sprite GetSprite(string name)
    {
        return dicionarioSprite[name];
    }

    public Sprite GetSprite(int index)
    {
        return spriteArray[index];
    }

    public Sprite GetCandySprite(ECandyType type,bool highLighted)
    {
        Sprite sprite = null;
        int highLightedAdd=0;

        if (highLighted)
        {
          //  highLightedAdd = 1;
        }

        switch (type)
        {
            case ECandyType.One:
                sprite = GetSprite(0 + highLightedAdd);
                break;
            case ECandyType.Two:
                sprite = GetSprite(2 + highLightedAdd);
                break;
            case ECandyType.Three:
                sprite = GetSprite(4 + highLightedAdd);
                break;
            case ECandyType.Four:
                sprite = GetSprite(6 + highLightedAdd);
                break;
            case ECandyType.Five:
                sprite = GetSprite(8 + highLightedAdd);
                break;
            case ECandyType.Six:
                sprite = GetSprite(10 + highLightedAdd);
                break;
        }

        return sprite;
    }
}

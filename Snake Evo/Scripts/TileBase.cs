using UnityEngine;
using System.Collections;

public enum ETileType {Wall,Ground,Food,EspecialFood,SpeedFood,Snake};

public class TileBase : MonoBehaviour {

    [SerializeField]
    private ETileType type;

    [SerializeField]
    private GradienteLoopColor gradientLoopColor;



    public SpriteRenderer spriteRenderer;
    public Color originalColor;
    public Color wallColor;
    public Color foodColor;
    public Color especialFoodColor;
    public int xIndex;
    public int yIndex;

    public virtual void SetType(ETileType tileType)
    {
        type = tileType;

        gradientLoopColor.enabled = false;

        switch (tileType)
        {
            case ETileType.Wall:
                spriteRenderer.color = wallColor;
                break;
            case ETileType.Ground:
                break;
            case ETileType.Food:
                spriteRenderer.color = foodColor;
                break;
            case ETileType.EspecialFood:
                spriteRenderer.color = especialFoodColor;
                break;
            case ETileType.SpeedFood:
                gradientLoopColor.enabled = true;
                break;
            case ETileType.Snake:
                break;

        }
    }

    public ETileType GetType()
    {
        return type;
    }

    void Update()
    {
        if(gradientLoopColor.enabled)
        {
            spriteRenderer.color = gradientLoopColor.colorResult;
        }
    }

}

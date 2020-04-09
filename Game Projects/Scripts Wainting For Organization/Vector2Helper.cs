using UnityEngine;
public class Vector2Helper
{
    public static float Cross(Vector2 v, Vector2 w)
    {
        return v.x * w.y - v.y * w.x;
    }
}
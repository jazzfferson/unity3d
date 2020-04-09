using System;
using System.Collections.Generic;
using UnityEngine;
public class FixedRandom
{
    public static float[] Values = new float[10];
    public static int FixedUpdateIndex = -1;
    public static float ValueFromPosition(Vector3 position)
    {
        return FixedRandom.Values[FixedRandom.IndexFromPosition(position)];
    }
    public static int IndexFromPosition(Vector3 position)
    {
        int value = Mathf.FloorToInt(Mathf.PerlinNoise(position.x, position.y) * (float)(FixedRandom.Values.Length - 1));
        return Mathf.Clamp(value, 0, FixedRandom.Values.Length - 1);
    }
    public static int Range(int min, int max, int valuesIndex)
    {
        return (int)Mathf.Lerp((float)min, (float)max, FixedRandom.Values[valuesIndex]);
    }
    public static float Range(float min, float max, int valuesIndex)
    {
        return Mathf.Lerp(min, max, FixedRandom.Values[valuesIndex]);
    }
    public static T GetRandomListItem<T>(List<T> list, int valuesIndex) where T : class
    {
        return list[FixedRandom.Range(0, list.Count, valuesIndex)];
    }
    public static T GetRandomArrayItem<T>(T[] list, int valuesIndex) where T : class
    {
        return list[FixedRandom.Range(0, list.Length, valuesIndex)];
    }
    public static void UpdateValues()
    {
        UnityEngine.Random.seed = FixedRandom.FixedUpdateIndex;
        for (int i = 0; i < FixedRandom.Values.Length; i++)
        {
            FixedRandom.Values[i] = UnityEngine.Random.value;
        }
    }
    public static void SetFixedUpdateIndex(int index)
    {
        FixedRandom.FixedUpdateIndex = index;
        FixedRandom.UpdateValues();
    }
}

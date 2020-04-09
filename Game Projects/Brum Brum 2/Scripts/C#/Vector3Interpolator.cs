using UnityEngine;
using System.Collections;

public class Vector3Interpolator
{
    Vector3[] _vector3array;
    public Vector3Interpolator(Vector3[] vector3array)
    {
        _vector3array = vector3array;
    }

    public Vector3 Evaluate(float time)
    {
        Vector3 c = _vector3array[0];
        float val = time;
        val *= (_vector3array.Length - 1);

        int startIndex = Mathf.FloorToInt(val);

        if (startIndex >= 0)
        {
            if (startIndex + 1 < _vector3array.Length)
            {
                float factor = (val - startIndex);
                c = Vector3.Lerp(_vector3array[startIndex], _vector3array[startIndex + 1], factor);
            }
            else if (startIndex < _vector3array.Length)
            {
                c = _vector3array[startIndex];
            }
            else c = _vector3array[_vector3array.Length - 1];
        }

        return c;
    }

}

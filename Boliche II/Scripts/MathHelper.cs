using UnityEngine;

public class MathHelper {

    /// <summary>
    /// Determines whether the quaternion is safe for interpolation or use with transform.rotation.
    /// </summary>
    /// <returns><c>false</c> if using the quaternion in Quaternion.Lerp() will result in an error (eg. NaN values or zero-length quaternion).</returns>
    /// <param name="quaternion">Quaternion.</param>
    public static bool IsValid(Quaternion quaternion)
    {
        bool isNaN = float.IsNaN(quaternion.x + quaternion.y + quaternion.z + quaternion.w);

        bool isZero = quaternion.x == 0 && quaternion.y == 0 && quaternion.z == 0 && quaternion.w == 0;

        return !(isNaN || isZero);
    }

    public static bool IsValid(Vector3 quaternion)
    {
        bool isNaN = float.IsNaN(quaternion.x + quaternion.y + quaternion.z);

        bool isZero = quaternion.x == 0 && quaternion.y == 0 && quaternion.z == 0;

        return !(isNaN || isZero);
    }

	public static float Lerp (float from, float to, float value) {
		if (value < 0.0f)
			return from;
		else if (value > 1.0f)
			return to;
		return (to - from) * value + from;
	}
	
	public static float LerpUnclamped (float from, float to, float value) {
		return (1.0f - value)*from + value*to;
	}
	
	public static float InverseLerp (float from, float to, float value) {
		if (from < to) {
			if (value < from)
				return 0.0f;
			else if (value > to)
				return 1.0f;
		}
		else {
			if (value < to)
				return 1.0f;
			else if (value > from)
				return 0.0f;
		}
		return (value - from) / (to - from);
	}
	
	public static float InverseLerpUnclamped (float from, float to, float value) {
		return (value - from) / (to - from);
	}
	
	public static float SmoothStep (float from, float to, float value) {
		if (value < 0.0f)
			return from;
		else if (value > 1.0f)
			return to;
		value = value*value*(3.0f - 2.0f*value);
		return (1.0f - value)*from + value*to;
	}
	
	public static float SmoothStepUnclamped (float from, float to, float value) {
		value = value*value*(3.0f - 2.0f*value);
		return (1.0f - value)*from + value*to;
	}
	
	public static float SuperLerp (float from, float to, float from2, float to2, float value) {
		if (from2 < to2) {
			if (value < from2)
				value = from2;
			else if (value > to2)
				value = to2;
		}
		else {
			if (value < to2)
				value = to2;
			else if (value > from2)
				value = from2;	
		}
		return (to - from) * ((value - from2) / (to2 - from2)) + from;
	}
	
	public static float SuperLerpUnclamped (float from, float to, float from2, float to2, float value) {
		return (to - from) * ((value - from2) / (to2 - from2)) + from;
	}
	
	public static Color ColorLerp (Color c1, Color c2, float value) {
		if (value > 1.0f)
			return c2;
		else if (value < 0.0f)
			return c1;
		return new Color (	c1.r + (c2.r - c1.r)*value, 
							c1.g + (c2.g - c1.g)*value, 
							c1.b + (c2.b - c1.b)*value, 
							c1.a + (c2.a - c1.a)*value );
	}

	public static Vector2 Vector2Lerp (Vector2 v1, Vector2 v2, float value) {
		if (value > 1.0f)
			return v2;
		else if (value < 0.0f)
			return v1;
		return new Vector2 (v1.x + (v2.x - v1.x)*value, 
							v1.y + (v2.y - v1.y)*value );		
	}
	
	public static Vector3 Vector3Lerp (Vector3 v1, Vector3 v2, float value) {
		if (value > 1.0f)
			return v2;
		else if (value < 0.0f)
			return v1;
		return new Vector3 (v1.x + (v2.x - v1.x)*value, 
							v1.y + (v2.y - v1.y)*value, 
							v1.z + (v2.z - v1.z)*value );
	}
	
	public static Vector4 Vector4Lerp (Vector4 v1, Vector4 v2, float value) {
		if (value > 1.0f)
			return v2;
		else if (value < 0.0f)
			return v1;
		return new Vector4 (v1.x + (v2.x - v1.x)*value, 
							v1.y + (v2.y - v1.y)*value, 
							v1.z + (v2.z - v1.z)*value,
							v1.w + (v2.w - v1.w)*value );
	}

	public static void Clamp (ref float value, float a, float b) {
		if (value < a) {
			value = a;
		}
		else if (value > b) {
			value = b;
		}
	}
	
	public static void Clamp (ref int value, int a, int b) {
		if (value < a) {
			value = a;
		}
		else if (value > b) {
			value = b;
		}
	}

    public static float InterpolateCurves(float interpolate, float time, AnimationCurve[] curveArray)
    {
        float c = curveArray[0].Evaluate(time);
        float val = interpolate;
        val *= (curveArray.Length - 1);

        int startIndex = Mathf.FloorToInt(val);

        if (startIndex >= 0)
        {
            if (startIndex + 1 < curveArray.Length)
            {
                float factor = (val - startIndex);
                c = Mathf.Lerp(curveArray[startIndex].Evaluate(time), curveArray[startIndex + 1].Evaluate(time), factor);
            }
            else if (startIndex < curveArray.Length)
            {
                c = curveArray[startIndex].Evaluate(time);
            }
            else c = curveArray[curveArray.Length - 1].Evaluate(time);
        }

        return c;
    }

    public static Vector3 InterpolateVector3(float t, Vector3[] vectorArray)
    {
        Vector3 c = vectorArray[0];
        float val = t;
        val *= (vectorArray.Length - 1);

        int startIndex = Mathf.FloorToInt(val);

        if (startIndex >= 0)
        {
            if (startIndex + 1 < vectorArray.Length)
            {
                float factor = (val - startIndex);
                c = Vector3.Lerp(vectorArray[startIndex], vectorArray[startIndex + 1], factor);
            }
            else if (startIndex < vectorArray.Length)
            {
                c = vectorArray[startIndex];
            }
            else c = vectorArray[vectorArray.Length - 1];
        }

        return c;
    }

    public static Vector3 InterpolateVector3(float t, Transform[] transformArray)
    {
        Vector3 c = transformArray[0].position;
        float val = t;
        val *= (transformArray.Length - 1);

        int startIndex = Mathf.FloorToInt(val);

        if (startIndex >= 0)
        {
            if (startIndex + 1 < transformArray.Length)
            {
                float factor = (val - startIndex);
                c = Vector3.Lerp(transformArray[startIndex].position, transformArray[startIndex + 1].position, factor);
            }
            else if (startIndex < transformArray.Length)
            {
                c = transformArray[startIndex].position;
            }
            else c = transformArray[transformArray.Length - 1].position;
        }

        return c;
    }

    public static Vector3 InterpolateVector3Interpolator(float interpolate, float time, Vector3Interpolator[] vector3InterpolatorArray)
    {
        Vector3 c = vector3InterpolatorArray[0].Evaluate(time);
        float val = interpolate;
        val *= (vector3InterpolatorArray.Length - 1);

        int startIndex = Mathf.FloorToInt(val);

        if (startIndex >= 0)
        {
            if (startIndex + 1 < vector3InterpolatorArray.Length)
            {
                float factor = (val - startIndex);
                c = Vector3.Lerp(vector3InterpolatorArray[startIndex].Evaluate(time), vector3InterpolatorArray[startIndex + 1].Evaluate(time), factor);
            }
            else if (startIndex < vector3InterpolatorArray.Length)
            {
                c = vector3InterpolatorArray[startIndex].Evaluate(time);
            }
            else c = vector3InterpolatorArray[vector3InterpolatorArray.Length - 1].Evaluate(time);
        }

        return c;
    }

    #region Fisica
    public MathHelper()
    {

    }

    private Vector3 _lastPositionPTV;
    /// <summary>
    /// Retorna a velocidade de um objeto baseado na sua posição
    /// </summary>
    /// <param name="position"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public Vector3 VelocityByPosition(Vector3 position, float deltaTime)
    {
        Vector3 velocity = (position - _lastPositionPTV) / deltaTime;
        _lastPositionPTV = position;
        return velocity;
    }

    private Vector3 _lastAccelerationVTA;
    /// <summary>
    /// retorna a aceleração de um objeto baseado na sua velocidade
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public Vector3 AccelerationByVelocity(Vector3 velocity, float deltaTime)
    {
        Vector3 acceleration = (velocity - _lastAccelerationVTA) / deltaTime;
        _lastAccelerationVTA = velocity;
        return acceleration;
    }

    private Vector3 _lastPositionPTA;
    private Vector3 _lastAccelerationPTA;
    /// <summary>
    /// Retorna a aceleração baseado na posicao
    /// </summary>
    /// <param name="position"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public Vector3 AccelerationByPosition(Vector3 position, float deltaTime)
    {
        Vector3 velocity = (position - _lastPositionPTA) / deltaTime;
        _lastPositionPTA = position;

        Vector3 acceleration = (velocity - _lastAccelerationPTA) / deltaTime;
        _lastAccelerationPTA = velocity;
        return acceleration;
    }

    /// <summary>
    /// retorna a força G baseado na aceleração e na constante gravitacional (g = 9.81f)
    /// </summary>
    /// <param name="acceleration"></param>
    /// <param name="constatenGravitacional"></param>
    /// <returns></returns>
    public Vector3 GforceByAcceleration(Vector3 acceleration, float constatenGravitacional)
    {
        return acceleration / constatenGravitacional;
    }

    private float _lastPosition;
    public float Velocity(float position, float deltaTime)
    {
        float velocity = (position - _lastPosition) / deltaTime;
        _lastPosition = position;
        return velocity;
    }
  
    public static float Inertia(float mass, float raio)
    {
        return mass * Mathf.Pow(mass, 2);
    }

    #endregion

    #region Conversores de unidade
    
    public static float ConvertNmToLbft(float nm)
    {
        return nm * 0.73756214837f;
    }
    public static float ConvertLbftToNm(float lbFt)
    {
        return lbFt * 1.35581794884f;
    }
    public static float ConvertMeterPerSecondToKph(float metersPerSecond)
    {
        return metersPerSecond * 3.6f;
    }
    public static float RadiusToCircunference(float radius)
    {
        return 2 * Mathf.PI * radius; 
    }
    public static float WheelRpmToKph(float rpm, float wheelRadius)
    {
        //Metros por minutos
        float speed = RadiusToCircunference(wheelRadius) * rpm ;
        //Kilometros por hora
        return speed / 1000 * 60;
        //kph = speed / 1000 * 60
    }
    public static float KphToWheelRpm(float kph, float wheelRadius)
    {
        //Metros por minutos
        float speed = (kph / 60) * 1000;
        //Rpm
        return speed / RadiusToCircunference(wheelRadius);
    }

    public static float RpmToAngularVelocity(float rpm)
    {
        return (Mathf.PI * 2) * (rpm / 60);
    }

    #endregion

    #region Conversores de dados

    /// <summary>
    /// Sbyte range = -127..127
    /// Para floats com valores entre -1 e 1
    /// Valores acima ou menores disso serão "Clampeados" entre -1 e 1
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static sbyte FloatToSbyte(float value)
    {
        return (sbyte)QuantizeFloatToInt(value, 127);
    }

    public static float SbyteToFloat(sbyte value)
    {
        float asFloat = value;
        return asFloat / 127.0f;
    }

    /// <summary>
    /// Quantiza valores floats entre -1 e 1 para o range determinado.
    /// Valores acima ou menores disso serão "Clampeados" entre -1 e 1
    /// </summary>
    /// <param name="value"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static int QuantizeFloatToInt(float value,int range)
    {
        value = Mathf.Clamp(value, -1, 1);
        return Mathf.RoundToInt(value * range);
    }

    public static float QuantizeFloat(float value, int range)
    {
        return QuantizeFloatToInt(value, range) / (float)range;
    }

    #endregion
}
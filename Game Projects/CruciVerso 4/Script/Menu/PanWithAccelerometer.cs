using UnityEngine;

/// <summary>
/// Placing this script on the game object will make that game object pan with mouse movement.
/// </summary>

public class PanWithAccelerometer : IgnoreTimeScale
{
	public Vector2 degrees;
	public float range = 1f;

	Transform mTrans;
	Quaternion mStart;
	Vector2 mRot = Vector2.zero;

	void Start ()
	{
		mTrans = transform;
		mStart = mTrans.localRotation;
	}

	void Update ()
	{
		float delta = UpdateRealTimeDelta();
		//Vector3 pos = Input.mousePosition;
		
	
		
		Vector3 pos = Input.acceleration;
		
		DebugViewer.Texto[0] = pos.ToString();

		/*float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;*/
		
		if (range < 0.1f) range = 0.1f;
		
		
		float x = Mathf.Clamp((pos.x / range), -1f, 1f);
		float y = Mathf.Clamp((pos.y/ range), -1f, 1f);
		
		mRot = Vector2.Lerp(mRot, new Vector2(x, y), delta * 5f);

		mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * degrees.y, mRot.x * degrees.x, 0f);
	}
}
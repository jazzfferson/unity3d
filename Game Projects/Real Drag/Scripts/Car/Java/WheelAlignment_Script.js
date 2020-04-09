// Define the variables used in the script, the Corresponding collider is the wheel collider at the position of
// the visible wheel, the slip prefab is the prefab instantiated when the wheels slide, the rotation value is the
// value used to rotate the wheel around it's axel.
var CorrespondingCollider : WheelCollider;
private var RotationValue : float = 0.0;

function FixedUpdate () {

	// define a hit point for the raycast collision
	var hit : RaycastHit;
	// Find the collider's center point, you need to do this because the center of the collider might not actually be
	// the real position if the transform's off.
	var ColliderCenterPoint : Vector3 = CorrespondingCollider.transform.TransformPoint( CorrespondingCollider.center );
	
	// now cast a ray out from the wheel collider's center the distance of the suspension, if it hit something, then use the "hit"
	// variable's data to find where the wheel hit, if it didn't, then se tthe wheel to be fully extended along the suspension.
	if ( Physics.Raycast( ColliderCenterPoint, -CorrespondingCollider.transform.up, hit, CorrespondingCollider.suspensionDistance + CorrespondingCollider.radius ) ) {
		transform.position = hit.point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius);
	}else{
		transform.position = ColliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance);
	}
	
	// now set the wheel rotation to the rotation of the collider combined with a new rotation value. This new value
	// is the rotation around the axle, and the rotation from steering input.
	transform.rotation = CorrespondingCollider.transform.rotation * Quaternion.Euler( RotationValue, CorrespondingCollider.steerAngle, 0 );
	// increase the rotation value by the rotation speed (in degrees per second)
	RotationValue += CorrespondingCollider.rpm * ( 360/60 ) * Time.fixedDeltaTime;

}
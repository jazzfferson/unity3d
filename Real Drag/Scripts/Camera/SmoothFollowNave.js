@script ExecuteInEditMode()


// The target we are following
var target : Transform;
var followDamp:Vector3;
var offSetTarget: Vector3;
var XMaxPosition:float;

// Place the script in the Camera-Control group in the component menu
@script AddComponentMenu("Camera-Control/Smooth Follow")


function Start()
{

}
function FixedUpdate () {

	transform.position.z = Mathf.Lerp(transform.position.z,target.position.z + offSetTarget.z,Time.fixedDeltaTime*followDamp.z);
	transform.position.x = Mathf.Lerp(transform.position.x,target.position.x + offSetTarget.x,Time.fixedDeltaTime*followDamp.x);

}
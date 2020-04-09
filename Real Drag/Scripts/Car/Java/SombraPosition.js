#pragma strict

var target:Transform;

private var fixedPosition:Vector3;

function Start()
{
	fixedPosition = transform.position;
}
function Update () {


	transform.position.y = fixedPosition.y;

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EFreeSide {LeftFree,RightFree,BothFree,Blocked};
public class IAController : MonoBehaviour
{

    public float rayLength = 2;

    public AnimationCurve speed;

    public Path trackPath;
    public Transform frontRay;
	public Transform frontRayL;
	public Transform frontRayR;
	public Transform frontAngleRayL;
	public Transform frontAngleRayR;
	public Transform lateralRayL;
	public Transform lateralRayR;

    private CarManager carManager;
    private int actualWayPointIndex = 0;
    private Vector3 actualWayPointPosition = Vector3.zero;
    private Rigidbody ridiBody;
    public float pathDirectionOffset = 0;
    public float brakeMultiplier;


    private bool frontClear = true;
    private float frontRayDistance;

	private bool frontLClear = true;
    private float frontLRayDistance;

    private bool frontRClear = true;
    private float frontRRayDistance;

    private bool frontALClear = true;
    private float frontALRayDistance;

    private bool frontARClear = true;
    private float frontARRayDistance;

    private bool lateralLClear = true;
    private float lateralLRayDistance;

    private bool lateralRClear = true;
    private float lateralRRayDistance;

    private EFreeSide eSideFree = EFreeSide.BothFree;
    private bool isSkipingSide = false;
    private int sideToSkip = 0;

    void Start()
    {

        carManager = GetComponent<CarManager>();
        ridiBody = GetComponent<Rigidbody>();
        actualWayPointPosition = GetNodeWayPoint(actualWayPointIndex);


    }

    private void Accelerate()
    {
        carManager.ApplyThrottle(1);
        carManager.ApplyBrake(0);
    }
    private void Brake()
    {
        carManager.ApplyThrottle(0);
        carManager.ApplyBrake(1);
    }

    void Update()
    {


     
      //  carManager.ApplyThrottle(throttleNodeSpeed.Evaluate(GetNodePath(actualWayPointIndex).speed));
      //  carManager.ApplyBrake(brakeNodeSpeed.Evaluate(GetNodePath(actualWayPointIndex).speed));
		

        carManager.Steering(CalculeSteer(actualWayPointPosition));

        if (NodeAchievied(actualWayPointPosition, GetNodePath(actualWayPointIndex).size))
        {
            actualWayPointIndex++;
            if (actualWayPointIndex >=trackPath.Nodes.Length)
            {
                actualWayPointIndex = 0;
            }
            actualWayPointPosition = GetNodeWayPoint(actualWayPointIndex);
        }

        float targetSpeed = speed.Evaluate(GetNodePath(Mathf.Clamp(actualWayPointIndex - 1, 0, trackPath.Nodes.Length)).speed);

             if (ridiBody.velocity.magnitude * 3.6f <= targetSpeed)
             {
                 carManager.ApplyThrottle(1);
                 carManager.ApplyBrake(0);
             }
             else if (ridiBody.velocity.magnitude * 3.6f >= targetSpeed)
             {
                 carManager.ApplyThrottle(0);
                 carManager.ApplyBrake(1);
             }
      /*
        #region Rays calculation

        // front ray
        RaycastHit hit;
		if (IARay(12 * rayLength, frontRay, out hit))
        {
            frontRayDistance = Vector3.Distance(hit.point, frontRay.position);
            frontClear = false;
        }
        else
        {
            frontRayDistance = -1;
            frontClear = true;
        }

		// front R ray
		if (IARay(6 * rayLength, frontRayR, out hit))
		{
            frontRRayDistance = Vector3.Distance(hit.point, frontRayR.position);
            frontRClear = false;
		}
		else
		{
            frontRRayDistance = -1;
            frontRClear = true;
		}

		// front L ray
		if (IARay(6 * rayLength, frontRayL, out hit))
		{
            frontLRayDistance = Vector3.Distance(hit.point, frontRayL.position);
            frontLClear = false;
		}
		else
		{
            frontLRayDistance = -1;
            frontLClear = true;
		}

		// front Angle R ray
		if (IARay(6 * rayLength, frontAngleRayR, out hit))
		{
            frontARRayDistance = Vector3.Distance(hit.point, frontAngleRayR.position);
            frontARClear = false;
		}
		else
		{
            frontARRayDistance = -1;
            frontARClear = true;
		}

		// front Angle L ray
		if (IARay(6 * rayLength, frontAngleRayL, out hit))
		{
            frontALRayDistance = Vector3.Distance(hit.point, frontAngleRayL.position);
            frontALClear = false;
		}
		else
		{
            frontALRayDistance = -1;
            frontALClear = true;
		}

		// lateral L ray
		if (IARay(2 * rayLength, lateralRayL, out hit))
		{
            lateralLRayDistance = Vector3.Distance(hit.point, lateralRayL.position);
            lateralLClear = false;
		}
		else
		{
            lateralLRayDistance = -1;
			lateralLClear = true;
		}

		// lateral R ray
		if (IARay(2 * rayLength, lateralRayR, out hit))
		{
            lateralRRayDistance = Vector3.Distance(hit.point, lateralRayR.position);
            lateralRClear = false;
		}
		else
		{
            lateralRRayDistance = -1;
            lateralRClear = true;
		}

        #endregion
        */
    }

    void SkipFromSide(int dir)
    {
        if (Mathf.Abs(pathDirectionOffset) < 0.5f)
        {
            if (dir == 1)
            {
                pathDirectionOffset += Time.deltaTime;
            }
            else
            {
                pathDirectionOffset -= Time.deltaTime;
            }
        }
    }


    float CalculeSteer(Vector3 node)
    {
        Vector3 RelativeWaypointPosition = transform.InverseTransformPoint(new Vector3(node.x, transform.position.y, node.z));
        return RelativeWaypointPosition.x / RelativeWaypointPosition.magnitude;
    }

    bool NodeAchievied(Vector3 nodePosition, float distance)
    {
        if (Vector3.Distance(transform.position, nodePosition) < distance) { return true; }
        else { return false; }
    }

    Vector3 GetNodeWayPoint(int index)
    {
        return trackPath.Nodes[index].transform.position;
    }
    PathNode GetNodePath(int index)
    {
        return trackPath.Nodes[index];
    }

    Vector3 LocalDirection(Transform originTransform)
    {

        return originTransform.TransformDirection(Vector3.forward);
    }

    bool RayCast(Transform origin,Vector3 direction, float distance, out RaycastHit hit)
    {

        if (Physics.Raycast(origin.position, direction, out hit, distance))
        {
            Debug.Log(hit.collider.gameObject.name);
            return true;
        }
        else
            return false;
    }

    bool IARay(float distance, Transform originTransform, out RaycastHit hit)
    {

        Vector3 dir = LocalDirection(originTransform);

        if (RayCast(originTransform,dir, distance, out hit))
        {

          
            //  Debug.DrawLine(rayOrigin.position, hit.point, Color.red);
            Debug.DrawRay(originTransform.position, dir * distance, Color.red);
            return true;
        }
        else
        {
			Debug.DrawRay(originTransform.position, dir * distance, Color.white);
            return false;
        }
    }
}
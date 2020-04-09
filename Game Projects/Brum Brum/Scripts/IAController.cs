using UnityEngine;
using System.Collections;

public class IAController : MonoBehaviour
{

    public Path trackPath;
    private CarManager carManager;
    private int actualWayPointIndex = 0;
    private Vector3 actualWayPointPosition = Vector3.zero;
    private Rigidbody rigidbody;


    void Start()
    {

        carManager = GetComponent<CarManager>();
        rigidbody = GetComponent<Rigidbody>();
        actualWayPointPosition = GetNodeWayPoint(actualWayPointIndex);

    }


    void Update()
    {


        if ((rigidbody.velocity.magnitude * 3.6f) < 95)
        {
            carManager.ApplyThrottle(1f);
            carManager.ApplyBrake(0);
        }
        else
        {
            carManager.ApplyThrottle(0);
            carManager.ApplyBrake(0.2f);
        }

        carManager.Steering(CalculeSteer(actualWayPointPosition));

        if (NodeAchievied(actualWayPointPosition, trackPath.nodeSize))
        {
            actualWayPointIndex++;
            if (actualWayPointIndex >=trackPath.Nodes.Length)
            {
                actualWayPointIndex = 0;
            }
            actualWayPointPosition = GetNodeWayPoint(actualWayPointIndex);
        }

    }

    float CalculeSteer(Vector3 node)
    {
        Vector3 RelativeWaypointPosition = transform.InverseTransformPoint(new Vector3(node.x, transform.position.y, node.z));
        return RelativeWaypointPosition.x / RelativeWaypointPosition.magnitude;
    }

    bool NodeAchievied(Vector3 node, float distance)
    {
        if (Vector3.Distance(transform.position, node) <= distance) { return true; }
        else { return false; }
    }

    Vector3 GetNodeWayPoint(int index)
    {
        return trackPath.Nodes[index].position;
    }

}

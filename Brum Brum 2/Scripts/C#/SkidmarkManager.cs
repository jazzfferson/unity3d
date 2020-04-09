using UnityEngine;
using System.Collections;

public class SkidmarkManager : MonoBehaviour
{

    public float minSlip;
    public float multiplier = 2;
    public float forwardOffset = 0.1f;

    public Skidmarks skidMarks;
    [SerializeField]
    private WheelCollider wheel;

    private WheelData wData;
    private Vector3 lastPosition;

    class WheelData
    {
        public WheelCollider wheel;
        public int lastSkidMark = -1;
    };

    void Awake()
    {

        wData = new WheelData();
        wData.wheel = wheel;
    }

    void FixedUpdate()
    {
        WheelHit hit;

        if (wData.wheel.GetGroundHit(out hit))
        {
            float slip = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);

            if (slip > minSlip)
            {
                if (Vector3.Distance(lastPosition, hit.point) > skidMarks.minDistance)
                {
                    lastPosition = hit.point;
                    wData.lastSkidMark = skidMarks.AddSkidMark(hit.point + wheel.transform.forward * forwardOffset, hit.normal, (slip - minSlip) * 2f + multiplier, wData.lastSkidMark);
                }
                else
                {
                    return;
                }
            }
            else
                wData.lastSkidMark = -1;
        }
        else wData.lastSkidMark = -1;
    }
}

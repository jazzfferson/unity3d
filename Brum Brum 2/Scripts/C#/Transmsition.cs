using UnityEngine;
using System.Collections;

public class Transmsition : MonoBehaviour {

    public float[] gearsRatio;
	public float finalDriveRatio;
	public float speedChange;
	public int GearIndex;
	/*{
		get;
		private set;
	}*/
	public int NextGearIndex
	{
		get;
		private set;
	}

	public float ActualGearRatio
	{
		get;
		private set;
	}

	public float FinalDrive
	{
		get{return finalDriveRatio;}
	}

    public int GearsCount
    {
        get;
        private set;
    }

    public bool IsChangingGear
    {
        get;
        private set;
    }

    void Start()
    {
        GearsCount = gearsRatio.Length - 1;
    }

	public void ChangeGear(int gearIndex)
	{
		NextGearIndex = gearIndex;
        ActualGearRatio = gearsRatio[GearIndex];
        if(GearChanged!=null)
		GearChanged();
        IsChangingGear = true;
		StartCoroutine(DelayChange());
	}

    public void MaxRpmGear(int gear)
    {
       
    }
    
	public delegate void Changing();
	
	public event Changing GearChanged;
	
	IEnumerator DelayChange()
	{
		yield return new WaitForSeconds(speedChange);
        ActualGearRatio = gearsRatio[NextGearIndex];
		GearIndex = NextGearIndex;
        IsChangingGear = false;
	}
}

using UnityEngine;
using System.Collections;

public class Transmsition : MonoBehaviour {

	public AnimationCurve gearsRatioCurve;
	public float finalDriveRatio;
	public float speedChange;
	public int GearIndex
	{
		get;
		private set;
	}
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
	public void ChangeGear(int gearIndex)
	{
		NextGearIndex = gearIndex;
		
		ActualGearRatio = gearsRatioCurve.Evaluate(0);
		GearChanged();
		StartCoroutine(DelayChange());
	}
	
	public delegate void Changing();
	
	public Changing GearChanged;
	
	IEnumerator DelayChange()
	{
		yield return new WaitForSeconds(speedChange);
		ActualGearRatio = gearsRatioCurve.Evaluate(NextGearIndex);
		GearIndex = NextGearIndex;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerPlateSlot : MonoBehaviour {

	private EPlateStatus _status;
	public EPlateStatus Status 
	{
		get;
	}
	public int slotIndex;

	private SellingFood _food;
	public SellingFood Food 
	{
		get;
	}

	public void SetFood(SellingFood inFood)
	{
		_food = inFood;
		_food.transform.position = gameObject.transform.position;
		SetStatus (EPlateStatus.COOKING);
	}

	public bool IsOnCooking
	{
		get 
		{
			return (_status.Equals (EPlateStatus.COOKING) && Food != null);
		}	

	}

	public bool IsCookable
	{
		get
		{
			return (_status.Equals (EPlateStatus.EMPTY) && Food == null);	
		}
	}

	public void OnServedFood()
	{
		_food = null;
		SetStatus (EPlateStatus.EMPTY);
	}

	void Start()
	{
		SetStatus (EPlateStatus.LOCKED);
		_food = null;
	}

	public void SetStatus(EPlateStatus inStatus)
	{
		_status = inStatus;
		UpdateView ();
	}

	private void UpdateView()
	{
		Debug.Log(string.Format("[HamburgerPlateSlot] slot #{0} - status ({1}) - food ({2})", slotIndex, _status.ToString(), _food.materials));
	}

}

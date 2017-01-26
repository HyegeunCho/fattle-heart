using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerPlate : MonoBehaviour
{

	private const EFoodType allowableFood = EFoodType.HAMBURGER;

	private int _level;

	[SerializeField]
	private List<HamburgerPlateSlot> _plateSlots;

	public void Init(int inLevel)
	{
		SetLevel (inLevel);
	}

	private bool SetLevel(int inLevel)
	{
		if (inLevel > KitchenConfig.HAMBURGER_PLATE_MAX_LEVEL) 
		{
			return false;
		}
			
		_level = inLevel;

		for (int i = 0; i < inLevel; i++) 
		{
			HamburgerPlateSlot slot = _plateSlots [i];
			slot.SetStatus (EPlateStatus.EMPTY);
		}

		return true;
	}
		
	public bool StartCook()
	{
		for (int i = 0; i < _plateSlots.Count; i++) 
		{
			HamburgerPlateSlot slot = _plateSlots [i];
			if (slot.IsCookable) 
			{
				SellingFood food = CookingManager.It.CreateFood (allowableFood);
				food.AddMaterial (EMaterialType.BREAD);
				slot.SetFood(food);
				return true;
			}
		}
		return false;
	}


	public bool AddMaterial(EMaterialType inMaterial, int inIndex = -1)
	{
		if (inIndex < 0) {
			for (int i = 0; i < _plateSlots.Count; i++) 
			{
				HamburgerPlateSlot slot = _plateSlots [i];


				SellingFood food = _plateSlots [i].Food;
				if (food.IsAcceptableMaterial (inMaterial)) 
				{
					food.AddMaterial (inMaterial);
					return true;
				}
			}
			return false;
		} 

		// Not cooking at inIndex
		if (inIndex >= _plateSlots.Count - 1) 
		{
			return false;
		}

		SellingFood exFood = _plateSlots[inIndex].Food;
		if (exFood.IsAcceptableMaterial (inMaterial) == false) 
		{
			return false;
		}
		exFood.AddMaterial (inMaterial);
		return true;
	}
}

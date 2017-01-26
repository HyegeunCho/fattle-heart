using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingFood : MonoBehaviour
{
	public EFoodType foodType;
	public int materials;

	public void Init(EFoodType inFoodType)
	{
		foodType = inFoodType;
	}

	public bool IsAcceptableMaterial(EMaterialType inMaterial)
	{
		if (foodType.Equals (EFoodType.HOTDOG) && (int)inMaterial > (int)EMaterialType.KETCHUP) 
		{
			return false;
		}

		if (foodType.Equals (EFoodType.HAMBURGER) && inMaterial.Equals (EMaterialType.KETCHUP)) 
		{
			return false;
		}
				
		if (foodType.Equals (EFoodType.COKE)) 
		{
			return false;	
		}

		if (HasMaterial (inMaterial)) 
		{
			return false;
		}

		return true;
	}

	public bool HasMaterial(EMaterialType inMaterialType)
	{
		return (int)(materials & (int)inMaterialType) > 0;
	}

	public bool AddMaterial(EMaterialType inMaterialType)
	{
		// already added
		if (HasMaterial (inMaterialType)) 
		{
			return false;
		}
		materials += (int)inMaterialType;
		UpdateView ();
		return true;
	}

	public void UpdateView()
	{
		Debug.Log(string.Format("[SellingFood] foodType = {0} / materials = {1}", foodType.ToString(), materials));
	}

}

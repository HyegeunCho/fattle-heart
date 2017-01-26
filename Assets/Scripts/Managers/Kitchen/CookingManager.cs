using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour 
{
	// Singleton
	private static CookingManager _instance;
	public static CookingManager It 
	{
		get 
		{
			if (_instance == null) 
			{
				_instance = new CookingManager ();
			}
			return _instance;
		}
	}

	// Prices
	private int priceCoke = 1000;
	private int priceHamburgerBread = 200;
	private int pricePatty = 500;
	private int priceHotdogBread = 300;
	private int priceSausage = 500;
	private int priceKetchup = 100;
	private int priceCabbage = 300;
	private int priceTomato = 400;

	// SellingFood Prefab
	[SerializeField]
	private GameObject foodPrefab;

	public GameObject FoodPrefab
	{
		get 
		{
			return foodPrefab;
		}
	}

	public SellingFood CreateFood(EFoodType inFoodType)
	{
		GameObject food = Instantiate (foodPrefab) as GameObject;
		if (food == null) 
		{
			return null;
		}

		SellingFood result = food.GetComponent<SellingFood> ();
		result.Init (inFoodType);
		return result;
	}

	// Use this for initialization
	void Start () {
		
	}
	

}

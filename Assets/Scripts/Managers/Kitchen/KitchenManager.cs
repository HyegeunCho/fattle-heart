using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour {

	[SerializeField]
	private int _fixedWidth = 1280;
	[SerializeField]
	private int _fixedHeight = 720;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (_fixedWidth, _fixedHeight, true);
	}

	private void createKitchenObjects()
	{
		
	}
}

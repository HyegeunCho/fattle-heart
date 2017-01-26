using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
	public class BattleManager : MonoBehaviour 
	{
		[SerializeField]
		private readonly int _screenWidth = BattleConfig.SCREEN_WIDTH;
		[SerializeField]
		private readonly int _screenHeight = BattleConfig.SCREEN_HEIGHT;

		[SerializeField]
		private GameObject _lowerBackground;

		void Start () {
			Screen.SetResolution (_screenWidth, _screenHeight, true);
			Camera.main.orthographicSize = Mathf.CeilToInt (_screenHeight / 2);

			MeshRenderer temp = _lowerBackground.GetComponent<MeshRenderer> ();
			temp.material.color = Color.green;
			//_lowerBackground.GetComponent<Renderer> ().material.color = Color.green;
		}


		void Update () {

		}
	}
}


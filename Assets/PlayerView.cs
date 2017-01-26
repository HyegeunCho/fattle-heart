using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fattleheart.battle
{
	public class PlayerView : MonoBehaviour {

		struct SMouseData
		{
			public Vector3 buttonDownPosition;
			public float buttonDownTime;
			public Vector3 buttonUpPosition;
			public float buttonUpTime;

			public TouchPhase mousePhase;
			public List<Vector3> dragPositions;

			public bool isDragged;

			public void clear()
			{
				buttonDownPosition = Vector3.zero;
				buttonUpPosition = Vector3.zero;
				buttonDownTime = 0f;
				buttonUpTime = 0f;
				mousePhase = TouchPhase.Ended;
				dragPositions = new List<Vector3>();
			}
		};

		[SerializeField]
		private LineRenderer _waypoint;

		private SMouseData _mouseData;
		private List<Vector3> movePositions;


		void Start()
		{
			_mouseData = new SMouseData ();
			_mouseData.clear ();

			movePositions = new List<Vector3> ();
		}

		void Update()
		{
			// Touch and Move
			if (Input.touchSupported) {
				UpdateTouch ();
			} else {
				UpdateMouse ();
			}
		}


		private void UpdateMouse()
		{
			Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			currentMousePosition.z = 0;

			if (Input.GetMouseButtonDown (0)) {
				_mouseData.mousePhase = TouchPhase.Began;
				_mouseData.buttonDownPosition = currentMousePosition;
				_mouseData.buttonDownPosition.z = 0;
				_mouseData.buttonDownTime = Time.time;

				OnMouseButtonDown ();
			}

			if (Input.GetMouseButton(0)) {
				//if (_mouseData.buttonDownPosition.Equals (currentMousePosition)) {				
				if (Vector3.Distance (_mouseData.buttonUpPosition, currentMousePosition) < BattleConfig.MOUSE_STATIONLY_OFFSET) {
					_mouseData.mousePhase = TouchPhase.Stationary;
				} else {
					_mouseData.isDragged = true;
					_mouseData.mousePhase = TouchPhase.Moved;
					//Debug.Log (string.Format ("[PlayerView-UpdateMouse] Moved"));
					_mouseData.dragPositions.Add (currentMousePosition);
				}
			}

			if (Input.GetMouseButtonUp (0)) {
				//Debug.Log (string.Format ("[PlayerView-UpdateMouse] GetMouseButtonUp"));
				_mouseData.mousePhase = TouchPhase.Ended;
				_mouseData.buttonUpPosition = currentMousePosition;
				_mouseData.buttonUpPosition.z = 0;
				_mouseData.buttonUpTime = Time.time;
				_mouseData.isDragged = ! (_mouseData.buttonDownPosition.Equals(_mouseData.buttonUpPosition));

				OnMouseButtonUp ();

				_mouseData.clear ();
					
			}


		}

		private void OnMouseButtonDown()
		{
			movePositions.Clear ();
		}

		private void OnMouseButtonUp()
		{
			if (_mouseData.isDragged) {
				if (_mouseData.dragPositions.Count > 0) {
					movePositions = _mouseData.dragPositions;
					DrawWaypoint (movePositions);
				}

				MoveWaypoint();
			} else {
				//Debug.Log (string.Format ("[PlayerView-OnMouseButtonUp] not dragging"));
				MoveTo (_mouseData);
			}
		}
			
		private IEnumerator StepTo(Vector3 from, Vector3 to, float speed)
		{
			float timePassed = 0f;
			float maxTimePassed = Vector3.Distance (from, to) / speed;

			while (timePassed < maxTimePassed) {
				timePassed += Time.deltaTime;
				gameObject.transform.position = Vector3.Lerp (from, to, timePassed / maxTimePassed);
				yield return null;
			}
		}

		private IEnumerator StepToWaypoints (List<Vector3> path)
		{
			foreach (Vector3 position in path) {
				
				Vector3[] outWaypoints = new Vector3[_waypoint.numPositions]{};
				_waypoint.GetPositions (outWaypoints);
				List<Vector3> waypoints = new List<Vector3> ();
				for (int i = 0; i < outWaypoints.Length; i++) {
					waypoints.Add (outWaypoints [i]);
				}
				waypoints.RemoveRange (0, waypoints.IndexOf (position) + 1);
				_waypoint.SetPositions (waypoints.ToArray());
				yield return StepTo (gameObject.transform.position, position, 100);
			}
		}

		private void MoveWaypoint()
		{
			StopAllCoroutines ();
			StartCoroutine (StepToWaypoints (movePositions));
		}

		private void MoveTo(SMouseData inMouseData)
		{
			//Debug.Log (string.Format ("[PlayerView-MoveTo] Move from {0} To {1}", gameObject.transform.position.ToString(), inMouseData.buttonUpPosition.ToString ()));
			StopAllCoroutines ();
			StartCoroutine (StepTo (gameObject.transform.position, inMouseData.buttonUpPosition, 100));
		}

		private void DrawWaypoint(List<Vector3> inWaypoints)
		{
			if (inWaypoints == null || inWaypoints.Count == 0) {
				Debug.Log (string.Format ("[PlayerView=DrawWaypoint] movePositions == null"));
				return;
			}
			_waypoint.numPositions = inWaypoints.Count;
			_waypoint.SetPositions (inWaypoints.ToArray ());
			_waypoint.sortingOrder = -1;
		}



		private void UpdateTouch(){}

		// order layer 
		void OnTriggerEnter(Collider col)
		{
			Debug.Log (string.Format ("[PlayerView-OnTriggerEnter] collider {0}", col.ToString ()));
		}

	}
}



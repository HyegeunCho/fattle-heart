using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace fattleheart.battle
{
    public class PlayerMoveController : MonoBehaviour
    {

        [SerializeField]
        private LineRenderer _waypoint;

        private List<Vector3> waypoints;

        // Use this for initialization
        void Start()
        {
            waypoints = new List<Vector3>();
        }

        public void OnMouseButtonDown(SMouseData inMouseData)
        {
            waypoints.Clear();
        }

        public void OnMouseButtonUp(SMouseData inMouseData)
        {
            if (inMouseData.isDragged)
            {
                if (inMouseData.dragPositions.Count > 0)
                {
                    MoveWaypoint(inMouseData);
                }
            }
            else
            {
                MoveTo(inMouseData);
            }
        }

        #region MOVE_PLAYER   
        private IEnumerator StepTo(Vector3 from, Vector3 to, float speed, bool isWaypoint = false)
        {
            float timePassed = 0f;
            float maxTimePassed = Vector3.Distance(from, to) / speed;

            while (timePassed < maxTimePassed)
            {
                timePassed += Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(from, to, timePassed / maxTimePassed);
                if (isWaypoint)
                {
                    for (int i = 0; i < waypoints.Count; i++)
                    {
                        if (GetComponent<Collider2D>().OverlapPoint(waypoints[i]))
                        {
                            waypoints.RemoveAt(i--);
                        }
                        else
                        {
                            break;
                        }
                    }

                    /*
                    int checkIndex = waypoints.FindIndex(pos => Vector3.Distance(gameObject.transform.position, pos) <= BattleConfig.MOUSE_STATIONLY_OFFSET);
                    if (checkIndex >= 0)
                    {
                        Debug.Log(string.Format("waypoints.FindIndex | gameObject {0} <-> pos {1} : {2} | [{3}]", gameObject.transform.position.ToString(), waypoints[checkIndex].ToString(), Vector3.Distance(gameObject.transform.position, waypoints[checkIndex]), checkIndex));
                        waypoints.RemoveRange(0, checkIndex + 1);
                    }
                    */
                    waypoints.Insert(0, gameObject.transform.position);
                    DrawWaypoint(waypoints);

                }
                else
                {
                    List<Vector3> fromToWaypoint = new List<Vector3>();
                    fromToWaypoint.Add(gameObject.transform.position);
                    fromToWaypoint.Add(to);
                    DrawWaypoint(fromToWaypoint);
                    fromToWaypoint = null;
                }
                yield return null;
            }
        }

        private IEnumerator StepToWaypoints(SMouseData inMouseData)
        {
            // setting waypoints for draw line
            waypoints = new List<Vector3>(inMouseData.dragPositions);
            foreach (Vector3 position in inMouseData.dragPositions)
            {
                yield return StepTo(gameObject.transform.position, position, 100, true);
            }
        }

        private void MoveWaypoint(SMouseData inMouseData)
        {
            StopAllCoroutines();
            StartCoroutine(StepToWaypoints(inMouseData));
        }

        private void MoveTo(SMouseData inMouseData)
        {
            //Debug.Log (string.Format ("[PlayerView-MoveTo] Move from {0} To {1}", gameObject.transform.position.ToString(), inMouseData.buttonUpPosition.ToString ()));
            StopAllCoroutines();
            StartCoroutine(StepTo(gameObject.transform.position, inMouseData.buttonUpPosition, 100));
        }
        #endregion

        #region WAYPOINT
        private void DrawWaypoint(List<Vector3> inWaypoints)
        {
            if (inWaypoints == null || inWaypoints.Count == 0)
            {
                Debug.Log(string.Format("[PlayerView=DrawWaypoint] movePositions == null"));
                return;
            }
            _waypoint.numPositions = inWaypoints.Count;
            _waypoint.SetPositions(inWaypoints.ToArray());
            _waypoint.sortingOrder = -1;
        }
        #endregion
    }
}


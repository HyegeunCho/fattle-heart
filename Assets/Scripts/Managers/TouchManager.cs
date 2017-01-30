using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerView = fattleheart.battle.PlayerView;

public struct SMouseData
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


public class TouchManager : MonoBehaviour {

    public List<fattleheart.battle.PlayerView> touchDelegators;
    private SMouseData _mouseData;

    void Start () {
        _mouseData = new SMouseData();
        _mouseData.clear();
    }
	

	void Update () {
        UpdateMouse();
        /*
        if (Input.touchSupported)
        {
            UpdateTouch();
        }
        else
        {
            UpdateMouse();
        }
        */
    }


    private void UpdateMouse()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            _mouseData.mousePhase = TouchPhase.Began;
            _mouseData.buttonDownPosition = currentMousePosition;
            _mouseData.buttonDownPosition.z = 0;
            _mouseData.buttonDownTime = Time.time;

            foreach (PlayerView pv in touchDelegators)
            {
                pv.OnMouseButtonDown(_mouseData);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_mouseData.buttonDownPosition.Equals (currentMousePosition)) 
            //if (Vector3.Distance(_mouseData.buttonDownPosition, currentMousePosition) < BattleConfig.MOUSE_STATIONLY_OFFSET)
            {
                _mouseData.mousePhase = TouchPhase.Stationary;
            }
            else {
                _mouseData.isDragged = true;
                _mouseData.mousePhase = TouchPhase.Moved;
                _mouseData.dragPositions.Add(currentMousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mouseData.mousePhase = TouchPhase.Ended;
            _mouseData.buttonUpPosition = currentMousePosition;
            _mouseData.buttonUpPosition.z = 0;
            _mouseData.buttonUpTime = Time.time;
            _mouseData.isDragged = !(_mouseData.buttonDownPosition.Equals(_mouseData.buttonUpPosition));

            foreach (PlayerView pv in touchDelegators)
            {
                pv.OnMouseButtonUp(_mouseData);
            }
            _mouseData.clear();
        }
    }
}

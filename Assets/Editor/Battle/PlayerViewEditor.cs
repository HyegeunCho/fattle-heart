using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WarriorStatusBar = fattleheart.battle.WarriorStatusBar;

[CustomEditor(typeof(WarriorStatusBar))]
public class PlayerViewEditor : UnityEditor.Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WarriorStatusBar myScript = (WarriorStatusBar)target;

        if (GUILayout.Button("Set HP to"))
        {
            myScript.HealthPoint = myScript.testHealthPoint;
            EditorUtility.SetDirty(myScript);
        }

        if (GUILayout.Button("Set Energy to"))
        {
            myScript.EnergyPoint = myScript.testEnergyPoint;
            EditorUtility.SetDirty(myScript);
        }
    }
}

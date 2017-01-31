using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BattlePlayerView = fattleheart.battle.PlayerView;

[CustomEditor(typeof(BattlePlayerView))]
public class PlayerViewEditor : UnityEditor.Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BattlePlayerView myScript = (BattlePlayerView)target;

        if (GUILayout.Button("Set HP to"))
        {
            myScript.SetHealthPoint(myScript.HealthPoint);
            EditorUtility.SetDirty(myScript);
        }
    }
}

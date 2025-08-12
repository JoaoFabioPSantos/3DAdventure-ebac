using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager fsm = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("StateMachine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.currentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.currentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaiable States");
        if (showFoldout)
        {
            if (fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var values = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; ++i)
                {
                    EditorGUILayout.LabelField($"{keys[i]} :: {values[i]}");
                }
            }
        }

    }
}

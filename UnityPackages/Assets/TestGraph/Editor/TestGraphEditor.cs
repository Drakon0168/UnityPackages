using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestGraph))]
public class TestGraphEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Edit Graph"))
        {
            TestGraphWindow.Init((TestGraph)target);
        }
    }
}

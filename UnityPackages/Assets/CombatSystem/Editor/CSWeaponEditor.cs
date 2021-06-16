using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CombatSystem
{
    [CustomEditor(typeof(CSWeapon))]
    public class CSWeaponEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stats"), new GUIContent("Stats", "The base stats of this weapon."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("combo"), new GUIContent("Combo", "The combo tree used by this weapon."));

            if (GUILayout.Button("Edit Combo"))
            {
                EditorWindow.GetWindow<CSComboEditor>().OpenWindow((CSCombo)serializedObject.FindProperty("combo").objectReferenceValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
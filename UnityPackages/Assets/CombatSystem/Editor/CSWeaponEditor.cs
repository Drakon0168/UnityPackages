using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CombatSystem
{
    [CustomEditor(typeof(CSWeapon))]
    public class CSWeaponEditor : Editor
    {
        private SerializedProperty stats;
        private SerializedProperty attackTypes;
        private SerializedProperty combo;

        private void OnEnable()
        {
            stats = serializedObject.FindProperty("stats");
            attackTypes = serializedObject.FindProperty("attackTypes");
            combo = serializedObject.FindProperty("combo");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(stats, new GUIContent("Stats", "The base stats of this weapon."));
            int attackTypeCount = attackTypes.arraySize;
            EditorGUILayout.PropertyField(attackTypes, new GUIContent("Attack Types", "A list of the different inputs that this weapon supports."));

            CSWeapon weapon = (CSWeapon)serializedObject.targetObject;

            if (GUILayout.Button("Edit Combo"))
            {
                CSComboEditor.OpenWindow(weapon.Combo);
            }

            if (serializedObject.ApplyModifiedProperties())
            {
                if (attackTypeCount != attackTypes.arraySize)
                {
                    //TODO: Check for changes in attack name as well
                    weapon.Combo.Chains = weapon.Chains;
                }
            }
        }
    }
}
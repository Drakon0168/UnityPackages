using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CombatSystem;

public class CSComboEditor : EditorWindow
{
    [MenuItem("Window/CombatSystem/ComboEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CSComboEditor>("Combo Editor");
    }

    private void OnGUI()
    {
        if (Selection.activeGameObject != null)
        {
            CSAttack attack = Selection.activeGameObject.GetComponent<CSAttack>();

            if (attack != null)
            {
                if (attack is CSMeleeAttack)
                {
                    CSMeleeAttack meleeAttack = (CSMeleeAttack)attack;

                    GUILayout.Label("Melee Attack", EditorStyles.boldLabel);
                    meleeAttack.Anticipation = EditorGUILayout.FloatField("Anticipation", meleeAttack.Anticipation);
                    meleeAttack.Anticipation = EditorGUILayout.FloatField("Attack Time", meleeAttack.AttackTime);
                    meleeAttack.Anticipation = EditorGUILayout.FloatField("Recovery", meleeAttack.Recovery);
                    meleeAttack.Anticipation = EditorGUILayout.FloatField("Combo Time", meleeAttack.ComboTime);
                }
            }
        }
    }
}
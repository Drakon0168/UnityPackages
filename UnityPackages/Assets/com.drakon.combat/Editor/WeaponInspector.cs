using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace Drakon.CombatSystem
{
    [CustomEditor(typeof(CSWeapon))]
    public class WeaponInspector : Editor
    {
        private CSWeapon weapon;

        private void OnEnable()
        {
            weapon = (CSWeapon)target;
        }

        public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            Button editButton = new Button(() =>
            {
                WeaponEditorWindow window = EditorWindow.GetWindow<WeaponEditorWindow>();
                window.Setup();
                window.Show();
            });
            editButton.text = "Edit Combos";
            root.Add(editButton);

            return root;
        }
    }
}
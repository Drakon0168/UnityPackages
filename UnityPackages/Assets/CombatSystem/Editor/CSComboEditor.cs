using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;

namespace CombatSystem
{
    public class CSComboEditor : EditorWindow
    {
        private CSComboGraphView graph;
        private Toolbar toolbar;

        public static void OpenWindow(CSCombo asset)
        {
            CSComboEditor window = GetWindow<CSComboEditor>();
            window.Setup(asset);
            window.titleContent.text = "Combo Editor";
            window.Show();
        }

        public void Setup(CSCombo asset)
        {
            graph = new CSComboGraphView();
            graph.Setup(asset);
            graph.StretchToParentSize();
            rootVisualElement.Add(graph);

            toolbar = new Toolbar();

            Button saveButton = new Button(graph.Save);
            saveButton.text = "Save";
            toolbar.Add(saveButton);

            Button addButton = new Button(() =>
            {
                graph.AddNode("New Attack");
            });
            addButton.text = "Add Node";
            toolbar.Add(addButton);

            rootVisualElement.Add(toolbar);
        }
    }
}
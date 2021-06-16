using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

public class TestGraphWindow : EditorWindow
{
    private Toolbar toolbar;
    private TestGraphView graphView;

    public static void Init(TestGraph asset)
    {
        TestGraphWindow window = GetWindow<TestGraphWindow>();
        window.titleContent.text = "Test Graph Editor";
        window.Setup(asset);
        window.Show();
    }

    private void Setup(TestGraph asset)
    {
        graphView = new TestGraphView(asset);
        rootVisualElement.Add(graphView);
        graphView.StretchToParentSize();

        toolbar = new Toolbar();

        ToolbarButton saveButton = new ToolbarButton(() =>
        {
            graphView.Save();
        });
        saveButton.text = "Save";
        toolbar.Add(saveButton);

        ToolbarButton addButton = new ToolbarButton(() =>
        {

        });
        addButton.text = "Add Node";
        toolbar.Add(addButton);

        rootVisualElement.Add(toolbar);
    }
}

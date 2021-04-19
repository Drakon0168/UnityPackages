using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using CombatSystem;

public class CSComboEditor : EditorWindow
{
    private CSComboGraph graph;
    private Toolbar toolbar;

    [OnOpenAsset(1)]
    public static bool Init(int instanceID, int col)
    {
        Object asset = EditorUtility.InstanceIDToObject(instanceID);

        if(asset is CSComboTree)
        {
            CSComboEditor window = GetWindow<CSComboEditor>();
            window.Show();
            window.Setup((CSComboTree)asset);
            return true;
        }

        return false;
    }

    public void Setup(CSComboTree asset)
    {
        SetupGraph(asset);
        SetupToolbar();
    }

    public void SetupGraph(CSComboTree comboTree)
    {
        graph = new CSComboGraph(this, comboTree);
        graph.StretchToParentSize();
        rootVisualElement.Add(graph);
    }

    public void SetupToolbar()
    {
        toolbar = new Toolbar();

        ToolbarButton addNodeButton = new ToolbarButton(AddNode);
        addNodeButton.text = "Add Node";
        toolbar.Add(addNodeButton);

        ToolbarButton saveButton = new ToolbarButton(Save);
        saveButton.text = "Save";
        toolbar.Add(saveButton);

        rootVisualElement.Add(toolbar);
    }

    public void AddNode()
    {
        graph.AddNode("New Attack");
    }

    public void Save()
    {
        Debug.LogError("Saving has not been implemented.");
    }

    public void OnDisable()
    {
        if (rootVisualElement.Contains(graph))
        {
            rootVisualElement.Remove(graph);
        }
    }
}
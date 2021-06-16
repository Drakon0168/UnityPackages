using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

public class TestGraphViewNode : Node
{
    private TestData data;

    /// <summary>
    /// The data represented by this node
    /// </summary>
    public TestData Data
    {
        get { return data; }
        set { data = value; }
    }

    public TestGraphViewNode(TestData data) : base()
    {
        this.data = data;

        title = data.Name;

        Label titleLabel = titleContainer.Q<Label>("title-label");
        TextField titleInput = new TextField();
        titleInput.SetValueWithoutNotify(data.Name);
        titleInput.isDelayed = true;
        titleInput.RegisterValueChangedCallback(e =>
        {
            title = e.newValue;
            titleInput.style.display = DisplayStyle.None;
            titleLabel.style.display = DisplayStyle.Flex;
        });

        titleLabel.AddManipulator(new Clickable(() =>
        {
            titleInput.style.display = DisplayStyle.Flex;
            titleLabel.style.display = DisplayStyle.None;

            titleInput.Q<VisualElement>("unity-text-input").Focus();
        }));

        FloatField valueField = new FloatField();
        valueField.label = "Value: ";
        valueField.SetValueWithoutNotify(data.Value);
        valueField.RegisterValueChangedCallback(e => 
        {
            data.Value = e.newValue;
        });
        contentContainer.Add(valueField);
    }

    public void Save()
    {

    }
}

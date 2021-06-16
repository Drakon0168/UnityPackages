using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class TestGraphView : GraphView
{
    private TestGraph asset;
    private TestGraphViewNode entry;

    public TestGraphView(TestGraph asset)
    {
        this.asset = asset;

        styleSheets.Add(Resources.Load<StyleSheet>("StyleSheet"));

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ContentDragger());

        GridBackground grid = new GridBackground();
        grid.StretchToParentSize();
        Insert(0, grid);

        Load();
    }

    #region Data Management

    private void Load()
    {
        Port inputPort;
        AddNode(asset.Entry, out inputPort, true);
    }

    public void Save()
    {

    }

    #endregion

    #region Node Management

    public void AddNode(TestGraphViewNode parent, string name, out Port inputPort, bool entry = false)
    {
        TestData data = new TestData(name);
        AddNode(data, out inputPort, entry);
    }

    public void AddNode(TestData data, out Port inputPort, bool entry = false)
    {
        TestGraphViewNode newNode = new TestGraphViewNode(data);

        //Setup Ports
        inputPort = null;
        if (!entry)
        {
            inputPort = AddPort(newNode, Orientation.Horizontal, Direction.Input);
        }

        for(int i = 0; i < data.Children.Count; i++)
        {
            Port outputPort = AddPort(newNode, Orientation.Horizontal, Direction.Output);
            Port newInput;
            AddNode(data.Children[i], out newInput, false);
            outputPort.ConnectTo(newInput);
        }

        Button button = new Button(() =>
        {
            AddPort(newNode, Orientation.Horizontal, Direction.Output);
            newNode.RefreshPorts();
            newNode.RefreshExpandedState();
        });
        button.text = "+";
        newNode.titleContainer.Add(button);

        newNode.RefreshPorts();
        newNode.RefreshExpandedState();
        AddElement(newNode);
    }

    #endregion

    #region Port Management

    private Port AddPort(Node parent, Orientation orientation, Direction direction)
    {
        Port newPort = parent.InstantiatePort(orientation, direction, Port.Capacity.Single, typeof(TestData));

        switch (direction)
        {
            case Direction.Input:
                newPort.portName = "Input";
                parent.inputContainer.Add(newPort);
                break;
            case Direction.Output:
                newPort.portName = "Output";
                parent.outputContainer.Add(newPort);
                break;
        }

        return newPort;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (port.node != startPort.node && port.direction != startPort.direction)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    #endregion
}

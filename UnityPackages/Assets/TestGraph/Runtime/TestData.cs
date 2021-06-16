using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestData
{
    [SerializeField]
    private string name;
    [SerializeField]
    private float value;

    [SerializeField]
    private Vector2 graphPosition;
    [SerializeReference]
    private TestData parent;
    [SerializeReference]
    private List<TestData> children;

    /// <summary>
    /// The name of this node on the graph
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// The data stored by the graph at this node
    /// </summary>
    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    /// <summary>
    /// The position of this node in the graph view
    /// </summary>
    public Vector2 GraphPosition
    {
        get { return graphPosition; }
        set { graphPosition = value; }
    }

    /// <summary>
    /// The node that this node is a child of
    /// </summary>
    public TestData Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    /// <summary>
    /// The nodes that this node outputs to
    /// </summary>
    public List<TestData> Children
    {
        get { return children; }
        set { children = value; }
    }

    public TestData(string name, float value = 0.0f)
    {
        this.name = name;
        this.value = value;
        children = new List<TestData>();
    }
}

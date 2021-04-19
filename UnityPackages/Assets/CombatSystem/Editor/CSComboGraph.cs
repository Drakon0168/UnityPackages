using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace CombatSystem
{
    public class CSComboGraph : GraphView
    {
        private CSComboEditor window;
        private CSComboTree target;

        public CSComboGraph(CSComboEditor window, CSComboTree target)
        {
            this.window = window;
            this.target = target;

            styleSheets.Add(Resources.Load<StyleSheet>("StyleSheet"));

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentDragger());

            GridBackground grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            CSComboNode entryNode = GenerateEntryNode();
            AddElement(entryNode);
        }

        public void AddNode(string name)
        {
            AddElement(GenerateNode(name));
        }

        #region Node Management

        private CSComboNode GenerateNode(string name)
        {
            CSComboNode node = new CSComboNode();
            node.title = name;
            node.GUID = GUID.Generate().ToString();

            Port inputPort = GeneratePort(node, Direction.Input);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            CSAttackField attackField = new CSAttackField("Attack");
            node.inputContainer.Add(attackField);

            for (int i = 0; i < target.InputNames.Length; i++)
            {
                Port newPort = GeneratePort(node);
                newPort.portName = target.InputNames[i];
                node.outputContainer.Add(newPort);
            }

            node.RefreshExpandedState();
            node.RefreshPorts();
            return node;
        }

        private CSComboNode GenerateEntryNode()
        {
            CSComboNode entryNode = new CSComboNode();
            entryNode.title = "Idle";
            entryNode.GUID = GUID.Generate().ToString();
            entryNode.entryPoint = true;

            for(int i = 0; i < target.InputNames.Length; i++)
            {
                Port newPort = GeneratePort(entryNode);
                newPort.portName = target.InputNames[i];
                entryNode.outputContainer.Add(newPort);
            }

            entryNode.RefreshExpandedState();
            entryNode.RefreshPorts();

            entryNode.SetPosition(new Rect((window.position.width / 2.0f) - 75, (window.position.height / 2.0f) - 50, 150, 100));

            return entryNode;
        }

        #endregion

        #region Port Management

        private Port GeneratePort(CSComboNode node, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port newPort = node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(CSAttack));
            return newPort;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach((port) =>
            {
                if(port != startPort && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        #endregion
    }
}
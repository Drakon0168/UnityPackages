using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace CombatSystem
{
    public class CSComboGraphViewNode : Node
    {
        private CSAttack data;
        private bool entry;

        public CSAttack Data
        {
            get { return data; }
        }

        public CSComboGraphViewNode(CSAttack data, bool entry = false) : base()
        {
            this.data = data;
            this.entry = entry;
        }

        public void Setup()
        {
            title = data.Name;

            Label titleLabel = titleContainer.Q<Label>();
            titleLabel.AddManipulator(new Clickable(e => 
            {
                titleLabel.visible = false;

                TextField titleField = new TextField();
                titleField.isDelayed = true;
                titleField.value = title;
                titleField.RegisterCallback<FocusOutEvent>(a =>
                {
                    title = titleField.value;
                    data.Name = titleField.value;
                    titleField.RemoveFromHierarchy();
                    titleLabel.visible = true;
                });
                titleContainer.Add(titleField);
                titleField.SendToBack();
            }));
        }

        public void Save(CSCombo asset)
        {
            data.GraphPosition = GetPosition().position;

            if (entry)
            {
                asset.Entry = data;
            }

            IEnumerator<VisualElement> children = outputContainer.Children().GetEnumerator();
            int chain = 0;

            while (children.MoveNext())
            {
                if(children.Current is Port)
                {
                    Port port = (Port)children.Current;

                    IEnumerator<Edge> edges = port.connections.GetEnumerator();

                    while (edges.MoveNext())
                    {
                        CSComboGraphViewNode node = (CSComboGraphViewNode)edges.Current.input.node;

                        asset.AddAttack(node.Data, Data, chain);
                        node.Save(asset);
                    }
                    chain++;
                }
            }
        }
    }
}
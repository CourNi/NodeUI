using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class ComponentNode : Node
    {
        private ConnectionPoint _transformPoint;
        private List<ConnectionPoint> _paramsPoints = new();

        public List<ConnectionPoint> ParamsPoints { get => _paramsPoints; set => _paramsPoints = value; }

        public ComponentNode(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Component;
            _paramsPoints.Add(_transformPoint = new ConnectionPoint(this, ConnectionPointType.ParamOut, OnClick));
            SetSize(150, 90);
        }

        public override void Draw()
        {
            DrawBox();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 150, 70), "General", NodeStyle.Header);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 50, 110, 20), "Transform", NodeStyle.LabelRight);
            GUI.color = Color.blue;
            _transformPoint.Draw(50);
        }
    }
}
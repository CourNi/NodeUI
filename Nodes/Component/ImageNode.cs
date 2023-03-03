using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class ImageNode : Node
    {
        private ConnectionPoint _transformPoint;
        private ConnectionPoint _spritePoint;
        private ConnectionPoint _colorPoint;
        private List<ConnectionPoint> _paramsPoints = new();

        public List<ConnectionPoint> ParamsPoints { get => _paramsPoints; set => _paramsPoints = value; }

        public ImageNode(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Component;
            _paramsPoints.Add(_transformPoint = new ConnectionPoint(this ,ConnectionPointType.ParamOut, OnClick));
            _paramsPoints.Add(_spritePoint = new ConnectionPoint(this, ConnectionPointType.ParamOut, OnClick));
            _paramsPoints.Add(_colorPoint = new ConnectionPoint(this, ConnectionPointType.ParamOut, OnClick));
            SetSize(150, 120);
        }

        public override void Draw()
        {
            DrawBox();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 150, 70), "Image", NodeStyle.Header);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 50, 110, 20), "Transform", NodeStyle.LabelRight);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 70, 110, 20), "Sprite", NodeStyle.LabelRight);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 90, 110, 20), "Color", NodeStyle.LabelRight);
            GUI.color = Color.blue;
            _transformPoint.Draw(50);
            _spritePoint.Draw(70);
            _colorPoint.Draw(90);
        }
    }
}
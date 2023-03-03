using System;
using UnityEditor;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class ActionNodeTransform : Node
    {
        private ConnectionPoint _transformInPoint;

        private Vector3 _transform;
        private string[] _types = { "Position", "Rotation", "Scale" };
        private int _typeT = 0;
        private bool _add = false;

        public ActionNodeTransform(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Action;
            _color = new Color(1, 0.65f, 0);
            _transformInPoint = new ConnectionPoint(this, ConnectionPointType.ParamIn, OnClick);
            SetSize(230, 115);
        }

        public override void Draw()
        {
            base.Draw();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 150, 70), "Action: Transform", NodeStyle.Header);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 50, 50, 20), "Type", NodeStyle.Label);
            _typeT = EditorGUI.Popup(new Rect(RectNode.x + 60, RectNode.y + 50, 80, 20), _typeT, _types);
            GUI.Label(new Rect(RectNode.x + 175, RectNode.y + 50, 50, 20), "Add", NodeStyle.Label);
            _add = EditorGUI.Toggle(new Rect(RectNode.x + 150, RectNode.y + 50, 20, 20), _add);
            _transform = EditorGUI.Vector3Field(new Rect(RectNode.x + 20, RectNode.y + 75, 180, 20), "", _transform);
            GUI.color = Color.blue;
            _transformInPoint.Draw(75);
        }
    }
}
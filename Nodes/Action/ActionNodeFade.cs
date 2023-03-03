using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityTools.NodeUI {
    public class ActionNodeFade : Node
    {
        private ConnectionPoint _colorInPoint;

        private float _time = 0;
        private int _selected = 0;
        private string[] _options = { "In", "Out" };

        public ActionNodeFade(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) : 
            base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Action;
            _color = new Color(1, 0.65f, 0);
            _colorInPoint = new ConnectionPoint(this, ConnectionPointType.ParamIn, OnClick);
            SetSize(230, 90); 
        }

        public override void Draw()
        {
            base.Draw();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 230, 90), "Action: Fade In/Out", NodeStyle.Header);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 50, 50, 20), "Fade", NodeStyle.Label);
            _selected = EditorGUI.Popup(new Rect(RectNode.x + 60, RectNode.y + 50, 50, 20), _selected, _options);
            GUI.Label(new Rect(RectNode.x + 120, RectNode.y + 50, 50, 20), "for", NodeStyle.Label);
            _time = EditorGUI.FloatField(new Rect(RectNode.x + 150, RectNode.y + 50, 45, 20), _time);
            GUI.Label(new Rect(RectNode.x + 200, RectNode.y + 50, 50, 20), "s", NodeStyle.Label);
            GUI.color = Color.blue;
            _colorInPoint.Draw(50);
        }
    }
}
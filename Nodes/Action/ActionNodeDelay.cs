using System;
using UnityEditor;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class ActionNodeDelay : Node
    {
        private float _time = 0;

        public ActionNodeDelay(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Action;
            _color = new Color(0, 0.65f, 0);
            SetSize(180, 90);
        }

        public override void Draw()
        {
            base.Draw();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 230, 90), "Time: Delay", NodeStyle.Header);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 50, 50, 20), "Delay", NodeStyle.Label);
            _time = EditorGUI.FloatField(new Rect(RectNode.x + 65, RectNode.y + 50, 85, 20), _time);
            GUI.Label(new Rect(RectNode.x + 155, RectNode.y + 50, 50, 20), "s", NodeStyle.Label);
        }

        public override void Execute(Action action)
        {
            Debug.Log($"Test {DateTime.Now}");
        }
    }
}
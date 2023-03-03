using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class ExecuteNode : Node
    {
        public ExecuteNode(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Component;
            _color = new Color(0.5f, 0.2f, 0.4f);
        }

        public override void Draw()
        {
            DrawBox();
            DrawInPoint();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 150, 70), "Execute", NodeStyle.Header);
        }
    }
}
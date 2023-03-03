using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class OnClick : Node
    {
        private protected string _nodeTitle = "OnClick";

        public OnClick(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Event;
            _color = new Color(0.25f, 0.75f, 0.9f); 
        }

        public override void Draw()
        {
            DrawBox();
            DrawOutPoint();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 200, 70), $"Event: {_nodeTitle}", NodeStyle.Header);
        }
    }
}
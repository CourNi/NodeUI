using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityTools.NodeUI
{
    public class ActionNodeSprite : Node
    {
        private ConnectionPoint _spriteInPoint;

        private Sprite _sprite;

        public ActionNodeSprite(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode) 
            : base(position, OnClick, OnClickRemoveNode)
        {
            _type = NodeType.Action;
            _color = new Color(1, 0.65f, 0);
            _spriteInPoint = new ConnectionPoint(this, ConnectionPointType.ParamIn, OnClick);
            SetSize(230, 90);
        }

        public override void Draw()
        {
            base.Draw();
            GUI.color = Color.white;
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 15, 150, 70), "Action: Set Sprite", NodeStyle.Header);
            GUI.Label(new Rect(RectNode.x + 20, RectNode.y + 50, 50, 20), "Sprite", NodeStyle.Label);
            _sprite = (Sprite)EditorGUI.ObjectField(new Rect(RectNode.x + 80, RectNode.y + 50, 130, 16), _sprite, typeof(Sprite), false);
            GUI.color = Color.blue;
            _spriteInPoint.Draw(50);
        }
    }
}
using System;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public enum ConnectionPointType { In = 1, Out = -1, ParamIn = 2, ParamOut = -2 }

    public class ConnectionPoint
    {
        private const float MARGIN = 2f;

        private Rect _rect;
        private Node _node;
        private ConnectionPointType _type;
        private Action<ConnectionPoint> _onClickConnectionPoint;

        public Rect Rect { get => _rect; set => _rect = value; }
        public Node Node { get => _node; set => _node = value; }
        public ConnectionPointType Type { get => _type; set => _type = value; }

        public ConnectionPoint(Node node, ConnectionPointType type, Action<ConnectionPoint> OnClickConnectionPoint)
        {
            _node = node;
            _type = type;
            _onClickConnectionPoint = OnClickConnectionPoint;
            _rect = new Rect(0, 0, 15f, 15f);
        }

        public void Draw(float height = 0)
        {
            var sign = Mathf.Sign((int)_type);
            _rect.x = _node.RectNode.x + MARGIN * sign + (sign < 0 ? _node.RectNode.width : -_rect.width);
            _rect.y = _node.RectNode.y + (Mathf.Abs((int)_type) == 1 ? (_node.RectNode.height * 0.15f) - _rect.height * 0.15f : height);
            if (GUI.Button(Rect, "", NodeStyle.InOutPoint))
                _onClickConnectionPoint?.Invoke(this);
        }
    }
}
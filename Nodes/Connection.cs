using System;
using UnityEditor;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class Connection
    {
        private ConnectionPoint _inPoint;
        private ConnectionPoint _outPoint;
        private Action<Connection> _onClickRemoveConnection;

        public ConnectionPoint InPoint { get => _inPoint; set => _inPoint = value; }
        public ConnectionPoint OutPoint { get => _outPoint; set => _outPoint = value; }

        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
        {
            _inPoint = inPoint;
            _outPoint = outPoint;
            _onClickRemoveConnection = OnClickRemoveConnection;
        }

        public void Draw()
        {
            Vector2 vect = ((_inPoint.Rect.center + _outPoint.Rect.center) * 0.5f) - new Vector2(5f, 5f);
            Handles.DrawBezier(
                _inPoint.Rect.center, _outPoint.Rect.center,
                _inPoint.Rect.center + Vector2.left * 50f,
                _outPoint.Rect.center - Vector2.left * 50f,
                Color.gray, null, 5f);
            GUI.color = Color.red;
            if (GUI.Button(new Rect(vect.x, vect.y, 10, 10), ""))
                _onClickRemoveConnection?.Invoke(this);
        }
    }
}
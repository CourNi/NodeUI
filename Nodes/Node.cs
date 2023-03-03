using System;
using UnityEditor;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public enum NodeType { Action, Event, Component }

    public class Node
    {
        private Rect _rect;
        private bool _isDragged;
        private bool _isSelected;
        private Action<Node> _onRemoveNode;
        private ConnectionPoint _inPoint;
        private ConnectionPoint _outPoint;
        private protected Color _color = Color.gray;
        private protected NodeType _type;

        public Rect RectNode { get => _rect; set => _rect = value; }
        public ConnectionPoint InPoint { get => _inPoint; set => _inPoint = value; }
        public ConnectionPoint OutPoint { get => _outPoint; set => _outPoint = value; }

        public Node(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode)
        {
            _rect = new Rect(position.x, position.y, 200, 70);
            _inPoint = new ConnectionPoint(this, ConnectionPointType.In, OnClick);
            _outPoint = new ConnectionPoint(this, ConnectionPointType.Out, OnClick);
            _onRemoveNode = OnClickRemoveNode;
        }
        
        public virtual void Execute(Action action) { }

        public virtual void Draw()
        {
            DrawBox();
            DrawInPoint();
            DrawOutPoint();
        }

        #region DrawElements
        private protected void DrawBox()
        {
            GUI.color = _color;
            GUI.Box(_rect, "", _isSelected ? NodeStyle.NodeSelected : NodeStyle.Node);
        }

        private protected void DrawInPoint()
        {
            GUI.color = new Color(1f, 0.52f, 0.333f);
            _inPoint.Draw();
        }

        private protected void DrawOutPoint()
        {
            GUI.color = new Color(0.335f, 1f, 0.371f);
            _outPoint.Draw();
        }
        #endregion

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.KeyDown:
                    if (e.keyCode == KeyCode.Delete && _isSelected && _type != NodeType.Component)
                    {
                        _onRemoveNode?.Invoke(this);
                        GUI.changed = true;
                    }  
                    break;

                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (_rect.Contains(e.mousePosition))
                        {
                            _isDragged = true;
                            GUI.changed = true;
                            _isSelected = true;
                        }
                        else
                        {
                            GUI.changed = true;
                            _isSelected = false;
                        }
                    }
                    if (e.button == 1 && _isSelected && _rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    _isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && _isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }
            return false;
        }

        public void Drag(Vector2 delta) => _rect.position += delta;

        private protected void SetSize(int width, int height) => _rect.size = new Vector2(width, height);

        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            if (_type != NodeType.Component)
                genericMenu.AddItem(new GUIContent("Remove"), false, () => _onRemoveNode?.Invoke(this));
            genericMenu.ShowAsContext();
        }
    }
}
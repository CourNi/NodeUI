using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace UnityTools.NodeUI
{
    public class NodeUIEditor : EditorWindow
    {
        private NodeUI _object;
        private List<Node> _nodes = new();
        private List<Connection> _connections = new();

        private ConnectionPoint _activePoint;
        private NodeFactory _factory;

        private Vector2 _offset;
        private Vector2 _drag;

        [MenuItem("Window/NodeUI")]
        private static void OpenWindow()
        {
            NodeUIEditor window = GetWindow<NodeUIEditor>();
            window.titleContent = new GUIContent("NodeUI");
        }

        private void OnSelectionChange()
        {
            var selection = Selection.activeGameObject;
            if (selection != null)
            {
                if (selection.TryGetComponent(out NodeUI nodeui))
                {
                    _object = nodeui;
                    _nodes = nodeui.Nodes;
                    if (_nodes.Count == 0) InitNodeList();
                    _connections = nodeui.Connections;
                }
                else
                {
                    if (_object != null)
                    {
                        _object.Nodes = _nodes;
                        _object.Connections = _connections;
                    }
                    _object = null;
                    _nodes = null;
                    _connections = null;
                }
                Repaint();
            }
        }

        private void InitNodeList()
        {
            if (_object.TryGetComponent(out Image image))
                _nodes.Add(_factory.Create("ImageNode", new Vector2(100, 200)));
            else if (_object.TryGetComponent(out Text text))
                _nodes.Add(_factory.Create("TextNode", new Vector2(100, 200)));
            else
                _nodes.Add(_factory.Create("ComponentNode", new Vector2(100, 220)));
            _nodes.Add(_factory.Create("OnClick", new Vector2(100, 340)));
            _nodes.Add(_factory.Create("OnEnter", new Vector2(100, 420)));
            _nodes.Add(_factory.Create("OnExit", new Vector2(100, 500)));
            _nodes.Add(_factory.Create("ExecuteNode", new Vector2(1300, 500)));
        }

        private void OnEnable() => _factory = new(NodeList.Nodes.Values.ToList(), OnClick, OnClickRemoveNode);

        #region Draw
        private void OnGUI()
        {
            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);
            DrawConnections();
            DrawNodes();
            DrawConnectionLine(Event.current);
            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);
            if (GUI.changed) Repaint();
        }

        private void DrawNodes() => _nodes?.ToList().ForEach(i => i.Draw());

        private void DrawConnections() => _connections?.ToList().ForEach(i => i.Draw());

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            _offset += _drag * 0.5f;
            Vector3 newOffset = new Vector3(_offset.x % gridSpacing, _offset.y % gridSpacing, 0);
            for (int i = 0; i < widthDivs; i++)
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            for (int j = 0; j < heightDivs; j++)
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawConnectionLine(Event e)
        {
            if (_activePoint != null)
            {
                Handles.DrawBezier(
                    _activePoint.Rect.center, e.mousePosition,
                    _activePoint.Rect.center - Vector2.left * 50f * Mathf.Sign((int)_activePoint.Type) * -1,
                    e.mousePosition + Vector2.left * 50f * Mathf.Sign((int)_activePoint.Type) * -1,
                    Color.gray, null, 5f);
                GUI.changed = true;
            }           
        }
        #endregion

        #region Process
        private void ProcessEvents(Event e)
        {
            _drag = Vector2.zero;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                        _activePoint = null;
                    if (e.button == 1)
                        ProcessContextMenu(e.mousePosition);
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
                        OnDrag(e.delta);
                    break;
            }
        }

        private void ProcessNodeEvents(Event e) => _nodes?.ToList().ForEach(i => i.ProcessEvents(e));

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            NodeList.Nodes.Keys.ToList().ForEach(i =>
            {
                    if (NodeList.GetCategoryByKey(i) != "Component")
                        genericMenu.AddItem(new GUIContent(i), false, () => _nodes?.Add(_factory.Create(NodeList.Nodes[i].Name, mousePosition)));
            });
            genericMenu.ShowAsContext();        
        }
        #endregion

        #region Events
        private void OnDrag(Vector2 delta)
        {
            _drag = delta;
            _nodes?.ToList().ForEach(i => i.Drag(delta));
            GUI.changed = true;
        }

        private void OnClick(ConnectionPoint point)
        {
            if (_activePoint != null)
            {
                switch (_activePoint.Type)
                {
                    case ConnectionPointType.In:
                        if (point.Type == ConnectionPointType.Out)
                            _connections.Add(new Connection(_activePoint, point, OnClickRemoveConnection));
                        break;
                    case ConnectionPointType.Out:
                        if (point.Type == ConnectionPointType.In)
                            _connections.Add(new Connection(point, _activePoint, OnClickRemoveConnection));
                        break;
                    case ConnectionPointType.ParamIn:
                        if (point.Type == ConnectionPointType.ParamOut)
                            _connections.Add(new Connection(_activePoint, point, OnClickRemoveConnection));
                        break;
                    case ConnectionPointType.ParamOut:
                        if (point.Type == ConnectionPointType.ParamIn)
                            _connections.Add(new Connection(point, _activePoint, OnClickRemoveConnection));
                        break;
                }
                _activePoint = null;
            }
            else _activePoint = point;
        }

        private void OnClickRemoveNode(Node node)
        {
            _connections?.ToList().Where(i => i.InPoint.Node == node || i.OutPoint.Node == node)
                .ToList().ForEach(i => _connections.Remove(i));
            _nodes.Remove(node);
        }

        private void OnClickRemoveConnection(Connection connection) => _connections.Remove(connection);
        #endregion
    }
}


using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityTools.NodeUI.Utils;

namespace UnityTools.NodeUI
{
    public class NodeUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public class NodeComponent
        {
            private Component _component;

            public void Set<T>(T component) where T : Component
            {
                _component = component;
            }

            public bool TryGet<T>(out T component) where T : Component
            {
                component = null;
                if (_component is T)
                {
                    component = _component as T;
                    return true;
                }
                else return false;
            }

            public Transform GetTransform() { return _component.transform; }
        }

        [SerializeField] private ExecuteActions _onExecute;
        private NodeComponent _component = new();
        private List<Node> _nodes = new();
        private List<Connection> _connections = new();

        public List<Node> Nodes { get => _nodes; set => _nodes = value; }
        public List<Connection> Connections { get => _connections; set => _connections = value; }

        private NodeUtils _utils;

        private void Start()
        {
            _utils = new(this);
            if (TryGetComponent(out Image image)) _component.Set(image);
            else if (TryGetComponent(out TextMeshProUGUI text)) _component.Set(text);
            else _component.Set(transform);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var node = _nodes.FirstOrDefault(n => n.GetType() == typeof(OnClick));
            try
            {
                var connectedNodes = _connections.Where(c => c.InPoint == node.OutPoint).ToList();
                connectedNodes.ForEach(n => Debug.Log(n));
            }
            catch { }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var node = _nodes.FirstOrDefault(n => n.GetType() == typeof(OnEnter));
            try
            {
                node.Execute(() => { });
            }
            catch { }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var node = _nodes.FirstOrDefault(n => n.GetType() == typeof(OnExit));
            try
            {
                node.Execute(() => { });
            }
            catch { }
        }
    }
}
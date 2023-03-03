using System;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class NodeFactory
    {
        private Dictionary<string, Type> _nodes = new();
        private Action<ConnectionPoint> _onClick;
        private Action<Node> _onClickRemoveNode;

        public NodeFactory(List<Type> nodes, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode)
        {
            nodes.ForEach(i => _nodes.Add(i.Name, i));
            _onClick = OnClick;
            _onClickRemoveNode = OnClickRemoveNode;
        }

        public Node Create(string nodeType, Vector2 position)
        {
            Type type = _nodes[nodeType];
            var method = GetType().GetMethod("CreatorInit");
            var genMethod = method.MakeGenericMethod(new[] { typeof(Vector2), typeof(Action<ConnectionPoint>), typeof(Action<Node>), type });
            var node = genMethod.Invoke(this, new object[] { position, _onClick, _onClickRemoveNode });

            return (Node)node;
        }

        public static object CreatorInit<T1, T2, T3, T>(T1 position, T2 onClick, T3 onClickRemoveNode)
        {
            var creator = CreateCreator<T1, T2, T3, T>();
            return creator(position, onClick, onClickRemoveNode);
        }

        public static Func<T1, T2, T3, T> CreateCreator<T1, T2, T3, T>()
        {
            Type[] args = { typeof(T1), typeof(T2), typeof(T3) };
            var constructor = typeof(T).GetConstructor(args);

            var p1 = Expression.Parameter(typeof(T1), "p1");
            var p2 = Expression.Parameter(typeof(T2), "p2");
            var p3 = Expression.Parameter(typeof(T3), "p3");

            return Expression.Lambda<Func<T1, T2, T3, T>>(
                Expression.New(constructor, new Expression[] { p1, p2, p3 }), p1, p2, p3).Compile();
        }
    }
}
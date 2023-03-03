using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace UnityTools.NodeUI.Utils
{
    public delegate void NodeAction();

    public class NodeUtils
    {
        private ActionInjector _injector;

        public NodeUtils(MonoBehaviour mono)
        {
            _injector = new();
        }

        public class ActionInjector
        {
            private Queue<NodeAction> _actionQueue = new();
            private object _lock = new object();

            public void Update()
            {
                lock (_lock)
                    if (_actionQueue.Count > 0)
                        _actionQueue.Dequeue()();
            }

            public void Schedule(NodeAction action)
            {
                lock (_lock)
                    _actionQueue.Enqueue(action);
            }
        }

        public class Timer
        {
            private NodeUtils _parent;
            private System.Timers.Timer _timer;

            public Timer(NodeUtils parent) { _parent = parent; }

            public void Wait(float delay, Action Act)
            {
                _timer = new System.Timers.Timer(delay * 1000);
                _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    var timer = sender as System.Timers.Timer;
                    timer.Dispose();
                    _parent._injector.Schedule(new NodeAction(delegate { Act(); }));
                };
                _timer.Start();
            }

            public void Stop() =>
                _timer.Stop();
        }
    }
}
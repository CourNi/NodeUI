using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class OnEnter : OnClick
    {
        public OnEnter(Vector2 position, Action<ConnectionPoint> OnClick, Action<Node> OnClickRemoveNode)
            : base(position, OnClick, OnClickRemoveNode)
        {
            _nodeTitle = "OnEnter";
        }
    }
}